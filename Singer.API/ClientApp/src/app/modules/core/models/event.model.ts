import { AgeGroup } from './enum';

export interface SingerEventDTO {
   id: string;
   name: string;
   description: string;
   location: string;
   ageGroups: AgeGroup[];
   size: number;
   price: number;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   finalCancelationDate: Date;
   registrationOnDailyBasis: boolean;
   startDate: Date;
   endDate: Date;
   hasDayCareBefore: boolean;
   dayCareBeforeStartDate: Date;
   dayCareBeforeEndDate: Date;
   hasDayCareAfter: boolean;
   dayCareAfterStartDate: Date;
   dayCareAfterEndDate: Date;
}

export interface UpdateSingerEventDTO {

   name: string;
   description: string;
   location: string;
   ageGroups: AgeGroup[];
   size: number;
   price: number;
   startRegistrationDate: Date;
   endRegistrationDate: Date;S
   finalCancelationDate: Date;
   registrationOnDailyBasis: boolean;
   startDate: Date;
   endDate: Date;
   hasDayCareBefore: boolean;
   dayCareBeforeStartDate: Date;
   dayCareBeforeEndDate: Date;
   hasDayCareAfter: boolean;
   dayCareAfterStartDate: Date;
   dayCareAfterEndDate: Date;
}

export interface CreateSingerEventDTO {

   name: string;
   description: string;
   location: string;
   ageGroups: AgeGroup[];
   size: number;
   price: number;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   finalCancelationDate: Date;
   registrationOnDailyBasis: boolean;
   startDate: Date;
   endDate: Date;
   hasDayCareBefore: boolean;
   dayCareBeforeStartDate: Date;
   dayCareBeforeEndDate: Date;
   hasDayCareAfter: boolean;
   dayCareAfterStartDate: Date;
   dayCareAfterEndDate: Date;
}

export class SingerEvent {
   id: string;
   name: string;
   description: string;
   location: string;
   ageGroups: AgeGroup[];
   size: number;
   price: number;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   finalCancelationDate: Date;
   registrationOnDailyBasis: boolean;
   startDate: Date;
   endDate: Date;
   hasDayCareBefore: boolean;
   dayCareBeforeStartDate: Date;
   dayCareBeforeEndDate: Date;
   hasDayCareAfter: boolean;
   dayCareAfterStartDate: Date;
   dayCareAfterEndDate: Date;
}
