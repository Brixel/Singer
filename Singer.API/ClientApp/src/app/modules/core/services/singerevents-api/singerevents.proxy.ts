import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import {
   UpdateSingerEventDTO,
   CreateSingerEventDTO,
   SingerEventDTO,
   EventRelevantCareUserDTO,
   EventRegisterDetails,
} from '../../models/singerevent.model';
import { PaginationDTO } from '../../models/pagination.model';
import {
   EventRegistrationDTO,
   CreateEventSlotRegistrationDTO,
   CreateEventRegistrationDTO,
} from '../../models/event-registration';

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

   updateSingerEvents(id: string, updateSingerEventDTO: UpdateSingerEventDTO) {
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

   getEventRegisterDetails(eventId: string): Observable<EventRegisterDetails> {
      return this.apiService
         .get(`api/event/${eventId}/geteventregisterdetails`)
         .pipe(map(res => res));
   }

   registerCareUserOnEvent(
      eventId: string,
      dto: CreateEventRegistrationDTO
   ): Observable<EventRegistrationDTO[]> {
      return this.apiService
         .post(`api/event/${eventId}/registrations`, dto)
         .pipe(map(res => res));
   }

   registerCareUserOnEventSlot(
      eventId: string,
      eventSlotId: string,
      dto: CreateEventSlotRegistrationDTO
   ): Observable<EventRegistrationDTO> {
      return this.apiService
         .post(
            `api/event/${eventId}/eventslot/${eventSlotId}/registrations`,
            dto
         )
         .pipe(map(res => res));
   }
}
