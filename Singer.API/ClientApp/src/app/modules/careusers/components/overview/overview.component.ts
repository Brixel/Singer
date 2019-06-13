import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { CareUsersService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { DataSource } from '@angular/cdk/table';
import { merge } from 'rxjs';
import { tap } from 'rxjs/operators';

@Component({
   selector: 'app-overview',
   templateUrl: './overview.component.html',
   styleUrls: ['./overview.component.css'],
})
export class OverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   dataSource: OverviewDataSource;

   pageSize = 15;
   pageIndex = 0;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = [
      //'id',
      'name',
      //'email',
      //'userName',
      'birthDay',
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
      this.loadCareUsers();
   }

   private loadCareUsers(){
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.dataSource.loadCareUsers(sortDirection, sortColumn, this.pageIndex, this.pageSize);
   }

   ngAfterViewInit() {
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
      merge(this.sort.sortChange, this.paginator.page)
          .pipe(
              tap(() => {
                 this.pageIndex = this.paginator.pageIndex;
                 this.pageSize = this.paginator.pageSize;
                  this.loadCareUsers();
              })
          )
          .subscribe();
  }

   applyFilter(filterValue: string) {
      // this.dataSource.filter = filterValue.trim().toLowerCase();

      // if (this.paginator) {
      //    this.paginator.firstPage();
      //  }
    }
}
