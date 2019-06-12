import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { CareUsersAPIService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { CareUserDetailsComponent } from "../care-user-details/care-user-details.component";

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

   constructor(public dialog: MatDialog) {}

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

   selectRow(row): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         width: '1000px',
         data: row,
      });

      dialogRef.afterClosed().subscribe(result => {
         console.log('The dialog was closed');
         console.log(result);
      });
   }

   addCareUser(): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         width: '1000px',
         data: null,
      });

      dialogRef.afterClosed().subscribe(result => {
         console.log('The dialog was closed');
         console.log(result);
      });
   }

   applyFilter(filterValue: string) {
      this.dataSource.filter = filterValue.trim().toLowerCase();

      if (this.paginator) {
         this.paginator.firstPage();
      }
   }
}
