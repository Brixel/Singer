import { AgeGroup } from './enum';
import { SingerEventLocation } from './singer-event-location';
import { CareUserDTO } from './careuser.model';

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

export interface EventRegistrationDTO{
   id: string;
   eventSlot: EventSlotDTO;
   eventDescription: EventDescriptionDTO;
   careUser: CareUserDTO;
   status: RegistrationStatus;
}
export interface EventDescriptionDTO{
   title: string;
   description: string;
   startDate: Date;
   endDate: Date;
   ageGroup: AgeGroup[]
}

export interface EventSlotDTO{
   id: string;
   startDateTime: Date;
   endDateTime: Date;
}

export enum RegistrationStatus {
   Pending = 1,
   Accepted = 2,
   Rejected = 4
}
