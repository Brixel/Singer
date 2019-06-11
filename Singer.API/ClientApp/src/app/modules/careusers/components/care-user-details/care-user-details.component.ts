import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CareUser } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
@Component({
   selector: 'app-care-user-details',
   templateUrl: './care-user-details.component.html',
   styleUrls: ['./care-user-details.component.css'],
})

export class CareUserDetailsComponent implements OnInit {

   @Output() closeFormEvent = new EventEmitter<boolean>();

   careUser: CareUser;

   // Binding properties for form values
   idFieldValue: string;
   firstNameFieldValue: string;
   lastNameFieldValue: string;
   emailFieldValue: string;
   userNameFieldValue: string;
   birthdayFieldValue: Date;
   caseNumberFieldValue: string;
   ageGroupFieldValue: string;
   isExternFieldValue: string;
   hasTrajectoryFieldValue: string;
   hasNormalDayCareFieldValue: string;
   hasVacationDayCareFieldValue: string;
   hasResourcesFieldValue: string;

   idFieldPlaceholder: string = 'ID';
   firstNameFieldPlaceholder: string = 'Voornaam';
   lastNameFieldPlaceholder: string = 'Familienaam';
   emailFieldPlaceholder: string = 'email';
   userNameFieldPlaceholder: string = 'Gebruikersnaam';
   birthdayFieldPlaceholder: string = 'Selecteer een datum';
   caseNumberFieldPlaceholder: string = 'Dossiernr';
   ageGroupFieldPlaceholder: string = 'Leeftijdsgroep';
   isExternFieldPlaceholder: string = 'Klas of extern';
   hasTrajectoryFieldPlaceholder: string = 'Trajectfunctie';
   hasNormalDayCareFieldPlaceholder: string = 'Opvang normaal';
   hasVacationDayCareFieldPlaceholder: string = 'Opvang vakantie';
   hasResourcesFieldPlaceholder: string = 'Voldoende middelen';

   // Min and Max dates for the birthday datepicker
   birthdayDatePickerMinDate = new Date(1900, 0, 1);
   birthdayDatePickerMaxDate = new Date();

   constructor() {}

   ngOnInit() {
      this.clearFormValues();
   }

   public addNewCareUser() {
      this.clearFormValues();
   }

   public showCareUser(careUser: CareUser){
      this.careUser = careUser;
      this.updateFormValues();
   }

   private updateFormValues(){
      this.lastNameFieldValue = this.careUser.lastName;
      this.firstNameFieldValue = this.careUser.firstName;
      this.birthdayFieldValue = this.careUser.birthday;
      this.caseNumberFieldValue = this.careUser.caseNumber;
      this.ageGroupFieldValue =
         this.careUser.ageGroup === 'Kinderen' ? 'child' : 'youngster';
      this.isExternFieldValue = this.careUser.isExtern ? 'true' : 'false';
      this.hasTrajectoryFieldValue = this.careUser.hasTrajectory ? 'true' : 'false';
      this.hasNormalDayCareFieldValue = this.careUser.hasNormalDayCare
         ? 'true'
         : 'false';
      this.hasVacationDayCareFieldValue = this.careUser.hasVacationDayCare
         ? 'true'
         : 'false';
      this.hasResourcesFieldValue = this.careUser.hasResources ? 'true' : 'false';
   }

   private clearFormValues() {
      this.lastNameFieldValue = '';
      this.firstNameFieldValue = '';
      this.birthdayFieldValue = null;
      this.caseNumberFieldValue = '';
      this.ageGroupFieldValue = '';
      this.isExternFieldValue = '';
      this.hasTrajectoryFieldValue = '';
      this.hasNormalDayCareFieldValue = '';
      this.hasVacationDayCareFieldValue = '';
      this.hasResourcesFieldValue = '';
   }

   private closeForm() {
      this.closeFormEvent.emit(true);
   }
}
