import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { CareUsersAPIService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';

@Component({
   selector: 'app-overview',
   templateUrl: './overview.component.html',
   styleUrls: ['./overview.component.css'],
})
export class OverviewComponent implements AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   dataSource: OverviewDataSource;
   careUsersAPI: CareUsersAPIService = new CareUsersAPIService();

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = [
      //'id',
      'lastName',
      'firstName',
      //'email',
      //'userName',
      'birthday',
      'caseNumber',
      'ageGroup',
      'isExtern',
      'hasTrajectory',
      'hasNormalDayCare',
      'hasVacationDayCare',
      'hasResources',
      'edit',
   ];

   ngAfterViewInit() {
      this.dataSource = new OverviewDataSource(
         this.paginator,
         this.sort,
         this.careUsersAPI
      );
   }
}
