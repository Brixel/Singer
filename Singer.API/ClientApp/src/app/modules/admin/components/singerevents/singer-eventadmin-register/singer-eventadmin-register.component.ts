import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import {
   SingerEvent,
   RegistrationStatus,
   EventSlotRegistrations,
   EventCareUserRegistration,
   UserInfo,
} from 'src/app/modules/core/models/singerevent.model';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {
   SingerEventRegistrationsComponent,
   SingerEventRegistrationData,
} from '../singer-event-registrations/singer-event-registrations.component';
import { CareUserDTO } from 'src/app/modules/core/models/careuser.model';
import { BehaviorSubject } from 'rxjs';
import { Registrant } from 'src/app/modules/core/models/registrant.model';

@Component({
   selector: 'app-singer-eventadmin-register',
   templateUrl: './singer-eventadmin-register.component.html',
   styleUrls: ['./singer-eventadmin-register.component.css'],
})
export class SingerEventAdminRegisterComponent implements OnInit {
   formGroup: FormGroup;
   event: SingerEvent;
   careUsers: Registrant[] = [];

   private _userInfoSubject = new BehaviorSubject<UserInfo>(null);
   userInfo$ = this._userInfoSubject.asObservable();
   eventSlots: EventSlotRegistrations[];

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
         .getEventRegisterDetails(this.event.id).subscribe(res => {
            this.eventSlots = res.eventSlots;
         });
   }

   close() {
      this.dialogRef.close();
   }

   userSelected(careUser: CareUserDTO) {
      this.singerEventService
         .isUserRegisteredForEvent(this.event.id, careUser.id)
         .subscribe(res => {
            const userInfo = <UserInfo>{
               careUserId: res.careUserId,
               name: `${careUser.firstName} ${careUser.lastName}`,
               isRegistered: res.isRegistered,
               status: res.status,
            };
            this._userInfoSubject.next(userInfo);
            this.careUsers = [];
            if (!userInfo.isRegistered) {
               const registrant = <Registrant>{
                  careUserId: userInfo.careUserId,
                  name: userInfo.name,
                  registrationStatus: userInfo.status,
               };
               this.careUsers = [registrant];
            }
         });
   }
}