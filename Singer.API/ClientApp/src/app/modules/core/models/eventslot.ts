import { Registrant } from './registrant.model';
import { EventCareUserRegistrationDTO } from '../DTOs/event-registration.dto';
import { EventCareUserRegistration } from './singerevent.model';
export class EventSlot {
   id: string;
   startDateTime: Date;
   endDateTime: Date;
   registrants: Registrant[];
   currentRegistrants: number;
   constructor(
      id: string,
      startDateTime: Date,
      endDateTime: Date,
      registrations: EventCareUserRegistration[],
      currentRegistrants: number
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
                  reg.status,
                  reg.daycareLocation
               )
         )
         .sort(this.sorter());
      this.currentRegistrants = currentRegistrants;
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
