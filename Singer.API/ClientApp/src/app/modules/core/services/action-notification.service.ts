import {
   RegistrationLogCareUser,
   CareUserRegistrationStateChanged,
   CareUserRegistrationLocationChanged,
} from '../models/event-registration-log.model';
import { RegistrationLogCareUserDTO } from '../DTOs/event-registration-log.dto';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError, Subject } from 'rxjs';

@Injectable({
   providedIn: 'root',
})
export class ActionNotificationsService {
   error$ = new Subject<HttpErrorResponse>();
   constructor(protected httpClient: HttpClient) {}

   fetch() {
      return this.httpClient
         .get<RegistrationLogCareUserDTO[]>('api/actionnotification/pending')
         .pipe(catchError(error => this.handleError(error)));
   }

   sendEmails() {
      return this.httpClient
         .put('api/actionnotification/sendemail', null)
         .pipe(catchError(error => this.handleError(error)));
   }

   toModel(dto: RegistrationLogCareUserDTO): RegistrationLogCareUser {
      return <RegistrationLogCareUser>{
         careUser: dto.careUser,
         id: dto.id,
         legalGuardians: dto.legalGuardians.map(x => x.name),
         creationDateTimeUTC: dto.creationDateTimeUTC,
         registrationStateChanges: dto.registrationStateChanges.map(
            reg =>
               <CareUserRegistrationStateChanged>{
                  eventRegistrationId: reg.eventRegistrationId,
                  eventSlotEndDateTime: reg.eventSlotEndDateTime,
                  eventSlotStartDateTime: reg.eventSlotStartDateTime,
                  eventTitle: reg.eventTitle,
                  newStatus: reg.newStatus,
               }
         ),
         registrationLocationChanges: dto.registrationLocationChanges.map(
            reg =>
               <CareUserRegistrationLocationChanged>{
                  eventRegistrationId: reg.eventRegistrationId,
                  eventSlotEndDateTime: reg.eventSlotEndDateTime,
                  eventSlotStartDateTime: reg.eventSlotStartDateTime,
                  eventTitle: reg.eventTitle,
                  newLocation: reg.newLocation,
               }
         ),
      };
   }

   toEditDTO(): null {
      throw new Error('Method not implemented.');
   }
   toCreateDTO(): null {
      throw new Error('Method not implemented.');
   }

   protected handleError(error: HttpErrorResponse) {
      this.error$.next(error);
      return throwError(error.error);
   }
}
