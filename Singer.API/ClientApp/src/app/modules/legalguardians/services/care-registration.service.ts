import { EventRegistrationTypes } from '../components/register-care-wizard/register-care-wizard.component';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Injector, Inject, Injectable } from '@angular/core';
import { CareRegistrationProxy } from './care-registration.proxy';
import { CareRegistrationResultDTO } from '../../core/DTOs/care-registration-result.dto';
import { CreateCareRegistrationDTO } from '../../core/DTOs/create-care-registration.dto';

@Injectable()
export class CareRegistrationService {
   constructor(private proxy: CareRegistrationProxy) {}
   createCareRegistration(
      eventRegistrationType: EventRegistrationTypes,
      selectedDates: Date[],
      careUserIds: string[]
   ): Observable<CareRegistrationResultDTO> {
      const orderedDates = selectedDates.sort((a, b) => a.getTime() - b.getTime());
      const startDate = orderedDates[0];
      const endDate = orderedDates[orderedDates.length - 1];
      const createCareRegistrationDTO = <CreateCareRegistrationDTO>{
         careUserIds,
         eventRegistrationType,
         startDateTime: startDate,
         endDateTime: endDate,
      };
      return this.proxy.createCareRegistration(createCareRegistrationDTO).pipe(map(res => res));
   }
}
