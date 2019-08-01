import { Injectable } from '@angular/core';
import {
   SingerEvent,
   UpdateSingerEventDTO,
   CreateSingerEventDTO,
} from '../../models/singerevent.model';
import { Observable } from 'rxjs';
import { AgeGroup } from '../../models/enum';
import { Time } from '@angular/common';
import { SingerEventsProxy } from './singerevents.proxy';
import { map } from 'rxjs/operators';
import { PaginationDTO } from '../../models/pagination.model';

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
export class SingerEventsService {
   constructor(private singerEventsProxy: SingerEventsProxy) {}

   fetchSingerEventsData(sortDirection?: string, sortColumn?: string, pageIndex?: number, pageSize?: number, filter?: string): Observable<PaginationDTO> {
      return this.singerEventsProxy.getSingerEvents(sortDirection, sortColumn, pageIndex, pageSize, filter).pipe(map((res) => res));
   }

   updateSingerEvent(updateSingerEvent: SingerEvent) {
      const updateSingerEventDTO = <UpdateSingerEventDTO>{
         id: updateSingerEvent.id,
         name: updateSingerEvent.name,
         description: updateSingerEvent.description,
         location: updateSingerEvent.location,
         ageGroups: updateSingerEvent.ageGroups,
         totalSize: updateSingerEvent.totalSize,
         currentSize: updateSingerEvent.currentSize,
         price: updateSingerEvent.price,
         startRegistrationDate: updateSingerEvent.startRegistrationDate,
         endRegistrationDate: updateSingerEvent.endRegistrationDate,
         finalCancelationDate: updateSingerEvent.finalCancelationDate,
         registrationOnDailyBasis: updateSingerEvent.registrationOnDailyBasis,
         startDate: updateSingerEvent.startDate,
         endDate: updateSingerEvent.endDate,
         hasDayCareBefore: updateSingerEvent.hasDayCareBefore,
         dayCareBeforeStartTime: updateSingerEvent.dayCareBeforeStartTime,
         dayCareBeforeEndTime: updateSingerEvent.dayCareBeforeEndTime,
         hasDayCareAfter: updateSingerEvent.hasDayCareAfter,
         dayCareAfterStartTime: updateSingerEvent.dayCareAfterStartTime,
         dayCareAfterEndTime: updateSingerEvent.dayCareAfterEndTime,
      };
      return this.singerEventsProxy.updateSingerEvents(updateSingerEventDTO.id, updateSingerEventDTO).pipe(map((res) => res));
   }

   createSingerEvent(createSingerEvent: SingerEvent) {
      const createSingerEventDTO = <CreateSingerEventDTO>{
         name: createSingerEvent.name,
         description: createSingerEvent.description,
         location: createSingerEvent.location,
         ageGroups: createSingerEvent.ageGroups,
         totalSize: createSingerEvent.totalSize,
         currentSize: createSingerEvent.currentSize,
         price: createSingerEvent.price,
         startRegistrationDate: createSingerEvent.startRegistrationDate,
         endRegistrationDate: createSingerEvent.endRegistrationDate,
         finalCancelationDate: createSingerEvent.finalCancelationDate,
         registrationOnDailyBasis: createSingerEvent.registrationOnDailyBasis,
         startDate: createSingerEvent.startDate,
         endDate: createSingerEvent.endDate,
         hasDayCareBefore: createSingerEvent.hasDayCareBefore,
         dayCareBeforeStartTime: createSingerEvent.dayCareBeforeStartTime,
         dayCareBeforeEndTime: createSingerEvent.dayCareBeforeEndTime,
         hasDayCareAfter: createSingerEvent.hasDayCareAfter,
         dayCareAfterStartTime: createSingerEvent.dayCareAfterStartTime,
         dayCareAfterEndTime: createSingerEvent.dayCareAfterEndTime,
      };
      return this.singerEventsProxy.createSingerEvents(createSingerEventDTO).pipe(map((res) => res));
   }
}
