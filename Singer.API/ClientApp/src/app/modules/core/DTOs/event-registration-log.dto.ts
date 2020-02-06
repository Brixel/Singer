import { RegistrationStatus } from '../models/enum';

export interface RegistrationLogCareUserDTO {
   id: string;
   careUser: string;
   legalGuardians: LegalGuardianDTO[];
   creationDateTimeUTC: Date;
   registrationStateChanges: CareUserRegistrationStateChangedDTO[];
   registrationLocationChanges: CareUserRegistrationLocationChangedDTO[];
}

export interface RegistrationLogCareUserRegistrationDTO {
   eventRegistrationId: string;
   eventTitle: string;
   eventSlotStartDateTime: Date;
   eventSlotEndDateTime: Date;
}
export interface LegalGuardianDTO {
   name: string;
   email: string;
}
export interface CareUserRegistrationStateChangedDTO {
   eventRegistrationId: string;
   eventTitle: string;
   eventSlotStartDateTime: Date;
   eventSlotEndDateTime: Date;
   newStatus: RegistrationStatus;
}
export interface CareUserRegistrationLocationChangedDTO {
   eventRegistrationId: string;
   eventTitle: string;
   eventSlotStartDateTime: Date;
   eventSlotEndDateTime: Date;
   newLocation: string;
}
