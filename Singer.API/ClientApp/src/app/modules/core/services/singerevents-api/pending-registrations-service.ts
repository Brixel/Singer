import { Injectable } from '@angular/core';
import { EventRegistration } from '../../models/singerevent.model';
import { EventRegistrationDTO } from '../../DTOs/event-registration.dto';
import { GenericService } from '../generic-service';
import { HttpClient } from '@angular/common/http';
import { EventSlot } from '../../models/eventslot';

@Injectable({
   providedIn: 'root',
})
export class PendingRegistrationsService extends GenericService<EventRegistration, EventRegistrationDTO, null, null> {
   toModel(dto: EventRegistrationDTO): EventRegistration {
      return <EventRegistration>{
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

   toEditDTO(model: EventRegistration): null {
      throw new Error('Method not implemented.');
   }
   toCreateDTO(model: EventRegistration): null {
      throw new Error('Method not implemented.');
   }
}
