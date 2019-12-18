import { AgeGroup, RegistrationStatus } from './enum';
import { EventSlot } from './eventslot';
import { DaycareLocation } from './daycarelocation.model';

export class SingerEvent {
   id: string;
   title: string;
   description: string;
   allowedAgeGroups: AgeGroup[];
   location: SingerEventLocation;
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

export class EventDescription {
   id: string;
   title: string;
   description: string;
   ageGroups: AgeGroup[];
   startDateTime: Date;
   endDateTime: Date;
}

export class EventRegisterDetails {
   id: string;
   title: string;
   description: string;
   ageGroups: AgeGroup[];
   startDateTime: Date;
   endDateTime: Date;
   eventSlots: EventSlotRegistrations[];
   relevantCareUsers: EventRelevantCareUser[];
   registrationsOnDailyBasis: boolean;
}
export class EventRelevantCareUser {
   id: string;
   firstName: string;
   lastName: string;
   ageGroup: AgeGroup;
   appropriateAgeGroup: boolean;
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

export class SingerEventLocation {
   id: string;
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}
