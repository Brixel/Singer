import { Injectable } from '@angular/core';
import {
   SingerEvent,
   UpdateSingerEventDTO,
   CreateSingerEventDTO,
} from '../../models/event.model';
import { AgeGroup } from '../../models/enum';
import { Time } from '@angular/common';

const EXAMPLE_SINGER_EVENTS: SingerEvent[] = [
   {
      id: '1',
      name: 'Introduction',
      description: 'Meetup between parents and teachers',
      location: 'Sint-Gerardusdreef 1 3590 Diepenbeek',
      ageGroups: [AgeGroup.Adult, AgeGroup.Child],
      totalSize: 15,
      currentSize: 0,
      price: 26.95,
      startRegistrationDate: new Date(),
      endRegistrationDate: new Date(),
      finalCancelationDate: new Date(),
      registrationOnDailyBasis: false,
      startDate: new Date(),
      endDate: new Date(),
      hasDayCareBefore: false,
      dayCareBeforeStartTime: { hours: 0, minutes: 0 },
      dayCareBeforeEndTime: { hours: 0, minutes: 0 },
      hasDayCareAfter: false,
      dayCareAfterStartTime: { hours: 0, minutes: 0 },
      dayCareAfterEndTime: { hours: 0, minutes: 0 },
   },
   {
      id: '2',
      name: 'Picknick',
      description: 'A lovely picknick for ourstudents',
      location: 'Sint-Gerardusdreef 1 3590 Diepenbeek',
      ageGroups: [AgeGroup.Toddler, AgeGroup.Child, AgeGroup.Youngster],
      totalSize: 35,
      currentSize: 0,
      price: 3.5,
      startRegistrationDate: new Date(),
      endRegistrationDate: new Date(),
      finalCancelationDate: new Date(),
      registrationOnDailyBasis: true,
      startDate: new Date(),
      endDate: new Date(),
      hasDayCareBefore: true,
      dayCareBeforeStartTime: { hours: 0, minutes: 0 },
      dayCareBeforeEndTime: { hours: 0, minutes: 0 },
      hasDayCareAfter: true,
      dayCareAfterStartTime: { hours: 0, minutes: 0 },
      dayCareAfterEndTime: { hours: 0, minutes: 0 },
   },
];

@Injectable({
   providedIn: 'root',
})
export class EventsService {
   constructor() {}

   fetchSingerEventsData(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ) {
      return EXAMPLE_SINGER_EVENTS;
   }
   updateSingerEvent(updateSingerEvent: SingerEvent) {
      const updateSingerEventDTO = <UpdateSingerEventDTO>{};
   }

   createSingerEvent(createSingerEvent: SingerEvent) {
      const createSingerEventDTO = <CreateSingerEventDTO>{};
      EXAMPLE_SINGER_EVENTS.push(createSingerEvent);
   }
}
