import { GenericModel } from './generic-model';

export class EventRegistrationLog extends GenericModel {
   id: string;
   eventRegistrationId: string;
   eventTitle: string;
   careUser: string;
   legalGuardians: string[];
   creationDateTimeUTC: Date;
   eventSlotStartDateTime: Date;
   eventSlotEndDateTime: Date;
}
