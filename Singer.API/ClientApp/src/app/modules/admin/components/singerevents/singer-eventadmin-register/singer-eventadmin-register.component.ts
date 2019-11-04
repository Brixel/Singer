import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SingerEventRegistrationsComponent, SingerEventRegistrationData } from '../singer-event-registrations/singer-event-registrations.component';
import { CareUserDTO } from 'src/app/modules/core/models/careuser.model';

@Component({
  selector: 'app-singer-eventadmin-register',
  templateUrl: './singer-eventadmin-register.component.html',
  styleUrls: ['./singer-eventadmin-register.component.css']
})
export class SingerEventAdminRegisterComponent implements OnInit {

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
  }


  close(){
   this.dialogRef.close();
}

save(){
   // this.singerEventService.updateRegistrants(this.event.id, registrantIds).subscribe((res) => res);
}
userSelected(careUser: CareUserDTO) {

   this.singerEventService.isUserRegisteredForEvent(this.event.id, careUser.id).subscribe((res) => console.log(res));
   // if (!this.registrants.map(r => r.careUserId).includes(careUser.id)) {
   //    // const registrant = new Registrant(careUser, RegistrationStatus.Pending);
   //    // this.registrants.push(registrant);
   // } else {
   //    // TODO Add notification when user already assigned
   // }
}
}
