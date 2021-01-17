import { AfterViewInit, Component, ViewChild, OnInit, ElementRef } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { SingerEventOverviewDataSource } from './singerevent-overview-datasource';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import {
   SingerEventDetailsComponent,
   SingerEventDetailsFormData,
} from '../singerevent-details/singerevent-details.component';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';
import { SingerLocationService } from 'src/app/modules/core/services/singer-location-api/singer-location.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
   SingerRegistrationsComponent,
   SingerRegistrationData,
} from '../event-registrations/event-registrations.component';
import { SingerEventAdminRegisterComponent } from '../singer-eventadmin-register/singer-eventadmin-register.component';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { SingerLocation } from 'src/app/modules/core/models/singer-location.model';

@Component({
   selector: 'app-singerevent-overview',
   templateUrl: './singerevent-overview.component.html',
   styleUrls: ['./singerevent-overview.component.css'],
})
export class SingerEventOverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
   @ViewChild(MatSort, { static: true }) sort: MatSort;
   @ViewChild('filterInput', { static: true }) filterInput: ElementRef;

   // Filter
   filter: string;

   readonly maxFilterLength = 2048;

   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      filterFieldControl: new FormControl(this.filter, [Validators.maxLength(this.maxFilterLength)]),
   });

   // Datatable
   dataSource: SingerEventOverviewDataSource;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = [
      'title',
      'location',
      'ageGroups',
      'maxRegistrants',
      'cost',
      'startDateTime',
      'endDateTime',
      'hasDayCareBefore',
      'hasDayCareAfter',
      'actions',
   ];

   availableLocations: SingerLocation[];

   // Paginator
   pageSize = 15;
   pageIndex = 0;

   constructor(
      public dialog: MatDialog,
      private _singerEventsService: SingerEventsService,
      private _singerLocationService: SingerLocationService,
      private _snackBar: MatSnackBar,
      private _loadingService: LoadingService
   ) {}

   ngOnInit() {
      this.dataSource = new SingerEventOverviewDataSource(this._singerEventsService);
      this._singerLocationService.fetchSingerLocationsData('asc', 'name', 0, 1000, '').subscribe(res => {
         this.availableLocations = res.items as SingerLocation[];
      });
      this.sort.active = 'title';
      this.sort.direction = 'asc';
      this.loadSingerEvents();
   }

   ngAfterViewInit() {
      fromEvent(this.filterInput.nativeElement, 'keyup')
         .pipe(
            debounceTime(400),
            distinctUntilChanged(),
            tap(() => {
               this.paginator.pageIndex = 0;
               this.loadSingerEvents();
            })
         )
         .subscribe();
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
      merge(this.sort.sortChange, this.paginator.page)
         .pipe(
            tap(() => {
               this.pageIndex = this.paginator.pageIndex;
               this.pageSize = this.paginator.pageSize;
               this.loadSingerEvents();
            })
         )
         .subscribe();
      this.dataSource.loading$.subscribe(res => {
         if (res) {
            this._loadingService.show();
         } else {
            this._loadingService.hide();
         }
      });
   }

   private loadSingerEvents() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadSingerEvents(sortDirection, sortColumn, this.pageIndex, this.pageSize, this.filter);
   }

   isMaxRegistrantsExceeded(row: SingerEvent): boolean {
      return row.eventSlots.some(x => x.currentRegistrants > row.maxRegistrants);
   }

   getRegistrantsNumberString(row: SingerEvent): string {
      return !row.registrationOnDailyBasis ? `${row.eventSlots[0].currentRegistrants}/${row.maxRegistrants}` : '';
   }

   manageRegistrations(row: SingerEvent) {
      this.dialog.open(SingerRegistrationsComponent, {
         data: <SingerRegistrationData>{
            event: row,
         },
         width: '60vw',
         maxHeight: '70vh',
      });
   }

   addRegistration(row: SingerEvent) {
      this.dialog.open(SingerEventAdminRegisterComponent, {
         data: <SingerRegistrationData>{
            event: row,
         },
         width: '50vw',
         maxHeight: '70vh',
      });
   }

   addSingerEvent(): void {
      const dialogRef = this.dialog.open(SingerEventDetailsComponent, {
         data: {
            singerEventInstance: null,
            availableLocations: this.availableLocations,
         },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: SingerEvent) => {
         this._singerEventsService.create(result).subscribe(
            () => {
               this.loadSingerEvents();
               this._snackBar.open(`Evenement ${result.title} werd toegevoegd.`, 'OK', { duration: 2000 });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   selectRow(row: SingerEvent): void {
      const dialogRef = this.dialog.open(SingerEventDetailsComponent, {
         data: <SingerEventDetailsFormData>{
            singerEventInstance: row,
            availableLocations: this.availableLocations,
         },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: SingerEvent) => {
         // Update the SingerEvent
         this._singerEventsService.update(result).subscribe(
            () => {
               // Reload SingerEvents
               this.loadSingerEvents();
               this._snackBar.open(`Evenement ${result.title} werd aangepast.`, 'OK', { duration: 2000 });
            },
            err => {
               this.handleApiError(err);
               // TODO: Should be optimised, reloading results should be necessary
               this.loadSingerEvents();
            }
         );
      });

      dialogRef.componentInstance.deleteEvent.subscribe((result: SingerEvent) => {
         this._singerEventsService.deleteSingerEvent(result.id).subscribe(
            res => {
               // Reload SingerEvents
               this.loadSingerEvents();
               this._snackBar.open(`Evenement ${result.title} werd verwijderd.`, 'OK', { duration: 2000 });
            },
            err => {
               this.handleApiError(err);
               // TODO: Should be optimised, reloading results should be necessary
               this.loadSingerEvents();
            }
         );
      });
   }

   handleApiError(err: any) {
      if (typeof err === 'string') {
         this._snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         const messages = [];
         for (const k in err) {
            messages.push(err[k]);
         }
         this._snackBar.open(`⚠ Er zijn fouten opgetreden bij het opslagen:\n${messages.join('\n')}`, 'OK', {
            panelClass: 'multi-line-snackbar',
         });
      }
   }
}
