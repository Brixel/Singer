import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import {
   EventDescription,
   EventRelevantCareUserDTO,
} from 'src/app/modules/core/models/singerevent.model';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';

@Component({
   selector: 'app-event-registration',
   templateUrl: './event-registration.component.html',
   styleUrls: ['./event-registration.component.css'],
})
export class EventRegistrationComponent implements OnInit {
   public careUsers: EventRelevantCareUserDTO[];
   constructor(
      public dialogRef: MatDialogRef<EventRegistrationComponent>,
      @Inject(MAT_DIALOG_DATA) public event: EventDescription,
      private _eventService: SingerEventsService,
      private _snackBar: MatSnackBar
   ) {}

   ngOnInit() {
      this._eventService.getRelevantCareUsers(this.event.id).subscribe(res => {
         this.careUsers = res;
      });
   }

   registerCareUser(
      event: EventDescription,
      careUser: EventRelevantCareUserDTO
   ) {
      this._eventService.registerCareUser(event.id, careUser.id).subscribe(
         res => {
            this._snackBar.open(
               `${careUser.firstName} ${careUser.lastName} werd ingeschreven voor het evenement: ${event.title}`,
               'OK',
               { duration: 2000 }
            );
         },
         err => {
            this._snackBar.open(
               `⚠ Inschrijven van gebruiker ${careUser.firstName} ${careUser.lastName} op het evenement: ${event.title} is mislukt!`,
               'OK',
               { duration: 2000 }
            );
         }
      );
   }
}
