import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { AdminDatasource } from '../../../services/admin.datasource';
import { AdminUserService } from '../../../services/admin-user.service';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';

@Component({
  selector: 'app-admin-list',
  templateUrl: './admin-list.component.html',
  styleUrls: ['./admin-list.component.css']
})
export class AdminListComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;

   pageSize = 15;
   pageIndex = 0;

    /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
    displayedColumns = [
      'firstName',
      'lastName',
      'email',
      'userName',
   ];
   filter: string;
   dataSource: AdminDatasource;
  constructor(private adminUserService: AdminUserService) { }

  ngOnInit() {
   this.dataSource = new AdminDatasource(this.adminUserService);
   this.sort.active = 'lastName';
   this.sort.direction = 'asc';
   this.loadCareUsers();
  }
   loadCareUsers() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadAdmins(sortDirection, sortColumn, this.pageIndex, this.pageSize, this.filter);
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
