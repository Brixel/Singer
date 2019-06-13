import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { CareUsersService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { DataSource } from '@angular/cdk/table';

@Component({
   selector: 'app-overview',
   templateUrl: './overview.component.html',
   styleUrls: ['./overview.component.css'],
})
export class OverviewComponent implements OnInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   dataSource: OverviewDataSource;

   pageSize = 15;
   pageIndex = 0;

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

   constructor(private careUserService: CareUsersService){}

   ngOnInit(){
      this.dataSource = new OverviewDataSource(this.careUserService);
      this.dataSource.loadCareUsers('', '', this.pageIndex, this.pageSize);
   }

   applyFilter(filterValue: string) {
      // this.dataSource.filter = filterValue.trim().toLowerCase();

      // if (this.paginator) {
      //    this.paginator.firstPage();
      //  }
    }
}
