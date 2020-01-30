import {
   AfterViewInit,
   ViewChild,
   ElementRef,
   ChangeDetectorRef,
} from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { GenericModel } from 'src/app/modules/core/models/generics/generic-model';
import { DataSource } from '@angular/cdk/table';
import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
export abstract class GenericOverviewComponent<
   TModel extends GenericModel,
   TDTO,
   TDataSource extends GenericDataSource<TModel, TDTO>
> implements AfterViewInit {
   @ViewChild(MatPaginator)
   paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput')
   filterInput: ElementRef;

   pageSize = 15;
   pageIndex = 0;
   filter: string;

   displayedColumns = [];

   constructor(
      private cd: ChangeDetectorRef,
      public dataSource: TDataSource,
      private defaultSortColumn: string
   ) {}

   ngOnInit() {
      this.displayedColumns.push('actions');
      this.myOnInit();
   }
   abstract myOnInit();
   ngAfterViewInit(): void {
      this.sort.active = this.defaultSortColumn;
      this.sort.direction = 'asc';
      this.cd.detectChanges();
      this.loadData();
      if (this.filterInput) {
         fromEvent(this.filterInput.nativeElement, 'keyup')
            .pipe(
               debounceTime(400),
               distinctUntilChanged(),
               tap(() => {
                  this.paginator.pageIndex = 0;
                  this.loadData();
               })
            )
            .subscribe();
      }
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
      merge(this.sort.sortChange, this.paginator.page)
         .pipe(
            tap(() => {
               this.pageIndex = this.paginator.pageIndex;
               this.pageSize = this.paginator.pageSize;
               this.loadData();
            })
         )
         .subscribe();
   }
   protected loadData() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput
         ? this.filterInput.nativeElement.value
         : '';
      this.dataSource.load(
         sortDirection,
         sortColumn,
         this.pageIndex,
         this.pageSize,
         this.filter
      );
   }
}
