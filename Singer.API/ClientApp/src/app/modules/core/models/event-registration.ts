import { RegistrationStatus } from './enum';
import { EventSlotDTO } from './event-slot';
import { EventDescription } from './singerevent.model';
import { CareUserDTO } from './careuser.model';

export interface CreateEventRegistrationDTO {
   eventId: string;
   careUserId: string;
   status?: RegistrationStatus;
}

export interface CreateEventSlotRegistrationDTO {
   eventSlotId: string;
   careUserId: string;
   status?: RegistrationStatus;
}

export interface EventRegistrationDTO {
   id: string;
   eventSlot: EventSlotDTO;
   eventDescription: EventDescription;
   careUser: CareUserDTO;
   status: RegistrationStatus;
}
export interface UserRegisteredDTO{
   isRegistered: boolean;
   pendingStatussesRemaining: number;
   status: RegistrationStatus;
}
