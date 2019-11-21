import {
   RegistrationStatus,
   EventRelevantCareUserDTO,
} from 'src/app/modules/core/models/singerevent.model';
import { CareUserDTO } from 'src/app/modules/core/models/careuser.model';
export class Registrant {
   registrationId: string;
   careUserId: string;
   name: string;
   registrationStatus: RegistrationStatus;

   constructor(
      registrationId: string,
      careUserId: string,
      firstName: string,
      lastName: string,
      registrationStatus: RegistrationStatus
   ) {
      this.careUserId = careUserId;
      this.registrationId = registrationId;
      this.name = `${firstName} ${lastName}`;
      this.registrationStatus = registrationStatus;
   }
}


