import { Injectable } from '@angular/core';
import {
   SingerEvent,
   UpdateSingerEventDTO,
   CreateSingerEventDTO,
   SingerEventLocation,
} from '../../models/singerevent.model';
import { Observable } from 'rxjs';
import { AgeGroup } from '../../models/enum';
import { Time } from '@angular/common';
import { SingerEventsProxy } from './singerevents.proxy';
import { map } from 'rxjs/operators';
import { PaginationDTO } from '../../models/pagination.model';
import { now } from 'moment';

const EXAMPLE_SINGER_EVENTS: SingerEvent[] = [
   {
      id: '1',
      title: 'Introduction',
      description: 'Meetup between parents and teachers',
      location: <SingerEventLocation>{
         name: 'StG HQ',
         address: 'Sint-Gerardusdreef 1',
         city: 'Diepenbeek',
         postalCode: '3590',
      },
      allowedAgeGroups: [AgeGroup.Adult, AgeGroup.Child],
      maxRegistrants: 15,
      currentRegistrants: 0,
      cost: 26.95,
      startRegistrationDate: new Date(),
      endRegistrationDate: new Date(),
      finalCancelationDate: new Date(),
      registrationOnDailyBasis: false,
      dailyStartTime: { hours: 0, minutes: 0 },
      dailyEndTime: { hours: 0, minutes: 0 },
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
      title: 'Picknick',
      description: 'A lovely picknick for ourstudents',
      location: <SingerEventLocation>{
         name: 'StG HQ',
         address: 'Sint-Gerardusdreef 1',
         city: 'Diepenbeek',
         postalCode: '3590',
      },
      allowedAgeGroups: [AgeGroup.Toddler, AgeGroup.Child, AgeGroup.Youngster],
      maxRegistrants: 35,
      currentRegistrants: 0,
      cost: 3.5,
      startRegistrationDate: new Date(),
      endRegistrationDate: new Date(),
      finalCancelationDate: new Date(),
      registrationOnDailyBasis: true,
      dailyStartTime: { hours: 0, minutes: 0 },
      dailyEndTime: { hours: 0, minutes: 0 },
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
      const updateSingerEventDTO = <UpdateSingerEventDTO>{};
   }

   createSingerEvent(createSingerEvent: SingerEvent) {
      const createSingerEventDTO = <CreateSingerEventDTO>{};
      EXAMPLE_SINGER_EVENTS.push(createSingerEvent);
   }
}
