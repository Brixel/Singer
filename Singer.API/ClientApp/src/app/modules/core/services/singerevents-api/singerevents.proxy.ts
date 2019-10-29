import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import {
   UpdateSingerEventDTO,
   CreateSingerEventDTO,
   SingerEventDTO,
   EventRegistrationDTO,
} from '../../models/singerevent.model';
import { PaginationDTO } from '../../models/pagination.model';

@Injectable({
   providedIn: 'root',
})
export class SingerEventsProxy {
   constructor(private apiService: ApiService) {}

   getSingerEvents(
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

   updateSingerEvents(
      id: string,
      updateSingerEventDTO: UpdateSingerEventDTO
   ) {
      return this.apiService
         .put(`api/event/${id}`, updateSingerEventDTO)
         .pipe(map(res => res));
   }

   createSingerEvents(
      createSingerEventDTO: CreateSingerEventDTO
   ): Observable<SingerEventDTO> {
      return this.apiService
         .post('api/event', createSingerEventDTO)
         .pipe(map(res => res));
   }


   geteventRegistrations(eventId: string): Observable<PaginationDTO> {
      return this.apiService
      .get(`api/event/${eventId}/registrations`).pipe(map(res => res));
   }
}
