import { RegistrationStatus } from 'src/app/modules/core/models/singerevent.model';
import { CareUserDTO } from 'src/app/modules/core/models/careuser.model';
export class Registrant {
   careUserId: string;
   name: string;
   registrationStatus: RegistrationStatus;
   constructor(careUser: CareUserDTO, registrationStatus: RegistrationStatus) {
      console.log(careUser);
      this.careUserId = careUser.id;
      this.name = `${careUser.firstName} ${careUser.lastName}`;
      this.registrationStatus = registrationStatus;
   }
}
