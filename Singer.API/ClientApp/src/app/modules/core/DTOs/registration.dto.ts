import { SearchDTOBase } from './base.dto';
import { RegistrationStatus } from '../models/enum';
import { RegistrationType } from '../enums/registration-type';
import { DaycareLocationDTO } from './daycarelocation.dto';
import { EventSlotDTO } from '../models/eventslot.dto';
import { EventDescriptionDTO } from './event-registration.dto';
import { CareUserDTO } from '../models/careuser.model';
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

export interface RegistrationOverviewDTO {
   id: string;
   eventTitle: string;
   startDateTime: Date;
   endDateTime: Date;
   registrationType: RegistrationType;
   careUserFirstName: string;
   careUserLastName: string;
   registrationStatus: RegistrationStatus;
   daycareLocation: DaycareLocationDTO;
}

export interface CreateRegistrationDTO {
   eventId: string;
   careUserId: string;
   status?: RegistrationStatus;
}

export interface RegistrationSearchDTO extends SearchDTOBase {
   careUserIds?: string[];
   registrationType?: RegistrationType;
   registrationStatus?: RegistrationStatus;
   dateFrom?: Date;
   dateTo?: Date;
}
