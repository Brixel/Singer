import {
   AgeGroup,
   WeekDay,
   MonthRepeatMoment,
   RepeatType,
   TimeUnit
} from './enum';
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
   repeatSettings: EventRepeatSettingsDTO;
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
      id: string,
      title: string,
      description: string,
      ageGroups: AgeGroup[],
      startDateTime: Date,
      endDateTime: Date
   ) {
      this.id = id;
      this.title = title;
      this.description = description;
      this.ageGroups = ageGroups;
      this.startDateTime = startDateTime;
      this.endDateTime = endDateTime;
   }
   id: string;
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

export class EventRelevantCareUserDTO {
   id: string;
   firstName: string;
   lastName: string;
   ageGroup: AgeGroup;
   appropriateAgeGroup: boolean;
}

export interface EventRepeatSettingsDTO {
   interval: number;
   intervalUnit: TimeUnit;
   weekRepeatMoment: WeekDay;
   monthRepeatMoment: MonthRepeatMoment;
   repeatType: RepeatType;
   numberOfRepeats: number;
   stopRepeatDate: Date;
}

export class EventRegisterDetails {
   id: string;
   title: string;
   description: string;
   ageGroups: AgeGroup[];
   startDateTime: Date;
   endDateTime: Date;
   eventSlots: EventSlotRegistrations[];
   relevantCareUsers: EventRelevantCareUserDTO[];
   registrationsOnDailyBasis: boolean;
}

export class EventSlotRegistrations {
   id: string;
   startDateTime: Date;
   endDateTime: Date;
   registrations: EventCareUserRegistration[];
}

export class EventCareUserRegistration {
   careUserId: string;
   status: RegistrationStatus;
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
