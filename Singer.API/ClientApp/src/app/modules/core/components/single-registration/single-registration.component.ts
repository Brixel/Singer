import { Component, OnInit, Input } from '@angular/core';
import { EventSlotRegistrations, EventCareUserRegistration } from '../../models/singerevent.model';
import { Registrant } from '../../models/registrant.model';
import { SingerEventsService } from '../../services/singerevents-api/singerevents.service';
import { MatSnackBar } from '@angular/material';
import { RegistrationStatus } from '../../models/enum';

@Component({
   selector: 'app-single-registration',
   templateUrl: './single-registration.component.html',
   styleUrls: ['./single-registration.component.css'],
})
export class SingleRegistrationComponent implements OnInit {
   @Input() eventId: string;
   @Input() eventSlots: EventSlotRegistrations[] = [];
   @Input() careUsers: Registrant[] = [];
   RegistrationStatus = RegistrationStatus;
   constructor(private _eventService: SingerEventsService, private _snackBar: MatSnackBar) {}

   ngOnInit() {}

   getRegistrationStatus(careUserId: string): RegistrationStatus {
      let eventSlot = this.eventSlots[0].registrations.find(x => x.careUserId === careUserId);
      //console.log(eventSlot);
      return eventSlot === undefined ? 0 : eventSlot.status;
   }

   registerCareUserOnEvent(careUser: Registrant) {
      this._eventService.registerCareUserOnEvent(this.eventId, careUser.careUserId).subscribe(
         res => {
            this._snackBar.open(`${careUser.name} werd ingeschreven`, 'OK', {
               duration: 2000,
            });
            //TODO: When I wrote this, only me and god knew what I was doing, now, only god knows...
            let user = this.careUsers.find(x => x.careUserId === careUser.careUserId);
            user.registrationStatus = res[0].status;
            let slot = this.eventSlots.find(x => x.id === res[0].eventSlot.id);
            let registration = <EventCareUserRegistration>{
               careUserId: careUser.careUserId,
               daycareLocation: res[0].daycareLocation,
               firstName: res[0].careUser.firstName,
               lastName: res[0].careUser.lastName,
               registrationId: res[0].id,
               status: res[0].status,
            };
            slot.registrations.push(registration);
         },
         err => {
            this._snackBar.open(`⚠ ${err}`, 'OK');
         }
      );
   }
}
