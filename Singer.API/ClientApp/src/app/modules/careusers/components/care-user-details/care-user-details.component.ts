import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { CareUser } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { FormControl, Validators, FormGroup } from '@angular/forms';
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

   // Form placeholders
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
   birthdayDatePickerMinDate: Date = new Date(1900, 0, 1);
   birthdayDatePickerMaxDate: Date = new Date();

   // Form control group
   formControlGroup: FormGroup = new FormGroup({

      // Form controls
   idFieldControl: new FormControl('', [Validators.required]),
   firstNameFieldControl: new FormControl('', [Validators.required]),
   lastNameFieldControl: new FormControl('', [Validators.required]),
   emailFieldControl: new FormControl('', [
      Validators.required,
      Validators.email,
   ]),
   userNameFieldControl: new FormControl('', [Validators.required]),
   birthdayFieldControl: new FormControl(null, [Validators.required]),
   caseNumberFieldControl: new FormControl('', [Validators.required]),
   ageGroupFieldControl: new FormControl('', [Validators.required]),
   isExternFieldControl: new FormControl('', [Validators.required]),
   hasTrajectoryFieldControl: new FormControl('', [Validators.required]),
   hasNormalDayCareFieldControl: new FormControl('', [Validators.required]),
   hasVacationDayCareFieldControl: new FormControl('', [Validators.required]),
   hasResourcesFieldControl: new FormControl('', [Validators.required]),
   })



   // Error messages for required fields
   getIdFieldErrorMessage() {
      return this.formControlGroup.controls.idFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getFirstNameFieldErrorMessage() {
      return this.formControlGroup.controls.firstNameFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getLastNameFieldErrorMessage() {
      return this.formControlGroup.controls.lastNameFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getEmailFieldErrorMessage() {
      return this.formControlGroup.controls.emailFieldControl.hasError('required')
         ? 'You must enter a value'
         : this.formControlGroup.controls.emailFieldControl.hasError('email')
         ? 'Not a valid email'
         : '';
   }

   getUserNameFieldErrorMessage() {
      return this.formControlGroup.controls.userNameFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getBirthdayErrorMessage() {
      return this.formControlGroup.controls.birthdayFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }


   getCaseNumberFieldErrorMessage() {
      return this.formControlGroup.controls.caseNumberFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getAgeGroupFieldErrorMessage() {
      return this.formControlGroup.controls.ageGroupFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getIsExternFieldErrorMessage() {
      return this.formControlGroup.controls.isExternFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getHasTrajectoryFieldErrorMessage() {
      return this.formControlGroup.controls.hasTrajectoryFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getHasNormalDayCareFieldErrorMessage() {
      return this.formControlGroup.controls.hasNormalDayCareFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getHasVacationDayCareFieldErrorMessage() {
      return this.formControlGroup.controls.hasVacationDayCareFieldControl.hasError('required')
         ? 'You must enter a value'
         : '';
   }

   getHasResourcesFieldErrorMessage() {
      return this.formControlGroup.controls.hasResourcesFieldControl.hasError('required')
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
      this.formControlGroup.controls.idFieldControl.setValue(this.currentCareUserInstance.id);
      this.formControlGroup.controls.firstNameFieldControl.setValue(this.currentCareUserInstance.firstName);
      this.formControlGroup.controls.lastNameFieldControl.setValue(this.currentCareUserInstance.lastName);
      this.formControlGroup.controls.emailFieldControl.setValue(this.currentCareUserInstance.email);
      this.formControlGroup.controls.userNameFieldControl.setValue(this.currentCareUserInstance.userName);
      this.formControlGroup.controls.birthdayFieldControl.setValue(this.currentCareUserInstance.birthday);
      this.formControlGroup.controls.caseNumberFieldControl.setValue(this.currentCareUserInstance.caseNumber);
      this.formControlGroup.controls.ageGroupFieldControl.setValue(
         this.currentCareUserInstance.ageGroup === 'Kinderen'
            ? 'child'
            : 'youngster');
      this.formControlGroup.controls.isExternFieldControl.setValue(this.currentCareUserInstance.isExtern
         ? 'true'
         : 'false');
      this.formControlGroup.controls.hasTrajectoryFieldControl.setValue(this.currentCareUserInstance.hasTrajectory
         ? 'true'
         : 'false');
      this.formControlGroup.controls.hasNormalDayCareFieldControl.setValue(this.currentCareUserInstance
         .hasNormalDayCare
         ? 'true'
         : 'false');
      this.formControlGroup.controls.hasVacationDayCareFieldControl.setValue(this.currentCareUserInstance
         .hasVacationDayCare
         ? 'true'
         : 'false');
      this.formControlGroup.controls.hasResourcesFieldControl.setValue(this.currentCareUserInstance.hasResources
         ? 'true'
         : 'false');
   }

   private resetFormControls() {
      // Clear all form fields
      this.formControlGroup.controls.idFieldControl.reset();
      this.formControlGroup.controls.firstNameFieldControl.reset();
      this.formControlGroup.controls.lastNameFieldControl.reset();
      this.formControlGroup.controls.emailFieldControl.reset();
      this.formControlGroup.controls.birthdayFieldControl.reset();
      this.formControlGroup.controls.caseNumberFieldControl.reset();
      this.formControlGroup.controls.ageGroupFieldControl.reset();
      this.formControlGroup.controls.isExternFieldControl.reset();
      this.formControlGroup.controls.hasTrajectoryFieldControl.reset();
      this.formControlGroup.controls.hasNormalDayCareFieldControl.reset();
      this.formControlGroup.controls.hasVacationDayCareFieldControl.reset();
      this.formControlGroup.controls.hasResourcesFieldControl.reset();
   }

   checkForChanges(): boolean {
      debugger;
      console.log(this.formControlGroup.controls.caseNumberFieldControl.value);
      //If we are editing an existing user and there are no changes return false
      if (this.currentCareUserInstance.id !== this.formControlGroup.controls.idFieldControl.value) return true;
      if (this.currentCareUserInstance.firstName !== this.formControlGroup.controls.firstNameFieldControl.value)
         return true;
      if (this.currentCareUserInstance.lastName !== this.formControlGroup.controls.lastNameFieldControl.value)
         return true;
      if (this.currentCareUserInstance.email !== this.formControlGroup.controls.emailFieldControl.value)
         return true;
      if (this.currentCareUserInstance.birthday !== this.formControlGroup.controls.birthdayFieldControl.value)
         return true;
      if (this.currentCareUserInstance.caseNumber !== this.formControlGroup.controls.caseNumberFieldControl.value)
         return true;
      if (
         this.currentCareUserInstance.ageGroup !==
         (this.formControlGroup.controls.ageGroupFieldControl.value === 'child' ? 'Kinderen' : 'Jongeren')
      )
         return true;
      if (
         this.currentCareUserInstance.isExtern !==
         (this.formControlGroup.controls.isExternFieldControl.value === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasTrajectory !==
         (this.formControlGroup.controls.hasTrajectoryFieldControl.value === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasNormalDayCare !==
         (this.formControlGroup.controls.hasNormalDayCareFieldControl.value === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasVacationDayCare !==
         (this.formControlGroup.controls.hasVacationDayCareFieldControl.value === 'true' ? true : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasResources !==
         (this.formControlGroup.controls.hasResourcesFieldControl.value === 'true' ? true : false)
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
         id: this.formControlGroup.controls.idFieldControl.value,
         firstName: this.formControlGroup.controls.firstNameFieldControl.value,
         lastName: this.formControlGroup.controls.lastNameFieldControl.value,
         email: this.formControlGroup.controls.emailFieldControl.value,
         userName: '',
         birthday: this.formControlGroup.controls.birthdayFieldControl.value,
         caseNumber: this.formControlGroup.controls.caseNumberFieldControl.value,
         ageGroup: this.formControlGroup.controls.ageGroupFieldControl.value,
         isExtern: this.formControlGroup.controls.isExternFieldControl.value === 'true' ? true : false,
         hasTrajectory: this.formControlGroup.controls.hasTrajectoryFieldControl.value === 'true' ? true : false,
         hasNormalDayCare:
            this.formControlGroup.controls.hasNormalDayCareFieldControl.value === 'true' ? true : false,
         hasVacationDayCare:
            this.formControlGroup.controls.hasVacationDayCareFieldControl.value === 'true' ? true : false,
         hasResources: this.formControlGroup.controls.hasResourcesFieldControl.value === 'true' ? true : false,
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
