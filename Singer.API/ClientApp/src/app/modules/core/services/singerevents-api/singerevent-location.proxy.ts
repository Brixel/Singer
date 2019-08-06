import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import {
   UpdateSingerEventLocationDTO,
   CreateSingerEventLocationDTO,
   SingerEventLocationDTO,
} from '../../models/singerevent.model';
import { PaginationDTO } from '../../models/pagination.model';

@Injectable({
   providedIn: 'root',
})
export class SingerEventLocationProxy {
   constructor(private apiService: ApiService) {}

   getSingerEventLocations(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ): Observable<PaginationDTO> {
      const searchParams = new HttpParams()
         .set('sortDirection', sortDirection)
         .set('sortColumn', sortColumn)
         .set('pageIndex', pageIndex.toString())
         .set('pageSize', pageSize.toString())
         .set('filter', filter);
      return this.apiService
         .get('api/event', searchParams)
         .pipe(map(res => res));
   }

   updateSingerEventLocation(
      id: string,
      updateSingerEventLocationDTO: UpdateSingerEventLocationDTO
   ) {
      return this.apiService
         .put(`api/event/${id}`, updateSingerEventLocationDTO)
         .pipe(map(res => res));
   }

   createSingerEventLocation(
      createSingerEventLocationDTO: CreateSingerEventLocationDTO
   ): Observable<SingerEventLocationDTO> {
      return this.apiService
         .post('api/event', createSingerEventLocationDTO)
         .pipe(map(res => res));
   }
}
