
export interface EventRegistrationLogDTO {
   id: string;
   eventRegistrationId: string;
   eventTitle: string;
   careUser: string;
   legalGuardians: string[];
   creationDateTimeUTC: Date;
   eventSlotStartDateTime: Date;
   EventSlotEndDateTime: Date;
}
