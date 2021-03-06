import { GenericModel } from './generics/generic-model';
import { EventSlot } from './eventslot';
import { EventDescription } from './singerevent.model';
import { CareUser } from './careuser.model';
import { RegistrationStatus } from './enum';
import { RegistrationType } from '../enums/registration-type';
import { DaycareLocation } from './daycarelocation.model';

export class Registration extends GenericModel {
   eventSlot: EventSlot;
   eventDescription: EventDescription;
   careUser: CareUser;
   status: RegistrationStatus;
   registrationType: RegistrationType;
   daycareLocation: DaycareLocation;
   startDateTime: Date;
   endDateTime: Date;
}

export class RegistrationOverview extends GenericModel {
   id: string;
   eventTitle: string;
   startDateTime: Date;
   endDateTime: Date;
   registrationType: RegistrationType;
   careUserFirstName: string;
   careUserLastName: string;
   registrationStatus: RegistrationStatus;
   daycareLocation: DaycareLocation;
}
