import { HttpParams } from '@angular/common/http';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { EventRegisterDetails, EventFilterParameters } from '../../models/singerevent.model';
import { PaginationDTO } from '../../DTOs/pagination.dto';
import {
   CreateEventSlotRegistrationDTO,
   UserRegisteredDTO,
   UpdateSingerEventDTO,
   CreateSingerEventDTO,
   SingerEventDTO,
   EventDescriptionDTO,
   EventFilterParametersDTO,
} from '../../DTOs/event-registration.dto';
import { Injectable } from '@angular/core';
import { CreateRegistrationDTO, RegistrationDTO } from '../../DTOs/registration.dto';

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
      return this.apiService.get('api/event', searchParams).pipe(map(res => res));
   }

   updateSingerEvents(id: string, updateSingerEventDTO: UpdateSingerEventDTO) {
      return this.apiService.put(`api/event/${id}`, updateSingerEventDTO).pipe(map(res => res));
   }

   createSingerEvents(createSingerEventDTO: CreateSingerEventDTO): Observable<SingerEventDTO> {
      return this.apiService.post('api/event', createSingerEventDTO).pipe(map(res => res));
   }

   deleteSingerEvent(eventId: string): Observable<any> {
      return this.apiService.delete(`api/event/${eventId}`).pipe(map(res => res));
   }

   getEventRegisterDetails(eventId: string): Observable<EventRegisterDetails> {
      return this.apiService.get(`api/event/${eventId}/geteventregisterdetails`).pipe(map(res => res));
   }

   registerCareUserOnEvent(eventId: string, dto: CreateRegistrationDTO): Observable<RegistrationDTO[]> {
      return this.apiService.post(`api/event/${eventId}/registrations`, dto).pipe(map(res => res));
   }

   registerCareUserOnEventSlot(
      eventId: string,
      eventSlotId: string,
      dto: CreateEventSlotRegistrationDTO
   ): Observable<RegistrationDTO> {
      return this.apiService
         .post(`api/event/${eventId}/eventslot/${eventSlotId}/registrations`, dto)
         .pipe(map(res => res));
   }

   isUserRegisteredForEvent(eventId: string, careUserId: string): Observable<UserRegisteredDTO> {
      return this.apiService.get(`api/event/${eventId}/isuserregistered/${careUserId}`).pipe(map(res => res));
   }

   getRegistrations(
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
      return this.apiService.get(`api/event/${eventId}/registrations`, searchParams).pipe(map(res => res));
   }

   getPublicEvents(eventFilterData: EventFilterParameters): Observable<EventDescriptionDTO[]> {
      const filterParams = <EventFilterParametersDTO>{
         startDate: eventFilterData.startDate,
         endDate: eventFilterData.endDate,
         locationId: eventFilterData.locationId,
         allowedAgeGroups: eventFilterData.allowedAgeGroups,
         text: eventFilterData.text,
      };
      return this.apiService.post('api/event/search', filterParams).pipe(map(res => res));
   }

   downloadEventSlotRegistartionCsv(eventId: string, eventSlotId: string): Observable<Blob> {
      return this.apiService.downloadFile(`api/Event/${eventId}/registrations/${eventSlotId}/deelnemerslijst.csv`);
   }

   downloadEventSlotRegistartionXlsx(eventId: string, eventSlotId: string): Observable<Blob> {
      return this.apiService.downloadFile(`api/Event/${eventId}/registrations/${eventSlotId}/deelnemerslijst.xlsx`);
   }

   getSingleEvent(eventId: string): Observable<SingerEventDTO> {
      return this.apiService.get<SingerEventDTO>(`api/event/${eventId}`);
   }
}
