import { Component, OnInit, Inject } from '@angular/core';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';
import { FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { Registrant, EventSlot } from 'src/app/modules/core/models/registrant.model';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';

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

            const currentDate = Date.now();
            const nextEventSlots =
               this.eventSlots
                  .filter(a => a.startDateTime.getTime() >= currentDate)
                  .sort((a, b) => a.startDateTime.getTime() - b.startDateTime.getTime());
            this.selectedEventSlot = nextEventSlots.length > 0 ? nextEventSlots[0] : null;
         });
   }


   close() {
      this.dialogRef.close();
   }
}

