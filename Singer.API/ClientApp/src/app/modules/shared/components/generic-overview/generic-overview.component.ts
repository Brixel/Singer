import { AfterViewInit, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { GenericModel } from 'src/app/modules/core/models/generics/generic-model';
import { DataSource } from '@angular/cdk/table';
import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { GenericService } from 'src/app/modules/core/services/generic-service';
import { SearchDTOBase } from 'src/app/modules/core/DTOs/base.dto';
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
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput')
   filterInput: ElementRef;

   searchDTO: TSearchDTO;

   displayedColumns = [];

   constructor(private cd: ChangeDetectorRef, public dataSource: TDataSource, private defaultSortColumn: string) {}

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
               this.searchDTO.pageIndex = this.paginator.pageIndex;
               this.searchDTO.pageSize = this.paginator.pageSize;
               this.loadData();
            })
         )
         .subscribe();
   }
   protected loadData() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.searchDTO.text = this.filterInput ? this.filterInput.nativeElement.value : '';
      this.dataSource.load(this.searchDTO);
   }
}
