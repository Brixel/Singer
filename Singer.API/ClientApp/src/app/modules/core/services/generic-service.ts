import { GenericModel } from '../models/generic-model';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PaginationDTO } from '../DTOs/pagination.dto';

export abstract class GenericService<TModel extends GenericModel, TDTO, TCreateDTO, TUpdateDTO> {
   error$: Subject<HttpErrorResponse>;
   protected abstract httpClient: HttpClient;
   constructor(protected endpoint: string) {}

   fetch(
      sortDirection: string = '0',
      sortColumn: string = 'id',
      pageIndex: number = 1,
      pageSize: number = 15,
      filter: string = ''
   ): Observable<PaginationDTO<TDTO>> {
      const searchParams = new HttpParams()
         .set('sortDirection', sortDirection)
         .set('sortColumn', sortColumn)
         .set('pageIndex', pageIndex.toString())
         .set('pageSize', pageSize.toString())
         .set('filter', filter);
      return this.httpClient
         .get<PaginationDTO<TDTO>>(this.endpoint, {
            params: searchParams,
         })
         .pipe(catchError(error => this.handleError(error)));
   }

   delete(model: TModel): Observable<void> {
      return this.httpClient
         .delete<void>(this.endpoint + '/' + model.id)
         .pipe(catchError(error => this.handleError(error)));
   }

   update(model: TModel): Observable<TDTO> {
      let dto = this.toEditDTO(model);
      return this.httpClient
         .put<TDTO>(`${this.endpoint}/${model.id}`, dto)
         .pipe(catchError(error => this.handleError(error)));
   }

   create(model: TModel): Observable<TDTO> {
      let dto = this.toCreateDTO(model);
      return this.httpClient.post<TDTO>(this.endpoint, dto).pipe(catchError(error => this.handleError(error)));
   }

   protected handleError(error: HttpErrorResponse) {
      this.error$.next(error);
      return throwError(error.error);
   }

   abstract toEditDTO(model: TModel): TUpdateDTO;
   abstract toCreateDTO(model: TModel): TCreateDTO;
   abstract toModel(dto: TDTO): TModel;
}
