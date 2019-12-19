import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RegistrationStatus } from '../../core/models/enum';
import { DaycareLocationDTO } from '../../core/DTOs/daycarelocation.dto';

@Injectable({
   providedIn: 'root',
})
export class SingerAdminEventProxy {
   constructor(private apiService: ApiService) {}

   acceptRegistration(
      eventId: string,
      eventRegistration: string
   ): Observable<RegistrationStatus> {
      return this.apiService
         .post(`api/event/${eventId}/registrations/${eventRegistration}/accept`)
         .pipe(map(res => res));
   }

   rejectRegistration(
      eventId: string,
      eventRegistration: string
   ): Observable<RegistrationStatus> {
      return this.apiService
         .post(`api/event/${eventId}/registrations/${eventRegistration}/reject`)
         .pipe(map(res => res));
   }

   updateDaycareLocation(
      eventId: string,
      eventRegistrationId: string,
      daycareLocationId: string
   ): Observable<DaycareLocationDTO> {
      return this.apiService
         .put(
            `api/event/${eventId}/registrations/${eventRegistrationId}/location`,
            daycareLocationId
         )
         .pipe(map(res => res));
   }
}
