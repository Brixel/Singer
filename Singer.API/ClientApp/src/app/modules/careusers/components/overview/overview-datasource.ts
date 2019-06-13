import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { CareUsersAPIService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { CareUser } from 'src/app/modules/core/services/care-users-api/care-users-api.service';

/**
 * Data source for the Overview view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class OverviewDataSource extends MatTableDataSource<CareUser> {
   data: CareUser[];

   constructor(
      public paginator: MatPaginator,
      public sort: MatSort,
      private careUsersAPI: CareUsersAPIService
   ) {
      super();
      this.data = careUsersAPI.fetchCareUsersData();
   }

   /**
    *  Called when the table is being destroyed. Use this function, to clean up
    * any open connections or free any held resources that were set up during connect.
    */
   disconnect() {}
}
