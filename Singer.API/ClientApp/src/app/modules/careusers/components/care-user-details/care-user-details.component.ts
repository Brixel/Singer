import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { CareUser } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { MatInput, MatDatepicker, MatSelect } from '@angular/material';

@Component({
   selector: 'app-care-user-details',
   templateUrl: './care-user-details.component.html',
   styleUrls: ['./care-user-details.component.css'],
})
export class CareUserDetailsComponent implements OnInit {

   careUser: CareUser =  {
      id: '1',
      firstName: 'Joske',
      lastName: 'Vermeulen',
      email: 'joske.vermeulen@gmail.com',
      userName: 'joske',
      birthday: new Date('2008-07-06'),
      caseNumber: '0123456789',
      ageGroup: 'Kinderen',
      isExtern: false,
      hasTrajectory: true,
      hasNormalDayCare: true,
      hasVacationDayCare: true,
      hasResources: true,
   }

   idField: string;
   firstNameField: string;
   lastNameField: string;
   emailField: string;
   userNameField: string;
   birthdayField: Date;
   caseNumberField: string;
   ageGroupField: string; //Maybe replace by own class?
   isExternField: string;
   hasTrajectoryField: string;
   hasNormalDayCareField: string;
   hasVacationDayCareField: string;
   hasResourcesField: string;

   // Min and Max dates for the birthday datepicker
   birthdayDatePickerMinDate = new Date(1900, 0, 1);
   birthdayDatePickerMaxDate = new Date();

   constructor() {}

   ngOnInit() {
      this.lastNameField = this.careUser.lastName;
      this.firstNameField = this.careUser.firstName;
      this.birthdayField = this.careUser.birthday;
      this.caseNumberField = this.careUser.caseNumber;
      debugger;
      this.ageGroupField = this.careUser.ageGroup === 'Kinderen' ? 'child' : 'youngster';
      this.isExternField = this.careUser.isExtern ? 'true' : 'false';
      this.hasTrajectoryField = this.careUser.hasTrajectory ? 'true' : 'false';
      this.hasNormalDayCareField = this.careUser.hasNormalDayCare ? 'true' : 'false';
      this.hasVacationDayCareField = this.careUser.hasVacationDayCare ? 'true' : 'false';
      this.hasResourcesField = this.careUser.hasResources ? 'true' : 'false';
   }
}
