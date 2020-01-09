import { HttpParams } from '@angular/common/http';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { EventRegisterDetails } from '../../models/singerevent.model';
import { PaginationDTO } from '../../DTOs/pagination.dto';
import {
   EventRegistrationDTO,
   CreateEventSlotRegistrationDTO,
   CreateEventRegistrationDTO,
   UserRegisteredDTO,
   UpdateSingerEventDTO,
   CreateSingerEventDTO,
   SingerEventDTO,
   EventDescriptionDTO,
   SearchEventDTO,
} from '../../DTOs/event-registration.dto';
import { SearchEventData } from 'src/app/modules/dashboard/components/event-search/event-search.component';
import { Injectable } from '@angular/core';

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

   deleteSingerEvent(eventId: string): Observable<any> {
      return this.apiService.delete(`api/event/${eventId}`).pipe(map(res => res));
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

   isUserRegisteredForEvent(
      eventId: string,
      careUserId: string
   ): Observable<UserRegisteredDTO> {
      return this.apiService
         .get(`api/event/${eventId}/isuserregistered/${careUserId}`)
         .pipe(map(res => res));
   }

   getEventRegistrations(
      eventId: string,
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
         .get(`api/event/${eventId}/registrations`, searchParams)
         .pipe(map(res => res));
   }

   getPublicEvents(
      searchEventData: SearchEventData
   ): Observable<EventDescriptionDTO[]> {
      const searchParams = <SearchEventDTO>{
         startDate: searchEventData.startDateTime,
         endDate: searchEventData.endDateTime,
         locationId: searchEventData.locationId,
      };
      return this.apiService
         .post('api/event/search', searchParams)
         .pipe(map(res => res));
   }

   downloadEventSlotRegistartionCsv(
      eventId: string,
      eventSlotId: string
   ): Observable<Blob> {
      return this.apiService.downloadFile(
         `api/Event/${eventId}/registrations/${eventSlotId}/deelnemerslijst.csv`
      );
   }

   getSingleEvent(eventId: string): Observable<SingerEventDTO> {
      return this.apiService.get<SingerEventDTO>(`api/event/${eventId}`);
   }
}
