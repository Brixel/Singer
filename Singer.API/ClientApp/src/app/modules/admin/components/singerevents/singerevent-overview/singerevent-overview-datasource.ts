import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, Observable } from 'rxjs';
import { CollectionViewer } from '@angular/cdk/collections';
import {
   SingerEventDTO,
   SingerEvent,
} from 'src/app/modules/core/models/singerevent.model';

/**
 * Data source for the EventsOverview view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class SingerEventOverviewDataSource extends DataSource<SingerEvent> {
   private singerEventsSubject$ = new BehaviorSubject<SingerEvent[]>([]);
   private totalSizeSubject$ = new BehaviorSubject<number>(0);
   private queryCountSubject$ = new BehaviorSubject<number>(0);
   private loadingSubject$ = new BehaviorSubject<boolean>(false);

   public singerEvents$ = this.singerEventsSubject$.asObservable();
   public totalSize$ = this.totalSizeSubject$.asObservable();
   public queryCount$ = this.queryCountSubject$.asObservable();
   public loading$ = this.loadingSubject$.asObservable();

   constructor(private singerEventsService: SingerEventsService) {
      super();
   }

   loadSingerEvents(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ) {
      this.loadingSubject$.next(true);
      this.singerEventsService
         .fetchSingerEventsData(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .subscribe(res => {
            this.singerEventsSubject$.next(res.items as SingerEvent[]);
            this.totalSizeSubject$.next(res.totalSize);
            this.queryCountSubject$.next(res.size);
            this.loadingSubject$.next(false);
         });
   }

   connect(collectionViewer: CollectionViewer): Observable<SingerEvent[]> {
      return this.singerEventsSubject$.asObservable();
   }
   disconnect() {
      this.singerEventsSubject$.complete();
   }
}
