import { DataSource } from '@angular/cdk/table';
import { GenericModel } from '../models/generic-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { CollectionViewer } from '@angular/cdk/collections';
import { GenericService } from './generic-service';

export abstract class GenericDataSource<
   TModel extends GenericModel,
   TDTO
> extends DataSource<TModel> {
   protected modelsSubject$ = new BehaviorSubject<TModel[]>([]);
   protected totalSizeSubject$ = new BehaviorSubject<number>(0);
   protected queryCountSubject$ = new BehaviorSubject<number>(0);
   protected loadingSubject$ = new BehaviorSubject<boolean>(false);

   public models$ = this.modelsSubject$.asObservable();
   public totalSize$ = this.totalSizeSubject$.asObservable();
   public queryCount$ = this.queryCountSubject$.asObservable();
   public loading$ = this.loadingSubject$.asObservable();

   constructor(protected _dataService: GenericService<TModel, TDTO, any, any>) {
      super();
   }

   public load(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex: number = 0,
      pageSize?: number,
      filter?: string
   ) {
      this.loadingSubject$.next(true);
      sortDirection = sortDirection === 'asc' ? '0' : '1';
      this._dataService
         .fetch(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .subscribe(res => {
            this.modelsSubject$.next(
               res.items.map(x => this._dataService.toModel(x))
            );
            this.totalSizeSubject$.next(res.totalSize);
            this.queryCountSubject$.next(res.size);
            this.loadingSubject$.next(false);
         });
   }

   connect(_collectionViewer: CollectionViewer): Observable<TModel[]> {
      return this.modelsSubject$.asObservable();
   }
   disconnect() {
      this.modelsSubject$.complete();
   }
}
