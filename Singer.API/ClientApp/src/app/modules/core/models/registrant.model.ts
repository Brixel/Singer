import {
   RegistrationStatus,
   EventRelevantCareUserDTO,
   EventCareUserRegistrationDTO,
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

export class EventSlot {
   id: string;
   startDateTime: Date;
   endDateTime: Date;
   registrants: Registrant[];
   constructor(
      id: string,
      startDateTime: Date,
      endDateTime: Date,
      registrations: EventCareUserRegistrationDTO[]
   ) {
      this.id = id;
      this.startDateTime = new Date(startDateTime);
      this.endDateTime = new Date(endDateTime);
      this.registrants = registrations
         .map(
            reg =>
               new Registrant(
                  reg.registrationId,
                  reg.careUserId,
                  reg.firstName,
                  reg.lastName,
                  reg.status
               )
         )
         .sort(this.sorter());
   }

   private sorter(): (a: Registrant, b: Registrant) => number {
      return (a, b) => {
         // First sort by status
         if (a.registrationStatus < b.registrationStatus) {
            return -1;
         } else if (a.registrationStatus > b.registrationStatus) {
            return 1;
         }

         // then Sort by name
         if (a.name < b.name) {
            return -1;
         } else if (a.name > b.name) {
            return 1;
         } else {
            return 0;
         }
      };
   }
}
