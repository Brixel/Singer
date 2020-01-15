import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, Observable } from 'rxjs';
import { CollectionViewer } from '@angular/cdk/collections';
import { CareUser } from 'src/app/modules/core/models/careuser.model';

/**
 * Data source for the Overview view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class OverviewDataSource extends DataSource<CareUser> {

   private careUsersSubject$ = new BehaviorSubject<CareUser[]>([]);
   private totalSizeSubject$ = new BehaviorSubject<number>(0);
   private queryCountSubject$ = new BehaviorSubject<number>(0);
   private loadingSubject$ = new BehaviorSubject<boolean>(false);

   public careUsers$ = this.careUsersSubject$.asObservable();
   public totalSize$ = this.totalSizeSubject$.asObservable();
   public queryCount$ = this.queryCountSubject$.asObservable();
   public loading$ = this.loadingSubject$.asObservable();

   constructor(private careUserService: CareUserService) {
      super();
   }

   loadCareUsers(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number, filter?: string) {

      this.loadingSubject$.next(true);
         this.careUserService.fetchCareUsersData(sortDirection, sortColumn, pageIndex, pageSize, filter).subscribe((res) => {
            this.careUsersSubject$.next(res.items as CareUser[]);
            this.totalSizeSubject$.next(res.totalSize);
            this.queryCountSubject$.next(res.size);
            this.loadingSubject$.next(false);
         });
   }

   connect(collectionViewer: CollectionViewer): Observable<CareUser[]> {
      return this.careUsersSubject$.asObservable();
   }

   disconnect() {
      this.careUsersSubject$.complete();
   }
}
