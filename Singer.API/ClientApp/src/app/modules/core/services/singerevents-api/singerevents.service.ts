import { Injectable } from '@angular/core';
import {
   SingerEvent,
   UpdateSingerEventDTO,
   CreateSingerEventDTO,
   EventRepeatSettingsDTO,
   EventDescription,
} from '../../models/singerevent.model';
import { Observable } from 'rxjs';
import { SingerEventsProxy } from './singerevents.proxy';
import { map } from 'rxjs/operators';
import { PaginationDTO } from '../../models/pagination.model';
import {
   CreateEventSlotRegistrationDTO,
   CreateEventRegistrationDTO,
   UserRegisteredDTO,
} from '../../models/event-registration.model';
import { TimeUnit, RepeatType } from '../../models/enum';
import { SearchEventData } from 'src/app/modules/dashboard/components/event-search/event-search.component';

@Injectable({
   providedIn: 'root',
})
export class SingerEventsService {
   constructor(private singerEventsProxy: SingerEventsProxy) {}

   fetchSingerEventsData(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ): Observable<PaginationDTO> {
      return this.singerEventsProxy
         .getSingerEvents(
            sortDirection,
            sortColumn,
            pageIndex,
            pageSize,
            filter
         )
         .pipe(map(res => res));
   }

   updateSingerEvent(updateSingerEvent: SingerEvent) {
      const updateSingerEventDTO = <UpdateSingerEventDTO>{
         id: updateSingerEvent.id,
         title: updateSingerEvent.title,
         description: updateSingerEvent.description,
         locationId: updateSingerEvent.location.id,
         allowedAgeGroups: updateSingerEvent.allowedAgeGroups,
         maxRegistrants: updateSingerEvent.maxRegistrants,
         cost: updateSingerEvent.cost,
         startRegistrationDateTime: updateSingerEvent.startRegistrationDateTime,
         endRegistrationDateTime: updateSingerEvent.endRegistrationDateTime,
         finalCancellationDateTime: updateSingerEvent.finalCancellationDateTime,
         registrationOnDailyBasis: updateSingerEvent.registrationOnDailyBasis,
         startDateTime: updateSingerEvent.startDateTime,
         endDateTime: updateSingerEvent.endDateTime,
         hasDayCareBefore: updateSingerEvent.hasDayCareBefore,
         dayCareBeforeStartDateTime:
            updateSingerEvent.dayCareBeforeStartDateTime,
         hasDayCareAfter: updateSingerEvent.hasDayCareAfter,
         dayCareAfterEndDateTime: updateSingerEvent.dayCareAfterEndDateTime,
      };
      return this.singerEventsProxy
         .updateSingerEvents(updateSingerEventDTO.id, updateSingerEventDTO)
         .pipe(map(res => res));
   }

   createSingerEvent(createSingerEvent: SingerEvent) {
      const endDateTime = new Date(createSingerEvent.startDateTime);
      endDateTime.setHours(createSingerEvent.endDateTime.getHours());
      endDateTime.setMinutes(createSingerEvent.endDateTime.getMinutes());
      const createSingerEventDTO = <CreateSingerEventDTO>{
         title: createSingerEvent.title,
         description: createSingerEvent.description,
         locationId: createSingerEvent.location.id,
         allowedAgeGroups: createSingerEvent.allowedAgeGroups,
         maxRegistrants: createSingerEvent.maxRegistrants,
         cost: createSingerEvent.cost,
         startRegistrationDateTime: createSingerEvent.startRegistrationDateTime,
         endRegistrationDateTime: createSingerEvent.endRegistrationDateTime,
         finalCancellationDateTime: createSingerEvent.finalCancellationDateTime,
         registrationOnDailyBasis: createSingerEvent.registrationOnDailyBasis,
         startDateTime: createSingerEvent.startDateTime,
         endDateTime: endDateTime,
         hasDayCareBefore: createSingerEvent.hasDayCareBefore,
         dayCareBeforeStartDateTime:
            createSingerEvent.dayCareBeforeStartDateTime,
         hasDayCareAfter: createSingerEvent.hasDayCareAfter,
         dayCareAfterEndDateTime: createSingerEvent.dayCareAfterEndDateTime,
         // TODO: These are default repeat settings to ensure events overlapping multiple days will be created with daily timeslots
         repeatSettings: <EventRepeatSettingsDTO>{
            interval: 1,
            intervalUnit: TimeUnit.Day,
            repeatType: RepeatType.OnDate,
            stopRepeatDate: createSingerEvent.endDateTime,
         },
      };
      return this.singerEventsProxy
         .createSingerEvents(createSingerEventDTO)
         .pipe(map(res => res));
   }

   getEventRegisterDetails(eventId: string) {
      return this.singerEventsProxy.getEventRegisterDetails(eventId);
   }

   registerCareUserOnEvent(eventId: string, careUserId: string) {
      const eventRegDTO = <CreateEventRegistrationDTO>{
         careUserId: careUserId,
         eventId: eventId,
      };
      return this.singerEventsProxy.registerCareUserOnEvent(
         eventId,
         eventRegDTO
      );
   }

   registerCareUserOnEventSlot(
      eventId: string,
      eventSlotId: string,
      careUserId: string
   ) {
      const eventRegDTO = <CreateEventSlotRegistrationDTO>{
         careUserId: careUserId,
         eventSlotId: eventSlotId,
      };
      return this.singerEventsProxy.registerCareUserOnEventSlot(
         eventId,
         eventSlotId,
         eventRegDTO
      );
   }

   isUserRegisteredForEvent(
      eventId: string,
      careUserId: string
   ): Observable<UserRegisteredDTO> {
      return this.singerEventsProxy
         .isUserRegisteredForEvent(eventId, careUserId)
         .pipe(map(res => res));
   }

   getPublicEvents(
      searchEventData: SearchEventData
   ): Observable<EventDescription[]> {
      return this.singerEventsProxy.getPublicEvents(searchEventData).pipe(
         map(res =>
            res.map(y => {
               return <EventDescription>{
                  ageGroups: y.ageGroups,
                  description: y.description,
                  endDateTime: new Date(y.endDateTime),
                  id: y.id,
                  startDateTime: new Date(y.startDateTime),
                  title: y.title,
               };
            })
         )
      );
   }
}
