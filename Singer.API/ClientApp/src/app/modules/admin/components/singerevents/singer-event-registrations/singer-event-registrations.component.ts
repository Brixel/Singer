import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';
import { FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatButtonToggleChange, MatSnackBar } from '@angular/material';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { Registrant } from 'src/app/modules/core/models/registrant.model';
import { EventSlot } from "src/app/modules/core/models/eventslot";
import { RegistrationStatus } from 'src/app/modules/core/models/enum';

import { SingerAdminEventService } from '../../../services/singer-admin-event.service';

export class SingerEventRegistrationData {
   event: SingerEvent;
}


@Component({
   selector: 'app-singer-event-registrations',
   templateUrl: './singer-event-registrations.component.html',
   styleUrls: ['./singer-event-registrations.component.css'],
})
export class SingerEventRegistrationsComponent implements OnInit {
   formGroup: FormGroup;
   event: SingerEvent;
   eventSlots: EventSlot[];
   selectedEventSlot: EventSlot;
   registrationStatus = RegistrationStatus;

   constructor(
      private singerEventService: SingerEventsService,
      private singerAdminEventService: SingerAdminEventService,
      private _snackBar: MatSnackBar,
      private dialogRef: MatDialogRef<SingerEventRegistrationsComponent>,
      @Inject(MAT_DIALOG_DATA) data: SingerEventRegistrationData
   ) {
      this.event = data.event;
      this.formGroup = new FormGroup({});
   }

   ngOnInit() {
      this.singerEventService
         .getEventRegistrations(this.event.id, 'asc', 'startDateTime', 0, 1000, '')
         .subscribe(res => {
            this.eventSlots = res.map(r => new EventSlot(r.id, r.startDateTime, r.endDateTime, r.registrations));

            // Search for the next upcoming event
            const currentDate = Date.now();
            const nextEventSlots =
               this.eventSlots
                  .filter(a => a.startDateTime.getTime() >= currentDate)
                  .sort((a, b) => a.startDateTime.getTime() - b.startDateTime.getTime());
            // If no upcoming event is found, take the first in the list
            this.selectedEventSlot = nextEventSlots.length > 0 ? nextEventSlots[0] : this.eventSlots[0];
         });
   }

   changeSelection(event: MatButtonToggleChange, registrationId: string) {
      const registrationStatus = <RegistrationStatus>event.value;
      switch (registrationStatus) {
         case RegistrationStatus.Accepted:
            this.singerAdminEventService
               .acceptRegistration(this.event.id, registrationId)
               .subscribe(res => this.processRegistration(res, registrationId));
               break;
         case RegistrationStatus.Rejected:
               this.singerAdminEventService
               .rejectRegistration(this.event.id, registrationId)
               .subscribe(res => this.processRegistration(res, registrationId));
               break;
         default:
         case RegistrationStatus.Pending:
               this._snackBar.open('Het is niet mogelijk om de registratie naar \'In afwachting\' te zetten');
               break;
      }

   }

   processRegistration(value: RegistrationStatus, registrationId: string) {
      const registrant = this.selectedEventSlot.registrants.find(x => x.registrationId === registrationId);
      registrant.registrationStatus = value;
      const statusValue = value === RegistrationStatus.Accepted ? 'goedgekeurd' : 'niet goedgekeurd';
      this._snackBar.open(`${registrant.name} is ${statusValue} voor it evenement`);
   }


   close() {
      this.dialogRef.close();
   }
}

