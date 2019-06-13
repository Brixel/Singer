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
   idFieldControl = new FormControl('', [Validators.required]);
   firstNameFieldControl = new FormControl('', [Validators.required]);
   lastNameFieldControl = new FormControl('', [Validators.required]);
   emailFieldControl = new FormControl('', [
      Validators.required,
      Validators.email,
   ]);
   userNameFieldControl = new FormControl('', [Validators.required]);
   birthdayFieldControl = new FormControl('', [Validators.required]);
   caseNumberFieldControl = new FormControl('', [Validators.required]);
   ageGroupFieldControl = new FormControl('', [Validators.required]);
   isExternFieldControl = new FormControl('', [Validators.required]);
   hasTrajectoryFieldControl = new FormControl('', [Validators.required]);
   hasNormalDayCareFieldControl = new FormControl('', [Validators.required]);
   hasVacationDayCareFieldControl = new FormControl('', [Validators.required]);
   hasResourcesFieldControl = new FormControl('', [Validators.required]);

   // Error messages for required fields
   getIdFieldErrorMessage() {
      return this.idFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getFirstNameFieldErrorMessage() {
      return this.firstNameFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getLastNameFieldErrorMessage() {
      return this.lastNameFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getEmailFieldErrorMessage() {
      return this.emailFieldControl.hasError('required')
         ? 'You must enter a value'
         : this.emailFieldControl.hasError('email')
         ? 'Not a valid email'
         : '';
   }

   getUserNameFieldErrorMessage() {
      return this.userNameFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getBirthdayErrorMessage() {
      return this.birthdayFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }


   getCaseNumberFieldErrorMessage() {
      return this.caseNumberFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getAgeGroupFieldErrorMessage() {
      return this.ageGroupFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getIsExternFieldErrorMessage() {
      return this.isExternFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getHasTrajectoryFieldErrorMessage() {
      return this.hasTrajectoryFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getHasNormalDayCareFieldErrorMessage() {
      return this.hasNormalDayCareFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getHasVacationDayCareFieldErrorMessage() {
      return this.hasVacationDayCareFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getHasResourcesFieldErrorMessage() {
      return this.hasResourcesFieldControl.hasError('required')
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
      this.isAdding ? this.resetFormControls() : this.updateFormValues();
   }

   private updateFormValues() {
      this.idFieldControl.setValue(this.currentCareUserInstance.id);
      this.firstNameFieldControl.setValue(this.currentCareUserInstance.firstName);
      this.lastNameFieldControl.setValue(this.currentCareUserInstance.lastName);
      this.emailFieldControl.setValue(this.currentCareUserInstance.email);
      this.userNameFieldControl.setValue(this.currentCareUserInstance.userName);
      this.birthdayFieldControl.setValue(this.currentCareUserInstance.birthday);
      this.caseNumberFieldControl.setValue(this.currentCareUserInstance.caseNumber);
      this.ageGroupFieldControl.setValue(
         this.currentCareUserInstance.ageGroup === 'Kinderen'
            ? 'child'
            : 'youngster');
      this.isExternFieldControl.setValue(this.currentCareUserInstance.isExtern
         ? 'true'
         : 'false');
      this.hasTrajectoryFieldControl.setValue(this.currentCareUserInstance.hasTrajectory
         ? 'true'
         : 'false');
      this.hasNormalDayCareFieldControl.setValue(this.currentCareUserInstance
         .hasNormalDayCare
         ? 'true'
         : 'false');
      this.hasVacationDayCareFieldControl.setValue(this.currentCareUserInstance
         .hasVacationDayCare
         ? 'true'
         : 'false');
      this.hasResourcesFieldControl.setValue(this.currentCareUserInstance.hasResources
         ? 'true'
         : 'false');
   }

   private resetFormControls() {
      // Clear all form fields
      this.idFieldControl.reset();
      this.firstNameFieldControl.reset();
      this.lastNameFieldControl.reset();
      this.emailFieldControl.reset();
      this.birthdayFieldControl.reset();
      this.caseNumberFieldControl.reset();
      this.ageGroupFieldControl.reset();
      this.isExternFieldControl.reset();
      this.hasTrajectoryFieldControl.reset();
      this.hasNormalDayCareFieldControl.reset();
      this.hasVacationDayCareFieldControl.reset();
      this.hasResourcesFieldControl.reset();
   }

   checkForChanges(): boolean {
      debugger;
      console.log(this.caseNumberFieldControl.value);
      //If we are editing an existing user and there are no changes return false
      if (this.currentCareUserInstance.id !== this.idFieldControl.value) return true;
      if (this.currentCareUserInstance.firstName !== this.firstNameFieldControl.value)
         return true;
      if (this.currentCareUserInstance.lastName !== this.lastNameFieldControl.value)
         return true;
      if (this.currentCareUserInstance.email !== this.emailFieldControl.value)
         return true;
      if (this.currentCareUserInstance.birthday !== this.birthdayFieldControl.value)
         return true;
      if (this.currentCareUserInstance.caseNumber !== this.caseNumberFieldControl.value)
         return true;
      if (
         this.currentCareUserInstance.ageGroup !==
         (this.ageGroupFieldControl.value === 'child' ? 'Kinderen' : 'Jongeren')
      )
         return true;
      if (
         this.currentCareUserInstance.isExtern !==
         (this.isExternFieldControl.value === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasTrajectory !==
         (this.hasTrajectoryFieldControl.value === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasNormalDayCare !==
         (this.hasNormalDayCareFieldControl.value === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasVacationDayCare !==
         (this.hasVacationDayCareFieldControl.value === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasResources !==
         (this.hasResourcesFieldControl.value === 'true' ? true : false)
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
         id: this.idFieldControl.value,
         firstName: this.firstNameFieldControl.value,
         lastName: this.lastNameFieldControl.value,
         email: this.emailFieldControl.value,
         userName: '',
         birthday: this.birthdayFieldControl.value,
         caseNumber: this.caseNumberFieldControl.value,
         ageGroup: this.ageGroupFieldControl.value,
         isExtern: this.isExternFieldControl.value === 'true' ? true : false,
         hasTrajectory: this.hasTrajectoryFieldControl.value === 'true' ? true : false,
         hasNormalDayCare:
            this.hasNormalDayCareFieldControl.value === 'true' ? true : false,
         hasVacationDayCare:
            this.hasVacationDayCareFieldControl.value === 'true' ? true : false,
         hasResources: this.hasResourcesFieldControl.value === 'true' ? true : false,
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
