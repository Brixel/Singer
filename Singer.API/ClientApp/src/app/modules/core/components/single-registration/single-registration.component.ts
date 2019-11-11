import { Component, OnInit, Input } from '@angular/core';
import { EventSlotRegistrations, RegistrationStatus } from '../../models/singerevent.model';
import { Registrant } from '../../models/registrant.model';
import { SingerEventsService } from '../../services/singerevents-api/singerevents.service';
import { MatSnackBar } from '@angular/material';

@Component({
   selector: 'app-single-registration',
   templateUrl: './single-registration.component.html',
   styleUrls: ['./single-registration.component.css'],
})
export class SingleRegistrationComponent implements OnInit {
   @Input() eventId: string;
   @Input() eventSlots: EventSlotRegistrations[] = [];
   @Input() careUsers: Registrant[] = [];
   constructor(private _eventService: SingerEventsService, private _snackBar: MatSnackBar) {}

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
               this._snackBar.open(
                  `${careUser.name} werd ingeschreven`,
                  'OK',
                  { duration: 2000 }
               );
               this.careUsers
                  .find(x => x.careUserId === careUser.careUserId).registrationStatus = res[0].status;

            },
            err => {
               console.log(err);
               this._snackBar.open(`⚠ ${err}`, 'OK');
            }
         );
   }
}

