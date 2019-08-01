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
         location: updateSingerEvent.location,
         allowedAgeGroups: updateSingerEvent.allowedAgeGroups,
         maxRegistrants: updateSingerEvent.maxRegistrants,
         currentRegistrants: updateSingerEvent.currentRegistrants,
         cost: updateSingerEvent.cost,
         startRegistrationDate: updateSingerEvent.startRegistrationDate,
         endRegistrationDate: updateSingerEvent.endRegistrationDate,
         finalCancelationDate: updateSingerEvent.finalCancelationDate,
         registrationOnDailyBasis: updateSingerEvent.registrationOnDailyBasis,
         startDate: updateSingerEvent.startDate,
         endDate: updateSingerEvent.endDate,
         dailyStartTime: updateSingerEvent.dailyStartTime,
         dailyEndTime: updateSingerEvent.dailyEndTime,
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
         title: createSingerEvent.title,
         description: createSingerEvent.description,
         location: createSingerEvent.location,
         allowedAgeGroups: createSingerEvent.allowedAgeGroups,
         maxRegistrants: createSingerEvent.maxRegistrants,
         currentRegistrants: createSingerEvent.currentRegistrants,
         cost: createSingerEvent.cost,
         startRegistrationDate: createSingerEvent.startRegistrationDate,
         endRegistrationDate: createSingerEvent.endRegistrationDate,
         finalCancelationDate: createSingerEvent.finalCancelationDate,
         registrationOnDailyBasis: createSingerEvent.registrationOnDailyBasis,
         startDate: createSingerEvent.startDate,
         endDate: createSingerEvent.endDate,
         dailyStartTime: createSingerEvent.dailyStartTime,
         dailyEndTime: createSingerEvent.dailyEndTime,
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
