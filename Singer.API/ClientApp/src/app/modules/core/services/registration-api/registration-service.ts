import { Injectable } from '@angular/core';
import { RegistrationOverview } from '../../models/registration.model';
import { RegistrationOverviewDTO } from '../../DTOs/registration.dto';
import { GenericService } from '../generic-service';
import { HttpClient } from '@angular/common/http';
import { RegistrationSearchDTO } from '../../DTOs/registration.dto';

@Injectable({
   providedIn: 'root',
})
export class RegistrationService extends GenericService<
   RegistrationOverview,
   RegistrationOverviewDTO,
   null,
   null,
   RegistrationSearchDTO
> {
   toModel(dto: RegistrationOverviewDTO): RegistrationOverview {
      return <RegistrationOverview>{
         id: dto.id,
         registrationStatus: dto.registrationStatus,
         daycareLocation: dto.daycareLocation,
         endDateTime: dto.endDateTime,
         registrationType: dto.registrationType,
         startDateTime: dto.startDateTime,
         careUserFirstName: dto.careUserFirstName,
         careUserLastName: dto.careUserLastName,
         eventTitle: dto.eventTitle,
      };
   }
   constructor(protected httpClient: HttpClient) {
      super('api/registration');
   }

   toEditDTO(model: RegistrationOverview): null {
      throw new Error('Method not implemented.');
   }
   toCreateDTO(model: RegistrationOverview): null {
      throw new Error('Method not implemented.');
   }

   acceptRegistration(registrationId: string) {
      return this.httpClient.put(`${this.endpoint}/${registrationId}/accept`, '');
   }

   rejectRegistration(registrationId: string) {
      return this.httpClient.put(`${this.endpoint}/${registrationId}/reject`, '');
   }
}
