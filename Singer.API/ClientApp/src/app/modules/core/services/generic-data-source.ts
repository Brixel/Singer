import { DataSource } from '@angular/cdk/table';
import { GenericModel } from '../models/generics/generic-model';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { CollectionViewer } from '@angular/cdk/collections';
import { GenericService } from './generic-service';
import { SearchDTOBase } from '../DTOs/base.dto';

export abstract class GenericDataSource<
   TModel extends GenericModel,
   TDTO,
   TUpdateDTO,
   TCreateDTO,
   TService extends GenericService<TModel, TDTO, TUpdateDTO, TCreateDTO, TSearchDTO>,
   TSearchDTO extends SearchDTOBase
> extends DataSource<TModel> {
   protected modelsSubject$ = new BehaviorSubject<TModel[]>([]);
   protected totalSizeSubject$ = new BehaviorSubject<number>(0);
   protected queryCountSubject$ = new BehaviorSubject<number>(0);
   protected loadingSubject$ = new BehaviorSubject<boolean>(false);

   public models$ = this.modelsSubject$.asObservable();
   public totalSize$ = this.totalSizeSubject$.asObservable();
   public queryCount$ = this.queryCountSubject$.asObservable();
   public loading$ = this.loadingSubject$.asObservable();
   public error$ = new Subject<any>();

   constructor(protected _dataService: TService) {
      super();
   }

   public load(searchDTO: TSearchDTO) {
      this.loadingSubject$.next(true);
      this._dataService.advancedSearch(searchDTO).subscribe(
         res => {
            this.modelsSubject$.next(res.items.map(x => this._dataService.toModel(x)));
            this.totalSizeSubject$.next(res.totalSize);
            this.queryCountSubject$.next(res.size);
            this.loadingSubject$.next(false);
         },
         err => {
            this.error$.next(err);
         }
      );
   }

   connect(_collectionViewer: CollectionViewer): Observable<TModel[]> {
      return this.modelsSubject$.asObservable();
   }
   disconnect() {
      this.modelsSubject$.complete();
   }
}
