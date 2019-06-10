import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { CareUsersAPIService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import {
   Overlay,
   NoopScrollStrategy,
   OverlayContainer,
} from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { CareUserDetailsComponent } from '../care-user-details/care-user-details.component';

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
   ];

   ngAfterViewInit() {
      this.dataSource = new OverviewDataSource(
         this.paginator,
         this.sort,
         this.careUsersAPI
      );
   }

   onClick() {
      // const overlayRef = this.overlay.create();
      // const CareUserDetailsPortal = new ComponentPortal(CareUserDetailsComponent);
      // overlayRef.attach(CareUserDetailsPortal);
   }

   applyFilter(filterValue: string) {
      this.dataSource.filter = filterValue.trim().toLowerCase();

      if (this.paginator) {
         this.paginator.firstPage();
      }
   }
}
