import {
   RegistrationStatus,
} from 'src/app/modules/core/models/singerevent.model';
import { DaycareLocationDTO } from './daycarelocation.dto';
export class Registrant {
   registrationId: string;
   careUserId: string;
   name: string;
   registrationStatus: RegistrationStatus;
   daycareLocation: DaycareLocationDTO;

   constructor(
      registrationId: string,
      careUserId: string,
      firstName: string,
      lastName: string,
      registrationStatus: RegistrationStatus,
      daycareLocation: DaycareLocationDTO
   ) {
      this.careUserId = careUserId;
      this.registrationId = registrationId;
      this.name = `${firstName} ${lastName}`;
      this.registrationStatus = registrationStatus;
      this.daycareLocation = daycareLocation;
   }
}


