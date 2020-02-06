import { Injectable } from '@angular/core';
import { Registration } from '../../models/singerevent.model';
import { RegistrationDTO } from '../../DTOs/event-registration.dto';
import { GenericService } from '../generic-service';
import { HttpClient } from '@angular/common/http';
import { EventSlot } from '../../models/eventslot';

@Injectable({
   providedIn: 'root',
})
export class PendingRegistrationsService extends GenericService<Registration, RegistrationDTO, null, null> {
   toModel(dto: RegistrationDTO): Registration {
      return <Registration>{
         careUser: dto.careUser,
         eventDescription: dto.eventDescription,
         eventSlot: <EventSlot>{
            id: dto.eventSlot.id,
            endDateTime: dto.eventSlot.endDateTime,
            startDateTime: dto.eventSlot.startDateTime,
            currentRegistrants: dto.eventSlot.currentRegistrants,
         },
         id: dto.id,
         status: dto.status,
      };
   }
   constructor(protected httpClient: HttpClient) {
      super('api/event/registrations/status/pending');
   }

   toEditDTO(model: Registration): null {
      throw new Error('Method not implemented.');
   }
   toCreateDTO(model: Registration): null {
      throw new Error('Method not implemented.');
   }
}
