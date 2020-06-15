import * as FileSaver from 'file-saver';
import { Component, OnInit, Inject } from '@angular/core';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';
import { FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatSelectChange } from '@angular/material';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { EventSlot } from 'src/app/modules/core/models/eventslot';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';

import { SingerAdminEventService } from '../../../services/singer-admin-event.service';
import { SingerLocationService } from 'src/app/modules/core/services/singer-location-api/singer-location.service';
import { DaycareLocationDTO } from 'src/app/modules/core/DTOs/daycarelocation.dto';
import { isNullOrUndefined } from 'util';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { SingerLocation } from 'src/app/modules/core/models/singer-location.model';

export class SingerRegistrationData {
   event: SingerEvent;
   defaultEventSlot?: EventSlot;
}

@Component({
   selector: 'app-singer-registrations',
   templateUrl: './event-registrations.component.html',
   styleUrls: ['./event-registrations.component.css'],
})
export class SingerRegistrationsComponent implements OnInit {
   formGroup: FormGroup;
   event: SingerEvent;
   selectedEventSlot: EventSlot;
   registrationStatus = RegistrationStatus;
   availableLocations: SingerLocation[];
   hasDaycare: boolean;

   constructor(
      private singerEventService: SingerEventsService,
      private singerAdminEventService: SingerAdminEventService,
      private _singerLocationService: SingerLocationService,
      private _snackBar: MatSnackBar,
      private dialogRef: MatDialogRef<SingerRegistrationsComponent>,
      @Inject(MAT_DIALOG_DATA) data: SingerRegistrationData,
      private _loadingService: LoadingService
   ) {
      this.event = data.event;
      this.hasDaycare = data.event.hasDayCareAfter || data.event.hasDayCareBefore;
      this.formGroup = new FormGroup({});
      this.selectedEventSlot = data.defaultEventSlot;
   }

   ngOnInit() {
      this._loadingService.show();
      this.singerEventService.getRegistrations(this.event.id, 'asc', 'startDateTime', 0, 1000, '').subscribe(res => {
         this.event.eventSlots = res.map(
            r => new EventSlot(r.id, r.startDateTime, r.endDateTime, r.registrations, r.registrations.length)
         );
         if (isNullOrUndefined(this.selectedEventSlot)) {
            // Search for the next upcoming event
            const currentDate = Date.now();
            const nextEventSlots = this.event.eventSlots
               .filter(a => a.startDateTime.getTime() >= currentDate)
               .sort((a, b) => a.startDateTime.getTime() - b.startDateTime.getTime());
            // If no upcoming event is found, take the first in the list
            this.selectedEventSlot = nextEventSlots.length > 0 ? nextEventSlots[0] : this.event.eventSlots[0];
         } else {
            this.selectedEventSlot = this.event.eventSlots.find(x => x.id === this.selectedEventSlot.id);
         }
         this._loadingService.hide();
      });

      this._singerLocationService.fetchSingerLocationsData('asc', 'name', 0, 1000, '').subscribe(res => {
         this.availableLocations = res.items as SingerLocation[];
      });
   }

   compareLocations(locationX: SingerLocation, locationY: DaycareLocationDTO) {
      if (!locationY) {
         return false;
      }
      return locationX.id === locationY.id;
   }

   changeLocation(event: MatSelectChange, registrationId: string) {
      const daycareLocation = <SingerLocation>event.value;
      this.singerAdminEventService
         .updateDaycareLocation(this.event.id, registrationId, daycareLocation.id)
         .subscribe(res =>
            this._snackBar.open(`Opvanglocatie naar ${res.name} gewijzigd`, 'OK', {
               duration: 2000,
            })
         );
   }

   changeRegistration(registrationStatus: RegistrationStatus, registrationId: string) {
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
            this._snackBar.open("Het is niet mogelijk om de registratie naar 'In afwachting' te zetten", 'OK', {
               duration: 2000,
            });
            break;
      }
   }

   processRegistration(value: RegistrationStatus, registrationId: string) {
      const registrant = this.selectedEventSlot.registrants.find(x => x.registrationId === registrationId);
      registrant.registrationStatus = value;
      const statusValue = value === RegistrationStatus.Accepted ? 'goedgekeurd' : 'niet goedgekeurd';
      this._snackBar.open(`${registrant.name} is ${statusValue} voor it evenement`, 'OK', {
         duration: 2000,
      });
   }

   close() {
      this.dialogRef.close();
   }

   export() {
      this.singerEventService.downloadEventSlotRegistartionCsv(this.event.id, this.selectedEventSlot.id).subscribe(
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

            FileSaver.saveAs(blob, `${this.event.title} - ${eventDate} - deelnemers.csv`);
         },
         error => this.handleDownloadError(error),
         () => console.info('File downloaded successfully')
      );
   }

   handleDownloadError(err: any) {
      if (typeof err === 'string') {
         this._snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         let messages = [];
         for (var k in err) {
            messages.push(err[k]);
         }
         this._snackBar.open(`⚠ Er zijn fouten opgetreden bij het downloaden:\n${messages.join('\n')}`, 'OK', {
            panelClass: 'multi-line-snackbar',
         });
      }
   }
}
