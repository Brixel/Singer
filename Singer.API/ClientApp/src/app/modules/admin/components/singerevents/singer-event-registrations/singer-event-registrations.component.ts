import * as FileSaver from 'file-saver';
import { Component, OnInit, Inject } from '@angular/core';
import {
   SingerEvent,
   SingerEventLocation,
} from 'src/app/modules/core/models/singerevent.model';
import { FormGroup } from '@angular/forms';
import {
   MatDialogRef,
   MAT_DIALOG_DATA,
   MatButtonToggleChange,
   MatSnackBar,
   MatSelectChange,
} from '@angular/material';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { EventSlot } from 'src/app/modules/core/models/eventslot';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';

import { SingerAdminEventService } from '../../../services/singer-admin-event.service';
import { SingerEventLocationService } from 'src/app/modules/core/services/singerevents-api/singerevent-location.service';
import { DaycareLocationDTO } from 'src/app/modules/core/DTOs/daycarelocation.dto';
import { isNullOrUndefined } from 'util';
import { LoadingService } from 'src/app/modules/core/services/loading.service';

export class SingerEventRegistrationData {
   event: SingerEvent;
   defaultEventSlot?: EventSlot;
}

@Component({
   selector: 'app-singer-event-registrations',
   templateUrl: './singer-event-registrations.component.html',
   styleUrls: ['./singer-event-registrations.component.css'],
})
export class SingerEventRegistrationsComponent implements OnInit {
   formGroup: FormGroup;
   event: SingerEvent;
   selectedEventSlot: EventSlot;
   registrationStatus = RegistrationStatus;
   availableLocations: SingerEventLocation[];
   hasDaycare: boolean;

   constructor(
      private singerEventService: SingerEventsService,
      private singerAdminEventService: SingerAdminEventService,
      private _singerEventLocationService: SingerEventLocationService,
      private _snackBar: MatSnackBar,
      private dialogRef: MatDialogRef<SingerEventRegistrationsComponent>,
      @Inject(MAT_DIALOG_DATA) data: SingerEventRegistrationData,
      private _loadingService: LoadingService
   ) {
      this.event = data.event;
      this.hasDaycare =
         data.event.hasDayCareAfter || data.event.hasDayCareBefore;
      this.formGroup = new FormGroup({});
      this.selectedEventSlot = data.defaultEventSlot;
   }

   ngOnInit() {
      this._loadingService.show();
      this.singerEventService
         .getEventRegistrations(
            this.event.id,
            'asc',
            'startDateTime',
            0,
            1000,
            ''
         )
         .subscribe(res => {
            this.event.eventSlots = res.map(
               r =>
                  new EventSlot(
                     r.id,
                     r.startDateTime,
                     r.endDateTime,
                     r.registrations,
                     r.registrations.length
                  )
            );
            if (isNullOrUndefined(this.selectedEventSlot)) {
               // Search for the next upcoming event
               const currentDate = Date.now();
               const nextEventSlots = this.event.eventSlots
                  .filter(a => a.startDateTime.getTime() >= currentDate)
                  .sort(
                     (a, b) =>
                        a.startDateTime.getTime() - b.startDateTime.getTime()
                  );
               // If no upcoming event is found, take the first in the list
               this.selectedEventSlot =
                  nextEventSlots.length > 0
                     ? nextEventSlots[0]
                     : this.event.eventSlots[0];
            } else {
               this.selectedEventSlot = this.event.eventSlots.find(
                  x => x.id === this.selectedEventSlot.id
               );
            }
            this._loadingService.hide();
         });

      this._singerEventLocationService
         .fetchSingerEventLocationsData('asc', 'name', 0, 1000, '')
         .subscribe(res => {
            this.availableLocations = res.items as SingerEventLocation[];
         });
   }

   compareLocations(
      locationX: SingerEventLocation,
      locationY: DaycareLocationDTO
   ) {
      if (!locationY) {
         return false;
      }
      return locationX.id === locationY.id;
   }

   changeLocation(event: MatSelectChange, registrationId: string) {
      const daycareLocation = <SingerEventLocation>event.value;
      this.singerAdminEventService
         .updateDaycareLocation(
            this.event.id,
            registrationId,
            daycareLocation.id
         )
         .subscribe(res =>
            this._snackBar.open(`Opvanglocatie naar ${res.name} gewijzigd`)
         );
   }

   changeRegistration(event: MatButtonToggleChange, registrationId: string) {
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
            this._snackBar.open(
               "Het is niet mogelijk om de registratie naar 'In afwachting' te zetten"
            );
            break;
      }
   }

   processRegistration(value: RegistrationStatus, registrationId: string) {
      const registrant = this.selectedEventSlot.registrants.find(
         x => x.registrationId === registrationId
      );
      registrant.registrationStatus = value;
      const statusValue =
         value === RegistrationStatus.Accepted
            ? 'goedgekeurd'
            : 'niet goedgekeurd';
      this._snackBar.open(
         `${registrant.name} is ${statusValue} voor it evenement`
      );
   }

   close() {
      this.dialogRef.close();
   }

   export() {
      this.singerEventService
         .downloadEventSlotRegistartionCsv(
            this.event.id,
            this.selectedEventSlot.id
         )
         .subscribe(
            response => {
               let blob: any = new Blob([response], {
                  type: 'text/plain; charset=utf-8',
               });

               let eventDate =
                  `${this.selectedEventSlot.startDateTime.getFullYear()}-` +
                  `${this.selectedEventSlot.startDateTime.getMonth()}-` +
                  `${this.selectedEventSlot.startDateTime.getDay()} ` +
                  `${this.selectedEventSlot.startDateTime.getHours()}u` +
                  `${this.selectedEventSlot.startDateTime.getMinutes()}`;

               FileSaver.saveAs(
                  blob,
                  `${this.event.title} - ${eventDate} - deelnemers.csv`
               );
            },
            error => {
               console.log('Error downloading the file');
               console.log(error);
            },
            () => console.info('File downloaded successfully')
         );
   }
}
