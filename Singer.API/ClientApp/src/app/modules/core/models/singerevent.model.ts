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
   startDateTime: Date;
   endDateTime: Date;
   startRegistrationDateTime: Date;
   endRegistrationDateTime: Date;
   finalCancellationDateTime: Date;
   registrationOnDailyBasis: boolean;
   hasDayCareBefore: boolean;
   dayCareBeforeStartDateTime: Date;
   hasDayCareAfter: boolean;
   dayCareAfterEndDateTime: Date;
}

export interface UpdateSingerEventDTO {
   id: string;
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   locationId: string;
   maxRegistrants: number;
   cost: number;
   startDateTime: Date;
   endDateTime: Date;
   startRegistrationDateTime: Date;
   endRegistrationDateTime: Date;
   finalCancellationDateTime: Date;
   registrationOnDailyBasis: boolean;
   hasDayCareBefore: boolean;
   dayCareBeforeStartDateTime: Date;
   hasDayCareAfter: boolean;
   dayCareAfterEndDateTime: Date;
}

export interface CreateSingerEventDTO {
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   locationId: string;
   maxRegistrants: number;
   cost: number;
   startDateTime: Date;
   endDateTime: Date;
   startRegistrationDateTime: Date;
   endRegistrationDateTime: Date;
   finalCancellationDateTime: Date;
   registrationOnDailyBasis: boolean;
   hasDayCareBefore: boolean;
   dayCareBeforeStartDateTime: Date;
   hasDayCareAfter: boolean;
   dayCareAfterEndDateTime: Date;
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
   startDateTime: Date;
   endDateTime: Date;
   startRegistrationDateTime: Date;
   endRegistrationDateTime: Date;
   finalCancellationDateTime: Date;
   registrationOnDailyBasis: boolean;
   hasDayCareBefore: boolean;
   dayCareBeforeStartDateTime: Date;
   hasDayCareAfter: boolean;
   dayCareAfterEndDateTime: Date;
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

export class EventDescription {
   constructor(
      title: string,
      description: string,
      ageGroups: AgeGroup[],
      startDateTime: Date,
      endDateTime: Date
   ) {
      this.title = title;
      this.description = description;
      this.ageGroups = ageGroups;
      this.startDateTime = startDateTime;
      this.endDateTime = endDateTime;
   }
   title: string;
   description: string;
   ageGroups: AgeGroup[];
   startDateTime: Date;
   endDateTime: Date;
}

export class SearchEventDTO {
   startDateTime: Date;
   endDateTime: Date;
   locationId: string;
}
