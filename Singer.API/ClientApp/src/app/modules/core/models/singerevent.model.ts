import { AgeGroup, RegistrationStatus } from './enum';
import { EventSlot } from './eventslot';
import { DaycareLocation } from './daycarelocation.model';
import { GenericModel } from './generics/generic-model';
import { GenericFilterParameters } from './generics/generic-filter-parameters.model';
import { SingerLocation } from './singer-location.model';

export class SingerEvent extends GenericModel {
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   location: SingerLocation;
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
   eventSlots: EventSlot[];
}

export class EventDescription extends GenericModel {
   title: string;
   description: string;
   ageGroups: AgeGroup[];
   cost: number;
   startDateTime: Date;
   endDateTime: Date;
}

export class EventRegisterDetails extends GenericModel {
   title: string;
   description: string;
   ageGroups: AgeGroup[];
   startDateTime: Date;
   endDateTime: Date;
   eventSlots: EventSlotRegistrations[];
   relevantCareUsers: EventRelevantCareUser[];
   registrationsOnDailyBasis: boolean;
}
export class EventRelevantCareUser extends GenericModel {
   firstName: string;
   lastName: string;
   ageGroup: AgeGroup;
   appropriateAgeGroup: boolean;
}
export class EventSlotRegistrations extends GenericModel {
   startDateTime: Date;
   endDateTime: Date;
   registrations: EventCareUserRegistration[];
}

export class EventCareUserRegistration {
   careUserId: string;
   status: RegistrationStatus;
   registrationId: string;
   firstName: string;
   lastName: string;
   daycareLocation: DaycareLocation;
}

export class UserInfo {
   careUserId: string;
   name: string;
   isRegisteredForAllEventslots: boolean;
   status: RegistrationStatus;
}

export class EventFilterParameters extends GenericFilterParameters {
   startDate: Date;
   endDate: Date;
   locationId: string;
   allowedAgeGroups: AgeGroup[];
}
