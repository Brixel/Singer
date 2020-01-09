import { GenericService } from './generic-service';
import { EventRegistrationLog } from '../models/event-registration-log.model';
import { EventRegistrationLogDTO } from '../DTOs/event-registration-log.dto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
   providedIn: 'root',
})
export class ActionNotificationsService extends GenericService<EventRegistrationLog, EventRegistrationLogDTO, null, null> {

   constructor(protected httpClient: HttpClient) {
      super('api/actionnotification/pending');
   }

   toModel(dto: EventRegistrationLogDTO): EventRegistrationLog {
      return <EventRegistrationLog>{
         careUser: dto.careUser,
         id: dto.id,
         legalGuardians: dto.legalGuardians,
         creationDateTimeUTC: dto.creationDateTimeUTC,
         eventRegistrationId: dto.eventRegistrationId
      };
   }

   toEditDTO(model: EventRegistrationLog): null {
      throw new Error('Method not implemented.');
   }
   toCreateDTO(model: EventRegistrationLog): null {
      throw new Error('Method not implemented.');
   }
}
