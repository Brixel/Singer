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
//import { SingerEventDetailsComponent } from '../events-details/events-details.component';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';

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
   pageIndex = 1;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = [
      'name',
      'location',
      'ageGroups',
      'size',
      'price',
      'startDate',
      'endDate',
      'hasDayCareBefore',
      'hasDayCareAfter',
   ];

   constructor(
      public dialog: MatDialog,
      private singerEventsService: SingerEventsService
   ) {}

   ngOnInit() {
      this.dataSource = new SingerEventOverviewDataSource(this.singerEventsService);
      this.sort.active = 'name';
      this.sort.direction = 'asc';
      this.loadSingerEvents();
   }

   selectRow(row: SingerEvent): void {
      // const dialogRef = this.dialog.open(SingerEventsDetailsComponent, {
      //    data: { singerEventInstance: row, isAdding: false },
      // });

      // dialogRef.componentInstance.submitEvent.subscribe((result: SingerEvent) => {
      //    // Update the SingerEvent
      //    this.singerEventsService.updateSingerEvent(result).subscribe(res => {
      //       // Reload SingerEvents
      //       this.loadSingerEvents();
      //    });
      // });
   }

   addSingerEvent(): void {
      // const dialogRef = this.dialog.open(SingerEventsDetailsComponent, {
      //    data: { singerEventInstance: null, isAdding: true },
      // });

      // dialogRef.componentInstance.submitEvent.subscribe((result: SingerEvent) => {
      //    this.singerEventsService.createSingerEvent(result).subscribe(res => {
      //       this.loadSingerEvents();
      //    });
      // });
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
