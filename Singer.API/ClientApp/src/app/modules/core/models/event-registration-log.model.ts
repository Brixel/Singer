import { RegistrationStatus } from './enum';
import { GenericModel } from './generics/generic-model';

export class EventRegistrationLogCareUser extends GenericModel {
   id: string;
   careUser: string;
   legalGuardians: string[];
   creationDateTimeUTC: Date;
   registrationStateChanges: CareUserRegistrationStateChanged[] = [];
   registrationLocationChanges: CareUserRegistrationLocationChanged[] = [];
}

export class CareUserRegistrationStateChanged {
   eventRegistrationId: string;
   eventTitle: string;
   eventSlotStartDateTime: Date;
   eventSlotEndDateTime: Date;
   newStatus: RegistrationStatus;
}
export class CareUserRegistrationLocationChanged {
   eventRegistrationId: string;
   eventTitle: string;
   eventSlotStartDateTime: Date;
   eventSlotEndDateTime: Date;
   newLocation: string;
}

export class LegalGuardian {
   name: string;
   email: string;
}
