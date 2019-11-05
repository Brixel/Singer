import { Component, OnInit, Input } from '@angular/core';
import { EventSlotRegistrations, EventRelevantCareUserDTO, RegistrationStatus } from '../../models/singerevent.model';
import { Registrant } from '../../models/registrant.model';

@Component({
   selector: 'app-single-registrations',
   templateUrl: './single-registrations.component.html',
   styleUrls: ['./single-registrations.component.css'],
})
export class SingleRegistrationsComponent implements OnInit {
   @Input() eventSlots: EventSlotRegistrations[] = [];
   @Input() careUsers: Registrant[] = [];
   constructor() {}

   ngOnInit() {}

   getEventRegistrationStatus(careUserId: string): RegistrationStatus {
      return this.careUsers.find(x => x.careUserId === careUserId).registrationStatus;
   }
}


