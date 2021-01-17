import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { SingerEvent, EventSlotRegistrations, UserInfo } from 'src/app/modules/core/models/singerevent.model';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
   SingerRegistrationsComponent,
   SingerRegistrationData,
} from '../event-registrations/event-registrations.component';
import { CareUserDTO } from 'src/app/modules/core/models/careuser.model';
import { BehaviorSubject } from 'rxjs';
import { Registrant } from 'src/app/modules/core/models/registrant.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';

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
   canRegisterForEvent: boolean;

   constructor(
      private singerEventService: SingerEventsService,
      private dialogRef: MatDialogRef<SingerRegistrationsComponent>,
      @Inject(MAT_DIALOG_DATA) data: SingerRegistrationData
   ) {
      this.event = data.event;
      this.formGroup = new FormGroup({});
   }

   ngOnInit() {
      this.singerEventService.getEventRegisterDetails(this.event.id).subscribe(res => {
         this.eventSlots = res.eventSlots;
      });
   }

   close() {
      this.dialogRef.close();
   }

   userSelected(careUser: CareUserDTO) {
      this.singerEventService.isUserRegisteredForEvent(this.event.id, careUser.id).subscribe(res => {
         const userInfo = <UserInfo>{
            careUserId: res.careUserId,
            name: `${careUser.firstName} ${careUser.lastName}`,
            isRegisteredForAllEventslots: res.isRegisteredForAllEventslots,
            status: res.status,
         };
         this._userInfoSubject.next(userInfo);
         this.canRegisterForEvent = this.canBeRegisteredForEvent(this.event.allowedAgeGroups, careUser.ageGroup);
         this.careUsers = [];
         const registrant = <Registrant>{
            careUserId: userInfo.careUserId,
            name: userInfo.name,
            registrationStatus: userInfo.status,
         };
         this.careUsers = [registrant];
      });
   }
   private canBeRegisteredForEvent(allowedAgeGroups: AgeGroup[], ageGroupOfUser: AgeGroup) {
      return allowedAgeGroups.includes(ageGroupOfUser);
   }
}
