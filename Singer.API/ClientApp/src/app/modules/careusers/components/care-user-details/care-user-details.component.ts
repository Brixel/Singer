import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';

// Data we pass along with the creation of the Mat-Dialog box
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
   // Submit event for when the user submits the form
   @Output() submitEvent: EventEmitter<CareUser> = new EventEmitter();

   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;

   // Boolean to check if changes have been made when editing a user
   isChangesMade: boolean;

   ageGroups = AgeGroup;

   // Current care user instance
   currentCareUserInstance: CareUser;

   //#region Binding properties for form:

   // Form placeholders
   firstNameFieldPlaceholder = 'Voornaam';
   lastNameFieldPlaceholder = 'Familienaam';
   birthdayFieldPlaceholder = 'Geboortedatum';
   caseNumberFieldPlaceholder = 'Dossiernr';
   ageGroupFieldPlaceholder = 'Leeftijdsgroep';
   isExternFieldPlaceholder = 'Klas of extern';
   hasTrajectoryFieldPlaceholder = 'Trajectfunctie';
   hasNormalDayCareFieldPlaceholder = 'Opvang normaal';
   hasVacationDayCareFieldPlaceholder = 'Opvang vakantie';
   hasResourcesFieldPlaceholder = 'Voldoende middelen';

   // Min and Max dates for the birthday datepicker
   birthdayDatePickerMinDate: Date = new Date(1900, 0, 1);
   birthdayDatePickerMaxDate: Date = new Date();

   // Form control group
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      firstNameFieldControl: new FormControl('', [Validators.required]),
      lastNameFieldControl: new FormControl('', [Validators.required]),
      birthdayFieldControl: new FormControl(null, [Validators.required]),
      caseNumberFieldControl: new FormControl('', [Validators.required]),
      ageGroupFieldControl: new FormControl('', [Validators.required]),
      isExternFieldControl: new FormControl('', [Validators.required]),
      hasTrajectoryFieldControl: new FormControl('', [Validators.required]),
      hasNormalDayCareFieldControl: new FormControl('', [Validators.required]),
      hasVacationDayCareFieldControl: new FormControl('', [
         Validators.required,
      ]),
      hasResourcesFieldControl: new FormControl('', [Validators.required]),
   });

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
      // If we are adding a new user then clear all fields
      // If we are editing an existing user then fill in his data

      if (this.isAdding) {
         this.resetFormControls();
         this.createEmptyUser();
      } else {
         this.loadCurrentCareUserInstanceValues();
      }
   }

   //#region Error messages for required fields
   getRequiredFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required')
         ? 'Dit veld is verplicht'
         : '';
   }

   getEmailFieldErrorMessage() {
      return this.formControlGroup.controls.emailFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : this.formControlGroup.controls.emailFieldControl.hasError('email')
         ? 'Dit is geen geldig email adres'
         : '';
   }
   //#endregion

   // Fill in the data of the current care usrers instance
   private loadCurrentCareUserInstanceValues() {
      this.formControlGroup.controls.firstNameFieldControl.reset(
         this.currentCareUserInstance.firstName
      );
      this.formControlGroup.controls.lastNameFieldControl.reset(
         this.currentCareUserInstance.lastName
      );
      this.formControlGroup.controls.birthdayFieldControl.reset(
         this.currentCareUserInstance.birthDay
      );
      this.formControlGroup.controls.caseNumberFieldControl.reset(
         this.currentCareUserInstance.caseNumber
      );
      this.formControlGroup.controls.ageGroupFieldControl.setValue(
         this.currentCareUserInstance.ageGroup
      );
      this.formControlGroup.controls.isExternFieldControl.reset(
         this.currentCareUserInstance.isExtern ? 'true' : 'false'
      );
      this.formControlGroup.controls.hasTrajectoryFieldControl.reset(
         this.currentCareUserInstance.hasTrajectory ? 'true' : 'false'
      );
      this.formControlGroup.controls.hasNormalDayCareFieldControl.reset(
         this.currentCareUserInstance.hasNormalDayCare ? 'true' : 'false'
      );
      this.formControlGroup.controls.hasVacationDayCareFieldControl.reset(
         this.currentCareUserInstance.hasVacationDayCare ? 'true' : 'false'
      );
      this.formControlGroup.controls.hasResourcesFieldControl.reset(
         this.currentCareUserInstance.hasResources ? 'true' : 'false'
      );
   }

   // Clear all form fields
   private resetFormControls() {
      this.formControlGroup.controls.firstNameFieldControl.reset();
      this.formControlGroup.controls.lastNameFieldControl.reset();
      this.formControlGroup.controls.birthdayFieldControl.reset();
      this.formControlGroup.controls.caseNumberFieldControl.reset();
      this.formControlGroup.controls.ageGroupFieldControl.reset();
      this.formControlGroup.controls.isExternFieldControl.reset();
      this.formControlGroup.controls.hasTrajectoryFieldControl.reset();
      this.formControlGroup.controls.hasNormalDayCareFieldControl.reset();
      this.formControlGroup.controls.hasVacationDayCareFieldControl.reset();
      this.formControlGroup.controls.hasResourcesFieldControl.reset();
   }

   createEmptyUser() {
      this.currentCareUserInstance = {
         id: '',
         firstName: '',
         lastName: '',
         email: '',
         userName: '',
         birthDay: new Date(),
         caseNumber: '',
         ageGroup: AgeGroup.Toddler,
         isExtern: false,
         hasTrajectory: false,
         hasNormalDayCare: false,
         hasVacationDayCare: false,
         hasResources: false,
      };
   }

   // If we are editing an existing user and there are no changes return false
   checkForChanges(): boolean {
      if (this.isAdding) return true;
      if (
         this.currentCareUserInstance.firstName !==
         this.formControlGroup.controls.firstNameFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.lastName !==
         this.formControlGroup.controls.lastNameFieldControl.value
      ) {
         return true;
      }

      const instanceDate = new Date(this.currentCareUserInstance.birthDay);
      const formDate = new Date(
         this.formControlGroup.controls.birthdayFieldControl.value
      );

      if (instanceDate.getFullYear() !== formDate.getFullYear()) { return true; }
      if (instanceDate.getMonth() !== formDate.getMonth()) { return true; }
      if (instanceDate.getDay() !== formDate.getDay()) { return true; }
      if (
         this.currentCareUserInstance.caseNumber !==
         this.formControlGroup.controls.caseNumberFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.ageGroup !==         this.formControlGroup.controls.ageGroupFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.isExtern !==
         (this.formControlGroup.controls.isExternFieldControl.value === 'true'
            ? true
            : false)
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.hasTrajectory !==
         (this.formControlGroup.controls.hasTrajectoryFieldControl.value ===
         'true'
            ? true
            : false)
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.hasNormalDayCare !==
         (this.formControlGroup.controls.hasNormalDayCareFieldControl.value ===
         'true'
            ? true
            : false)
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.hasVacationDayCare !==
         (this.formControlGroup.controls.hasVacationDayCareFieldControl
            .value === 'true'
            ? true
            : false)
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.hasResources !==
         (this.formControlGroup.controls.hasResourcesFieldControl.value ===
         'true'
            ? true
            : false)
      ) {
         return true;
      }
      return false;
   }

   // Load form field values into current care user instance
   private updateCurrentCareUserInstance() {
      this.currentCareUserInstance.firstName = this.formControlGroup.controls.firstNameFieldControl.value;
      this.currentCareUserInstance.lastName = this.formControlGroup.controls.lastNameFieldControl.value;
      this.currentCareUserInstance.birthDay = this.formControlGroup.controls.birthdayFieldControl.value;
      this.currentCareUserInstance.caseNumber = this.formControlGroup.controls.caseNumberFieldControl.value;
      this.currentCareUserInstance.ageGroup = this.formControlGroup.controls.ageGroupFieldControl.value;
      this.currentCareUserInstance.isExtern = this.formControlGroup.controls.isExternFieldControl.value === 'true'
      ? true
      : false;
      this.currentCareUserInstance.hasTrajectory = this.formControlGroup.controls.hasTrajectoryFieldControl.value ===
      'true'
         ? true
         : false;
      this.currentCareUserInstance.hasNormalDayCare = this.formControlGroup.controls.hasNormalDayCareFieldControl
      .value === 'true'
      ? true
      : false;
      this.currentCareUserInstance.hasVacationDayCare = this.formControlGroup.controls.hasVacationDayCareFieldControl
      .value === 'true'
      ? true
      : false;
      this.currentCareUserInstance.hasResources = this.formControlGroup.controls.hasResourcesFieldControl.value ===
      'true'
         ? true
         : false;
   }

   // Submit the form
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) { return; }

      // Check for changes and determine of an API call is necesarry
      if (this.checkForChanges()) {
         this.updateCurrentCareUserInstance();
         this.submitEvent.emit(this.currentCareUserInstance);
      }
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
