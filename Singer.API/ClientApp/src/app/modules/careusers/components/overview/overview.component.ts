import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { CareUsersAPIService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { CareUserDetailsComponent } from '../care-user-details/care-user-details.component';

@Component({
   selector: 'app-overview',
   templateUrl: './overview.component.html',
   styleUrls: ['./overview.component.css'],
})
export class OverviewComponent implements AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild(CareUserDetailsComponent)
   careUserDetailsForm: CareUserDetailsComponent;

   // Table Data source
   dataSource: OverviewDataSource;

   // Boolean value to check if the details form should be shown
   showDetailsForm: Boolean;

   // API to retrieve user data
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
      // Load datasource
      this.dataSource = new OverviewDataSource(
         this.paginator,
         this.sort,
         this.careUsersAPI
      );

      // Subscribe to paginator events
      this.paginator.page.subscribe((page) => {
         // console.log(page);
         // API pagination calls go here
      });

      //Subscribe to details form events
      this.careUserDetailsForm.closeFormEvent.subscribe((isFormClosedEvent) => {
         this.showDetailsForm = !isFormClosedEvent;
      })
   }

   selectRow(row) {

      // Show careUserDetailsForm
      this.showDetailsForm = true;

      // pass selected careUser to details form (row contains a careUser object)
      this.careUserDetailsForm.showCareUser(row);
   }

   addCareUser() {
      // Show careUserDetailsForm
      this.showDetailsForm = true;

      this.careUserDetailsForm.addNewCareUser();
   }

   applyFilter(filterValue: string) {
      this.dataSource.filter = filterValue.trim().toLowerCase();

      if (this.paginator) {
         this.paginator.firstPage();
      }
   }
}
