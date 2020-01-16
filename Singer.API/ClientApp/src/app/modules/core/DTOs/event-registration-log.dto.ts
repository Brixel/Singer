
export interface EventRegistrationLogCareUserDTO {
   id: string;
   careUser: string;
   legalGuardians: string[];
   creationDateTimeUTC: Date;
   registrations: EventRegistrationLogCareUserRegistrationDTO[];
}

export interface EventRegistrationLogCareUserRegistrationDTO{
   eventRegistrationId: string;
   eventTitle: string;
   eventSlotStartDateTime: Date;
   eventSlotEndDateTime: Date;
}
