import { GenericService } from './generic-service';
import { EventRegistrationLogCareUser, CareUserRegistrationStateChanged,
   CareUserRegistrationLocationChanged } from '../models/event-registration-log.model';
import { EventRegistrationLogCareUserDTO } from '../DTOs/event-registration-log.dto';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError, Subject } from 'rxjs';

@Injectable({
   providedIn: 'root',
})
export class ActionNotificationsService {

   error$: Subject<HttpErrorResponse>;
   constructor(protected httpClient: HttpClient) {   }

   fetch(){
      return this.httpClient
         .get<EventRegistrationLogCareUser[]>('api/actionnotification/pending')
         .pipe(catchError(error => this.handleError(error)));
   }

   toModel(dto: EventRegistrationLogCareUser): EventRegistrationLogCareUser {
      return <EventRegistrationLogCareUser>{
         careUser: dto.careUser,
         id: dto.id,
         legalGuardians: dto.legalGuardians,
         creationDateTimeUTC: dto.creationDateTimeUTC,
         registrationStateChanges: dto.registrationStateChanges.map(reg => <CareUserRegistrationStateChanged>{
            eventRegistrationId: reg.eventRegistrationId,
            eventSlotEndDateTime: reg.eventSlotEndDateTime,
            eventSlotStartDateTime: reg.eventSlotStartDateTime,
            eventTitle: reg.eventTitle,
            newStatus: reg.newStatus
         }),
         registrationLocationChanges: dto.registrationLocationChanges.map(reg => <CareUserRegistrationLocationChanged>{
            eventRegistrationId: reg.eventRegistrationId,
            eventSlotEndDateTime: reg.eventSlotEndDateTime,
            eventSlotStartDateTime: reg.eventSlotStartDateTime,
            eventTitle: reg.eventTitle,
            newLocation: reg.newLocation
         })

      };
   }

   toEditDTO(model: EventRegistrationLogCareUser): null {
      throw new Error('Method not implemented.');
   }
   toCreateDTO(model: EventRegistrationLogCareUser): null {
      throw new Error('Method not implemented.');
   }


   protected handleError(error: HttpErrorResponse) {
      this.error$.next(error);
      return throwError(error.error);
   }
}
