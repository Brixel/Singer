import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { CareUsersAPIService, CareUser } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { CareUserDetailsComponent } from '../care-user-details/care-user-details.component';

@Component({
   selector: 'app-overview',
   templateUrl: './overview.component.html',
   styleUrls: ['./overview.component.css'],
})
export class OverviewComponent implements AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;

   // Table Data source
   dataSource: OverviewDataSource;

   // Boolean value to check if the details form should be shown
   showDetailsForm: Boolean;

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

   constructor(
      // Dialog to display form to add and edit care users
      public dialog: MatDialog,
      // API to retrieve user data
      private careUsersAPI: CareUsersAPIService) {}

   ngAfterViewInit() {
      // Load datasource
      this.dataSource = new OverviewDataSource(
         this.paginator,
         this.sort,
         this.careUsersAPI
      );

      // Subscribe to paginator events
      this.paginator.page.subscribe(page => {
         // console.log(page);
         // API pagination calls go here
      });
   }

   selectRow(row: CareUser): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: { careUserInstance: row, isAdding: false },
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         console.log('The dialog was closed');
         console.log(result);
         this.reloadTable();
      });
   }

   addCareUser(): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: { careUserInstance: null, isAdding: true },
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         console.log('The dialog was closed');
         console.log(result);
         this.reloadTable();
      });
   }

   applyFilter(filterValue: string) {
      this.dataSource.filter = filterValue.trim().toLowerCase();

      if (this.paginator) {
         this.paginator.firstPage();
      }
   }

   private reloadTable() {
      this.dataSource.reload();
   }
}
