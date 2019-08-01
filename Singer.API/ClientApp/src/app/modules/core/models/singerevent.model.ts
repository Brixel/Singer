import { AgeGroup } from './enum';
import { Time } from '@angular/common';

export interface SingerEventDTO {
   id: string;
   name: string;
   description: string;
   location: string;
   ageGroups: AgeGroup[];
   totalSize: number;
   currentSize: number;
   price: number;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   finalCancelationDate: Date;
   registrationOnDailyBasis: boolean;
   startDate: Date;
   endDate: Date;
   hasDayCareBefore: boolean;
   dayCareBeforeStartTime: Time;
   dayCareBeforeEndTime: Time;
   hasDayCareAfter: boolean;
   dayCareAfterStartTime: Time;
   dayCareAfterEndTime: Time;
}

export interface UpdateSingerEventDTO {
   id: string;
   name: string;
   description: string;
   location: string;
   ageGroups: AgeGroup[];
   totalSize: number;
   currentSize: number;
   price: number;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   finalCancelationDate: Date;
   registrationOnDailyBasis: boolean;
   startDate: Date;
   endDate: Date;
   hasDayCareBefore: boolean;
   dayCareBeforeStartTime: Time;
   dayCareBeforeEndTime: Time;
   hasDayCareAfter: boolean;
   dayCareAfterStartTime: Time;
   dayCareAfterEndTime: Time;
}

export interface CreateSingerEventDTO {
   name: string;
   description: string;
   location: string;
   ageGroups: AgeGroup[];
   totalSize: number;
   currentSize: number;
   price: number;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   finalCancelationDate: Date;
   registrationOnDailyBasis: boolean;
   startDate: Date;
   endDate: Date;
   hasDayCareBefore: boolean;
   dayCareBeforeStartTime: Time;
   dayCareBeforeEndTime: Time;
   hasDayCareAfter: boolean;
   dayCareAfterStartTime: Time;
   dayCareAfterEndTime: Time;
}

export class SingerEvent {
   id: string;
   name: string;
   description: string;
   location: string;
   ageGroups: AgeGroup[];
   totalSize: number;
   currentSize: number;
   price: number;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   finalCancelationDate: Date;
   registrationOnDailyBasis: boolean;
   startDate: Date;
   endDate: Date;
   hasDayCareBefore: boolean;
   dayCareBeforeStartTime: Time;
   dayCareBeforeEndTime: Time;
   hasDayCareAfter: boolean;
   dayCareAfterStartTime: Time;
   dayCareAfterEndTime: Time;
}
