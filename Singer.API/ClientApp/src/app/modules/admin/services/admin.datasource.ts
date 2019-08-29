import { DataSource } from '@angular/cdk/table';


import { BehaviorSubject, Observable } from 'rxjs';

import { AdminUserService } from './admin-user.service';
import { AdminUser } from '../../core/models/adminuser.model';
import { CollectionViewer } from '@angular/cdk/collections';

/**
 * Data source for the Overview view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class AdminDatasource extends DataSource<AdminUser> {
   private adminUsersSubject$ = new BehaviorSubject<AdminUser[]>([]);
   private totalSizeSubject$ = new BehaviorSubject<number>(0);
   private queryCountSubject$ = new BehaviorSubject<number>(0);
   private loadingSubject$ = new BehaviorSubject<boolean>(false);

   public careUsers$ = this.adminUsersSubject$.asObservable();
   public totalSize$ = this.totalSizeSubject$.asObservable();
   public queryCount$ = this.queryCountSubject$.asObservable();
   public loading$ = this.loadingSubject$.asObservable();

   constructor(private adminUserService: AdminUserService) {
      super();
   }

   loadAdmins(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ) {
      this.loadingSubject$.next(true);
      this.adminUserService
         .get(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .subscribe(res => {
            this.adminUsersSubject$.next(res.items as AdminUser[]);
            this.totalSizeSubject$.next(res.totalSize);
            this.queryCountSubject$.next(res.size);
            this.loadingSubject$.next(false);
         });
   }

   connect(collectionViewer: CollectionViewer): Observable<AdminUser[]> {
      return this.adminUsersSubject$.asObservable();
   }
   disconnect() {
      this.adminUsersSubject$.complete();
   }
}
