import { RegistrationStatus } from './enum';
import { EventSlotDTO } from './eventslot.dto';
import { EventDescription } from './singerevent.model';
import { CareUserDTO } from './careuser.model';
import { GenericModel } from './generic-model';

export class EventRegistration extends GenericModel {
   eventSlot: EventSlotDTO;
   eventDescription: EventDescription;
   careUser: CareUserDTO;
   status: RegistrationStatus;
}
