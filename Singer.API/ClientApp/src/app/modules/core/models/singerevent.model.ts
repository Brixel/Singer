import { AgeGroup } from './enum';

export interface SingerEventDTO {
   id: string;
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   location: SingerEventLocation;
   maxRegistrants: number;
   currentRegistrants: number;
   cost: number;
   startDate: Date;
   endDate: Date;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   dailyStartTime: Date;
   dailyEndTime: Date;
   finalCancellationDate: Date;
   registrationOnDailyBasis: boolean;
   hasDayCareBefore: boolean;
   dayCareBeforeStartTime: Date;
   dayCareBeforeEndTime: Date;
   hasDayCareAfter: boolean;
   dayCareAfterStartTime: Date;
   dayCareAfterEndTime: Date;
}

export interface UpdateSingerEventDTO {
   id: string;
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   locationId: string;
   maxRegistrants: number;
   cost: number;
   startDate: Date;
   endDate: Date;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   dailyStartTime: Date;
   dailyEndTime: Date;
   finalCancellationDate: Date;
   registrationOnDailyBasis: boolean;
   hasDayCareBefore: boolean;
   dayCareBeforeStartTime: Date;
   dayCareBeforeEndTime: Date;
   hasDayCareAfter: boolean;
   dayCareAfterStartTime: Date;
   dayCareAfterEndTime: Date;
}

export interface CreateSingerEventDTO {
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   locationId: string;
   maxRegistrants: number;
   cost: number;
   startDate: Date;
   endDate: Date;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   dailyStartTime: Date;
   dailyEndTime: Date;
   finalCancellationDate: Date;
   registrationOnDailyBasis: boolean;
   hasDayCareBefore: boolean;
   dayCareBeforeStartTime: Date;
   dayCareBeforeEndTime: Date;
   hasDayCareAfter: boolean;
   dayCareAfterStartTime: Date;
   dayCareAfterEndTime: Date;
}

export class SingerEvent {
   id: string;
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   location: SingerEventLocation;
   maxRegistrants: number;
   currentRegistrants: number;
   cost: number;
   startDate: Date;
   endDate: Date;
   startRegistrationDate: Date;
   endRegistrationDate: Date;
   dailyStartTime: Date;
   dailyEndTime: Date;
   finalCancellationDate: Date;
   registrationOnDailyBasis: boolean;
   hasDayCareBefore: boolean;
   dayCareBeforeStartTime: Date;
   dayCareBeforeEndTime: Date;
   hasDayCareAfter: boolean;
   dayCareAfterStartTime: Date;
   dayCareAfterEndTime: Date;
}

export class SingerEventLocation {
   id: string;
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export interface SingerEventLocationDTO {
   id: string;
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export interface CreateSingerEventLocationDTO {
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export interface UpdateSingerEventLocationDTO {
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export class EventDescription{
   constructor(title: string, description: string, ageGroups: AgeGroup[], startDate: Date, endDate: Date) {
     this.title = title;
     this.description = description;
     this.ageGroups = ageGroups;
     this.startDate = startDate;
     this.endDate = endDate;
   }
   title: string;
   description: string;
   ageGroups: AgeGroup[];
   startDate: Date;
   endDate: Date;
}

export class SearchEventDTO{
   startDate: Date;
   endDate: Date;
   locationId: string;
}

