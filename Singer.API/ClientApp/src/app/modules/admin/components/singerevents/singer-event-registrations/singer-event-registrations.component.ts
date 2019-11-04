import { Component, OnInit, Inject } from '@angular/core';
import { SingerEvent, RegistrationStatus } from 'src/app/modules/core/models/singerevent.model';
import { FormGroup } from '@angular/forms';
import { CareUser, CareUserDTO } from 'src/app/modules/core/models/careuser.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { Registrant } from 'src/app/modules/core/models/registrant.model';

export class SingerEventRegistrationData {
   event: SingerEvent;
}


@Component({
   selector: 'app-singer-event-registrations',
   templateUrl: './singer-event-registrations.component.html',
   styleUrls: ['./singer-event-registrations.component.css'],
})
export class SingerEventRegistrationsComponent implements OnInit {
   registrants: Registrant[] = [];
   formGroup: FormGroup;
   event: SingerEvent;

   constructor(
      private singerEventService: SingerEventsService,
      private dialogRef: MatDialogRef<SingerEventRegistrationsComponent>,
      @Inject(MAT_DIALOG_DATA) data: SingerEventRegistrationData
   ) {
      this.event = data.event;
      this.formGroup = new FormGroup({});
   }

   ngOnInit() {
      // this.singerEventService.getEventRegistrations(this.event.id).subscribe((events) => {
      //    const careUsers = events.map(event => new Registrant(event.careUser, event.status));
      //    this.registrants = this.createRegistrants(careUsers);
      // });
   }

   private createRegistrants(registrants: Registrant[]) {
      const uniqueNames = registrants.filter(function(elem, index, self) {
         // TODO Improve this once we need to approve multiple eventslots separate

         return index === self.map(c => c.careUserId).indexOf(elem.careUserId);
      });
      return uniqueNames;
   }

   close(){
      this.dialogRef.close();
   }

   save(){
      const registrantIds = this.registrants.map(r=>r.careUserId);
      // this.singerEventService.updateRegistrants(this.event.id, registrantIds).subscribe((res) => res);
   }

   userSelected(careUser: CareUserDTO) {
      if (!this.registrants.map(r => r.careUserId).includes(careUser.id)) {
         // const registrant = new Registrant(careUser, RegistrationStatus.Pending);
         // this.registrants.push(registrant);
      } else {
         // TODO Add notification when user already assigned
      }
   }
}

