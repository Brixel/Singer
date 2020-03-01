import { AfterViewInit, Component, ViewChild, OnInit, ElementRef } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { CareUserOverviewDataSource } from './care-user-overview-datasource';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { CareUserDetailsComponent } from '../careuser-details/care-user-details.component';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
   selector: 'app-care-user-overview',
   templateUrl: './care-user-overview.component.html',
   styleUrls: ['./care-user-overview.component.css'],
})
export class CareUserOverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;

   // Filter
   filter: string;

   readonly maxFilterLength = 2048;

   formControlGroup: FormGroup = new FormGroup({
      filterFieldControl: new FormControl(this.filter, [Validators.maxLength(this.maxFilterLength)]),
   });

   // Datatable
   dataSource: CareUserOverviewDataSource;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = ['firstName', 'lastName', 'birthDay', 'ageGroup', 'isExtern', 'hasTrajectory'];

   // Paginator
   pageSize = 15;
   pageIndex = 0;

   constructor(
      public dialog: MatDialog,
      private _careUserService: CareUserService,
      private _snackBar: MatSnackBar,
      private _loadingService: LoadingService
   ) {}

   ngOnInit() {
      this.dataSource = new CareUserOverviewDataSource(this._careUserService);
      this.sort.active = 'firstName';
      this.sort.direction = 'asc';
      this.loadCareUsers();
   }

   ngAfterViewInit() {
      fromEvent(this.filterInput.nativeElement, 'keyup')
         .pipe(
            debounceTime(400),
            distinctUntilChanged(),
            tap(() => {
               this.paginator.pageIndex = 0;
               this.loadCareUsers();
            })
         )
         .subscribe();
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
      merge(this.sort.sortChange, this.paginator.page)
         .pipe(
            tap(() => {
               this.pageIndex = this.paginator.pageIndex;
               this.pageSize = this.paginator.pageSize;
               this.loadCareUsers();
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

   private loadCareUsers() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadCareUsers(sortDirection, sortColumn, this.pageIndex, this.pageSize, this.filter);
   }

   addCareUser(): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: {
            careUserInstance: null,
            displayLinkedUserFields: false,
         },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         this._careUserService.createCareUser(result).subscribe(
            _ => {
               this.loadCareUsers();
               this._snackBar.open(`Gebruiker ${result.firstName} ${result.lastName} werd toegevoegd.`, 'OK', {
                  duration: 2000,
               });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   selectRow(row: CareUser): void {
      //Dereference row to avoid updating row in overview when API might refuse the update
      const deRefRow = { ...row };
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: {
            careUserInstance: deRefRow,
            displayLinkedUserFields: true,
         },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         // Update the Careuser
         this._careUserService.updateUser(result).subscribe(
            () => {
               // Reload Careusers
               this.loadCareUsers();
               this._snackBar.open(`Gebruiker ${result.firstName} ${result.lastName} werd aangepast.`, 'OK', {
                  duration: 2000,
               });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   handleApiError(err: any) {
      if (typeof err === 'string') {
         this._snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         let messages = [];
         for (var k in err) {
            messages.push(err[k]);
         }
         this._snackBar.open(`⚠ Er zijn fouten opgetreden bij het opslaan:\n${messages.join('\n')}`, 'OK', {
            panelClass: 'multi-line-snackbar',
         });
      }
   }
}
