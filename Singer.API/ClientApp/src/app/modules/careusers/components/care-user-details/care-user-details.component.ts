import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { CareUser } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface CareUserDetailsFormData {
   careUserInstance: CareUser;
   isAdding: boolean;
}

@Component({
   selector: 'app-care-user-details',
   templateUrl: './care-user-details.component.html',
   styleUrls: ['./care-user-details.component.css'],
})
export class CareUserDetailsComponent implements OnInit {
   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;

   // Boolean to check if changes have been made when editing a user
   isChangesMade: boolean;

   // Current care user instance
   currentCareUserInstance: CareUser;

   //#region Binding properties for form:

   // Form Values
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

   // From placeholders
   idFieldPlaceholder: string = 'ID';
   firstNameFieldPlaceholder: string = 'Voornaam';
   lastNameFieldPlaceholder: string = 'Familienaam';
   emailFieldPlaceholder: string = 'email';
   userNameFieldPlaceholder: string = 'Gebruikersnaam';
   birthdayFieldPlaceholder: string = 'Geboortedatum';
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

   // Form controls
   emailFieldControl = new FormControl('', [
      Validators.required,
      Validators.email,
   ]);
   caseNumberFieldControl = new FormControl('', [Validators.required]);

   // Error messages for required fields
   getEmailFieldErrorMessage() {
      return this.emailFieldControl.hasError('required')
         ? 'You must enter a value'
         : this.emailFieldControl.hasError('email')
         ? 'Not a valid email'
         : '';
   }
   getCaseNumberFieldErrorMessage() {
      return this.caseNumberFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   //#endregion

   constructor(
      // dialogreference to close this dialog
      public dialogRef: MatDialogRef<CareUserDetailsComponent>,
      // Care user that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: CareUserDetailsFormData
   ) {
      this.currentCareUserInstance = data.careUserInstance;
      this.isAdding = data.isAdding;
   }

   ngOnInit() {
      this.isAdding ? this.clearFormValues() : this.updateFormValues();
   }

   private updateFormValues() {
      this.idFieldValue = this.currentCareUserInstance.id;
      this.firstNameFieldValue = this.currentCareUserInstance.firstName;
      this.lastNameFieldValue = this.currentCareUserInstance.lastName;
      this.emailFieldValue = this.currentCareUserInstance.email;
      this.userNameFieldValue = this.currentCareUserInstance.userName;
      this.birthdayFieldValue = this.currentCareUserInstance.birthday;
      this.caseNumberFieldValue = this.currentCareUserInstance.caseNumber;
      this.ageGroupFieldValue =
         this.currentCareUserInstance.ageGroup === 'Kinderen'
            ? 'child'
            : 'youngster';
      this.isExternFieldValue = this.currentCareUserInstance.isExtern
         ? 'true'
         : 'false';
      this.hasTrajectoryFieldValue = this.currentCareUserInstance.hasTrajectory
         ? 'true'
         : 'false';
      this.hasNormalDayCareFieldValue = this.currentCareUserInstance
         .hasNormalDayCare
         ? 'true'
         : 'false';
      this.hasVacationDayCareFieldValue = this.currentCareUserInstance
         .hasVacationDayCare
         ? 'true'
         : 'false';
      this.hasResourcesFieldValue = this.currentCareUserInstance.hasResources
         ? 'true'
         : 'false';
   }

   private clearFormValues() {
      // Clear all form fields
      this.idFieldValue = '';
      this.firstNameFieldValue = '';
      this.lastNameFieldValue = '';
      this.emailFieldValue = '';
      this.birthdayFieldValue = null;
      this.caseNumberFieldValue = '';
      this.ageGroupFieldValue = '';
      this.isExternFieldValue = '';
      this.hasTrajectoryFieldValue = '';
      this.hasNormalDayCareFieldValue = '';
      this.hasVacationDayCareFieldValue = '';
      this.hasResourcesFieldValue = '';
      //Reset Form Controls
      this.resetFormControls();
   }

   private resetFormControls() {
      this.emailFieldControl.reset();
      this.caseNumberFieldControl.reset();
   }

   checkForChanges(): boolean {
      debugger;
      console.log(this.caseNumberFieldControl.value);
      //If we are editing an existing user and there are no changes return false
      if (this.currentCareUserInstance.id !== this.idFieldValue) return true;
      if (this.currentCareUserInstance.firstName !== this.firstNameFieldValue)
         return true;
      if (this.currentCareUserInstance.lastName !== this.lastNameFieldValue)
         return true;
      if (this.currentCareUserInstance.email !== this.emailFieldValue)
         return true;
      if (this.currentCareUserInstance.birthday !== this.birthdayFieldValue)
         return true;
      if (this.currentCareUserInstance.caseNumber !== this.caseNumberFieldValue)
         return true;
      if (
         this.currentCareUserInstance.ageGroup !==
         (this.ageGroupFieldValue === 'child' ? 'Kinderen' : 'Jongeren')
      )
         return true;
      if (
         this.currentCareUserInstance.isExtern !==
         (this.isExternFieldValue === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasTrajectory !==
         (this.hasTrajectoryFieldValue === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasNormalDayCare !==
         (this.hasNormalDayCareFieldValue === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasVacationDayCare !==
         (this.hasVacationDayCareFieldValue === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasResources !==
         (this.hasResourcesFieldValue === 'true' ? true : false)
      )
         return true;
      return false;
   }

   onKeyUp() {
      // If a key is lifted and we are editing an existing user then check for changes
      if (!this.isAdding) this.isChangesMade = this.checkForChanges();
      console.log('onKeyUp was fired');
      console.log(this.isChangesMade);
   }

   private updateCurrentCareUserInstance() {
      this.currentCareUserInstance = {
         id: this.idFieldValue,
         firstName: this.firstNameFieldValue,
         lastName: this.lastNameFieldValue,
         email: this.emailFieldValue,
         userName: '',
         birthday: this.birthdayFieldValue,
         caseNumber: this.caseNumberFieldValue,
         ageGroup: this.ageGroupFieldValue,
         isExtern: this.isExternFieldValue === 'true' ? true : false,
         hasTrajectory: this.hasTrajectoryFieldValue === 'true' ? true : false,
         hasNormalDayCare:
            this.hasNormalDayCareFieldValue === 'true' ? true : false,
         hasVacationDayCare:
            this.hasVacationDayCareFieldValue === 'true' ? true : false,
         hasResources: this.hasResourcesFieldValue === 'true' ? true : false,
      };
   }

   submitForm() {
      this.updateCurrentCareUserInstance();
      this.dialogRef.close(this.currentCareUserInstance);
   }

   closeForm() {
      this.dialogRef.close();
   }
}
