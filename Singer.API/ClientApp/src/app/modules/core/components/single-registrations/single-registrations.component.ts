import { Component, OnInit, Input } from '@angular/core';
import { EventSlotRegistrations, RegistrationStatus } from '../../models/singerevent.model';
import { Registrant } from '../../models/registrant.model';
import { SingerEventsService } from '../../services/singerevents-api/singerevents.service';
import { MatSnackBar } from '@angular/material';

@Component({
   selector: 'app-single-registrations',
   templateUrl: './single-registrations.component.html',
   styleUrls: ['./single-registrations.component.css'],
})
export class SingleRegistrationsComponent implements OnInit {
   @Input() eventId: string;
   @Input() eventSlots: EventSlotRegistrations[] = [];
   @Input() careUsers: Registrant[] = [];
   constructor(private _eventService: SingerEventsService, private _snackbar: MatSnackBar) {}

   ngOnInit() {}

   getEventRegistrationStatus(careUserId: string): RegistrationStatus {
      return this.careUsers.find(x => x.careUserId === careUserId).registrationStatus;
   }

   registerCareUserOnEvent(
      careUser: Registrant
   ) {
      this._eventService
         .registerCareUserOnEvent(
            this.eventId,
            careUser.careUserId
         )
         .subscribe(
            res => {
               this._snackbar.open(
                  `${careUser.name} werd ingeschreven`,
                  'OK',
                  { duration: 2000 }
               );
               this.careUsers
                  .find(x => x.careUserId === careUser.careUserId).registrationStatus = res[0].status;

            },
            err => {
               console.log(err);
               this._snackbar.open(`⚠ ${err}`, 'OK');
            }
         );
   }
}


