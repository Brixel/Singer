import { DaycareLocationDTO } from '../DTOs/daycarelocation.dto';
import { RegistrationStatus } from './enum';
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
