import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, Observable } from 'rxjs';
import { CollectionViewer } from '@angular/cdk/collections';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';

/** Data source for the EventsOverview view.*/
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
         .fetch(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .subscribe(res => {
            let models = res.items.map(x =>
               this.singerEventsService.toModel(x)
            );
            this.singerEventsSubject$.next(models);
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
