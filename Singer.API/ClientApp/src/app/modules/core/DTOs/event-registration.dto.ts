import { RegistrationStatus, AgeGroup, TimeUnit, WeekDay, MonthRepeatMoment, RepeatType } from '../models/enum';
import { EventSlotDTO } from '../models/eventslot.dto';
import { SingerLocationDTO } from './singer-event-location.dto';
import { DaycareLocationDTO } from './daycarelocation.dto';
import { IFilterBaseDTO } from './filterbase.dto';

export interface CreateEventSlotRegistrationDTO {
   eventSlotId: string;
   careUserId: string;
   status?: RegistrationStatus;
}

export interface UserRegisteredDTO {
   careUserId: string;
   isRegisteredForAllEventslots: boolean;
   pendingStatesRemaining: number;
   status: RegistrationStatus;
}

export interface EventSlotRegistrationDTO {
   id: string;
   startDateTime: Date;
   endDateTime: Date;
   registrations: EventCareUserRegistrationDTO[];
}

export interface EventCareUserRegistrationDTO {
   registrationId: string;
   careUserId: string;
   status: RegistrationStatus;
   firstName: string;
   lastName: string;
   daycareLocation: DaycareLocationDTO;
}
export interface EventDescriptionDTO {
   title: string;
   description: string;
   ageGroup: AgeGroup[];
   cost: number;
   startDate: Date;
   endDate: Date;
}

export interface EventFilterParametersDTO extends IFilterBaseDTO {
   startDate: Date;
   endDate: Date;
   locationId: string;
   allowedAgeGroups: AgeGroup[];
   text: string;
}

export class EventRelevantCareUserDTO {
   id: string;
   userId: string;
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

export interface EventDescriptionDTO {
   id: string;
   title: string;
   description: string;
   ageGroups: AgeGroup[];
   startDateTime: Date;
   endDateTime: Date;
}

export interface SingerEventDTO {
   id: string;
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   location: SingerLocationDTO;
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
   eventSlots: EventSlotDTO[];
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
