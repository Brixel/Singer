import { AfterViewInit, Component, ViewChild, OnInit, ElementRef } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { CareUsersService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { DataSource } from '@angular/cdk/table';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
   selector: 'app-overview',
   templateUrl: './overview.component.html',
   styleUrls: ['./overview.component.css'],
})
export class OverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;
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
   filter: string;

   constructor(private careUserService: CareUsersService){}

   ngOnInit(){
      this.dataSource = new OverviewDataSource(this.careUserService);
      this.sort.active = 'name';
      this.sort.direction = 'asc';
      this.loadCareUsers();
   }

   private loadCareUsers(){
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadCareUsers(sortDirection, sortColumn, this.pageIndex, this.pageSize, this.filter);
   }

   ngAfterViewInit() {
      fromEvent(this.filterInput.nativeElement, 'keyup')
            .pipe(
                debounceTime(400),
                distinctUntilChanged(),
                tap(() => {
                    this.paginator.pageIndex = 0;
                    this.loadCareUsers();
                })
            )
            .subscribe();
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

}
