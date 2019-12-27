import { Injectable } from '@angular/core';
import { SingerAdminEventProxy } from './singer-admin-event.proxy';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RegistrationStatus } from '../../core/models/enum';
import { DaycareLocationDTO } from '../../core/DTOs/daycarelocation.dto';

@Injectable({
   providedIn: 'root',
})
export class SingerAdminEventService {
   constructor(private singerAdminEventProxy: SingerAdminEventProxy) {}

   acceptRegistration(
      eventId: string,
      eventRegistrationId: string
   ): Observable<RegistrationStatus> {
      return this.singerAdminEventProxy
         .acceptRegistration(eventId, eventRegistrationId)
         .pipe(map(res => res));
   }

   rejectRegistration(
      eventId: string,
      eventRegistrationId: string
   ): Observable<RegistrationStatus> {
      return this.singerAdminEventProxy
         .rejectRegistration(eventId, eventRegistrationId)
         .pipe(map(res => res));
   }

   updateDaycareLocation(
      eventId: string,
      eventRegistrationId: string,
      daycareLocationId: string
   ): Observable<DaycareLocationDTO> {
      return this.singerAdminEventProxy
         .updateDaycareLocation(eventId, eventRegistrationId, daycareLocationId)
         .pipe(map(res => res));
   }
}
