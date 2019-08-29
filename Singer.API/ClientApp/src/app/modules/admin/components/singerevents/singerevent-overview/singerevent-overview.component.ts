import {
   AfterViewInit,
   Component,
   ViewChild,
   OnInit,
   ElementRef,
} from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { SingerEventOverviewDataSource } from './singerevent-overview-datasource';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { SingerEventDetailsComponent, SingerEventDetailsFormData } from '../singerevent-details/singerevent-details.component';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { SingerEvent, SingerEventLocation } from 'src/app/modules/core/models/singerevent.model';
import { SingerEventLocationService } from 'src/app/modules/core/services/singerevents-api/singerevent-location.service';

@Component({
   selector: 'app-singerevent-overview',
   templateUrl: './singerevent-overview.component.html',
   styleUrls: ['./singerevent-overview.component.css'],
})
export class SingerEventOverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;
   dataSource: SingerEventOverviewDataSource;

   filter: string;

   pageSize = 15;
   pageIndex = 0;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = [
      'title',
      'location',
      'ageGroups',
      'maxRegistrants',
      'cost',
      'startDate',
      'endDate',
      'hasDayCareBefore',
      'hasDayCareAfter',
   ];
   availableLocations: SingerEventLocation[];

   constructor(
      public dialog: MatDialog,
      private singerEventsService: SingerEventsService,
      private singerEventLocationService: SingerEventLocationService
   ) {}

   ngOnInit() {
      this.dataSource = new SingerEventOverviewDataSource(
         this.singerEventsService
      );
      this.singerEventLocationService.fetchSingerEventsData('asc', 'name', 0, 1000, '').subscribe(res =>{
         this.availableLocations =  res.items as  SingerEventLocation[];
         }
      );
      this.sort.active = 'title';
      this.sort.direction = 'asc';
      this.loadSingerEvents();
   }

   selectRow(row: SingerEvent): void {
      const dialogRef = this.dialog.open(SingerEventDetailsComponent, {
         data: <SingerEventDetailsFormData>{
            singerEventInstance: row,
            isAdding: false,
            availableLocations: this.availableLocations },
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: SingerEvent) => {
         // Update the SingerEvent
         this.singerEventsService.updateSingerEvent(result).subscribe(res => {
            // Reload SingerEvents
            this.loadSingerEvents();
         });
      });
   }

   addSingerEvent(): void {
      const dialogRef = this.dialog.open(SingerEventDetailsComponent, {
         data: { singerEventInstance: null, isAdding: true, availableLocations:  this.availableLocations },
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: SingerEvent) => {
         this.singerEventsService.createSingerEvent(result).subscribe(res => {
            this.loadSingerEvents();
         });
      });
   }

   // Returns true if the max number of registrants for the event have been exceeded
   isMaxRegistrantsExceeded(row: SingerEvent):boolean {
      return row.currentRegistrants > row.maxRegistrants;
   }

   private loadSingerEvents() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadSingerEvents(
         sortDirection,
         sortColumn,
         this.pageIndex,
         this.pageSize,
         this.filter
      );
   }

   ngAfterViewInit() {
      fromEvent(this.filterInput.nativeElement, 'keyup')
         .pipe(
            debounceTime(400),
            distinctUntilChanged(),
            tap(() => {
               this.paginator.pageIndex = 1;
               this.loadSingerEvents();
            })
         )
         .subscribe();
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 1));
      merge(this.sort.sortChange, this.paginator.page)
         .pipe(
            tap(() => {
               this.pageIndex = this.paginator.pageIndex;
               this.pageSize = this.paginator.pageSize;
               this.loadSingerEvents();
            })
         )
         .subscribe();
   }
}
