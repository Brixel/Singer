import { SearchDTOBase } from './base.dto';
import { EventSlotDTO } from '../models/eventslot.dto';
import { EventDescriptionDTO } from './event-registration.dto';
import { CareUserDTO } from '../models/careuser.model';
import { RegistrationStatus } from '../models/enum';
import { RegistrationType } from '../enums/registration-type';
import { DaycareLocationDTO } from './daycarelocation.dto';
export interface RegistrationDTO {
   id: string;
   eventSlot: EventSlotDTO;
   eventDescription: EventDescriptionDTO;
   careUser: CareUserDTO;
   status: RegistrationStatus;
   registrationType: RegistrationType;
   daycareLocation: DaycareLocationDTO;
   startDateTime: Date;
   endDateTime: Date;
}

export interface CreateRegistrationDTO {
   eventId: string;
   careUserId: string;
   status?: RegistrationStatus;
}

export interface RegistrationSearchDTO extends SearchDTOBase {}
