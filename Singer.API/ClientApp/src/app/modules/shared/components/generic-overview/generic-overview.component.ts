import { AfterViewInit, ViewChild, ElementRef, ChangeDetectorRef, Directive } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { GenericModel } from 'src/app/modules/core/models/generics/generic-model';
import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { GenericService } from 'src/app/modules/core/services/generic-service';
import { SearchDTOBase } from 'src/app/modules/core/DTOs/base.dto';
import { SortDirection } from 'src/app/modules/core/enums/sort-direction';
@Directive()
export abstract class GenericOverviewComponent<
   TModel extends GenericModel,
   TDTO,
   TUpdateDTO,
   TCreateDTO,
   TService extends GenericService<TModel, TDTO, TUpdateDTO, TCreateDTO, TSearchDTO>,
   TDataSource extends GenericDataSource<TModel, TDTO, TUpdateDTO, TCreateDTO, TService, TSearchDTO>,
   TSearchDTO extends SearchDTOBase
> implements AfterViewInit {
   @ViewChild(MatPaginator)
   paginator: MatPaginator;
   @ViewChild(MatSort, { static: true }) sort: MatSort;
   @ViewChild('filterInput', /* TODO: add static flag */ {})
   filterInput: ElementRef;

   searchDTO: TSearchDTO;

   displayedColumns = [];

   constructor(
      private _cd: ChangeDetectorRef,
      public dataSource: TDataSource,
      private _defaultSortColumn: string,
      private _sortDirection = SortDirection.Ascending
   ) {}
   ngAfterViewInit(): void {
      this.sort.sort({
         disableClear: false,
         start: this._sortDirection == SortDirection.Ascending ? 'asc' : 'desc',
         id: this._defaultSortColumn,
      });

      if (this.sort.sortables.size > 0) {
         //WORKAROUND: https://github.com/angular/components/issues/15715
         const sortHeader = this.sort.sortables.get(this._defaultSortColumn);
         sortHeader['_setAnimationTransitionState']({ toState: 'active' });
      }
      this._cd.detectChanges();
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
               this.searchDTO.pageIndex = this.paginator.pageIndex;
               this.searchDTO.pageSize = this.paginator.pageSize;
               this.searchDTO.sortColumn = this.sort.active;
               this.searchDTO.sortDirection =
                  this.sort.direction === 'asc' ? SortDirection.Ascending : SortDirection.Descending;
               this.loadData();
            })
         )
         .subscribe();
   }
   protected loadData() {
      if (this.searchDTO !== undefined)
         this.searchDTO.text = this.filterInput ? this.filterInput.nativeElement.value : '';
      this.dataSource.load(this.searchDTO);
   }
}
