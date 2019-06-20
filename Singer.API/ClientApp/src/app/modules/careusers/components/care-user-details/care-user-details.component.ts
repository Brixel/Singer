import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { CareUser } from 'src/app/modules/core/services/care-users-api/care-users-api.service';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

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

   // Current care user instance
   currentCareUserInstance: CareUser;

   //#region Binding properties for form:

   // Form placeholders
   firstNameFieldPlaceholder: string = 'Voornaam';
   lastNameFieldPlaceholder: string = 'Familienaam';
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

   //#region Error messages for required fields
   getIdFieldErrorMessage() {
      return this.formControlGroup.controls.idFieldControl.hasError('required')
         ? 'Dit veld is verplicht'
         : '';
   }

   getFirstNameFieldErrorMessage() {
      return this.formControlGroup.controls.firstNameFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getLastNameFieldErrorMessage() {
      return this.formControlGroup.controls.lastNameFieldControl.hasError(
         'required'
      )
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

   getUserNameFieldErrorMessage() {
      return this.formControlGroup.controls.userNameFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getBirthdayErrorMessage() {
      return this.formControlGroup.controls.birthdayFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getCaseNumberFieldErrorMessage() {
      return this.formControlGroup.controls.caseNumberFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getAgeGroupFieldErrorMessage() {
      return this.formControlGroup.controls.ageGroupFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getIsExternFieldErrorMessage() {
      return this.formControlGroup.controls.isExternFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getHasTrajectoryFieldErrorMessage() {
      return this.formControlGroup.controls.hasTrajectoryFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getHasNormalDayCareFieldErrorMessage() {
      return this.formControlGroup.controls.hasNormalDayCareFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getHasVacationDayCareFieldErrorMessage() {
      return this.formControlGroup.controls.hasVacationDayCareFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }

   getHasResourcesFieldErrorMessage() {
      return this.formControlGroup.controls.hasResourcesFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : '';
   }
   //#endregion

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
      this.isAdding
         ? this.resetFormControls()
         : this.loadCurrentCareUserInstanceValues();
   }

   // Fill in the data of the current care usrers instance
   private loadCurrentCareUserInstanceValues() {
      this.formControlGroup.controls.firstNameFieldControl.reset(
         this.currentCareUserInstance.firstName
      );
      this.formControlGroup.controls.lastNameFieldControl.reset(
         this.currentCareUserInstance.lastName
      );
      this.formControlGroup.controls.birthdayFieldControl.reset(
         new Date(this.currentCareUserInstance.birthDay)
      );
      this.formControlGroup.controls.caseNumberFieldControl.reset(
         this.currentCareUserInstance.caseNumber
      );
      this.formControlGroup.controls.ageGroupFieldControl.reset(
         this.currentCareUserInstance.ageGroup == '1' ? '1' : '2'
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

   //If we are editing an existing user and there are no changes return false
   checkForChanges(): boolean {
      if (
         this.currentCareUserInstance.firstName !==
         this.formControlGroup.controls.firstNameFieldControl.value
      )
         return true;
      if (
         this.currentCareUserInstance.lastName !==
         this.formControlGroup.controls.lastNameFieldControl.value
      ) {
         return true;
      }

      var instanceDate = new Date(this.currentCareUserInstance.birthDay);
      var formDate = new Date(
         this.formControlGroup.controls.birthdayFieldControl.value
      );

      if (instanceDate.getFullYear() !== formDate.getFullYear()) return true;
      if (instanceDate.getMonth() !== formDate.getMonth()) return true;
      if (instanceDate.getDay() !== formDate.getDay()) return true;
      if (
         this.currentCareUserInstance.caseNumber !==
         this.formControlGroup.controls.caseNumberFieldControl.value
      )
         return true;
      if (
         this.currentCareUserInstance.ageGroup !=
         this.formControlGroup.controls.ageGroupFieldControl.value
      )
         return true;
      if (
         this.currentCareUserInstance.isExtern !==
         (this.formControlGroup.controls.isExternFieldControl.value === 'true'
            ? true
            : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasTrajectory !==
         (this.formControlGroup.controls.hasTrajectoryFieldControl.value ===
         'true'
            ? true
            : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasNormalDayCare !==
         (this.formControlGroup.controls.hasNormalDayCareFieldControl.value ===
         'true'
            ? true
            : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasVacationDayCare !==
         (this.formControlGroup.controls.hasVacationDayCareFieldControl
            .value === 'true'
            ? true
            : false)
      )
         return true;
      if (
         this.currentCareUserInstance.hasResources !==
         (this.formControlGroup.controls.hasResourcesFieldControl.value ===
         'true'
            ? true
            : false)
      )
         return true;
      return false;
   }

   // Load form field values into current care user instance
   private updateCurrentCareUserInstance() {
      this.currentCareUserInstance = {
         id: this.currentCareUserInstance.id,
         firstName: this.formControlGroup.controls.firstNameFieldControl.value,
         lastName: this.formControlGroup.controls.lastNameFieldControl.value,
         email: this.currentCareUserInstance.email,
         userName: this.currentCareUserInstance.userName,
         birthDay: this.formControlGroup.controls.birthdayFieldControl.value,
         caseNumber: this.formControlGroup.controls.caseNumberFieldControl
            .value,
         ageGroup: this.formControlGroup.controls.ageGroupFieldControl.value,
         isExtern:
            this.formControlGroup.controls.isExternFieldControl.value === 'true'
               ? true
               : false,
         hasTrajectory:
            this.formControlGroup.controls.hasTrajectoryFieldControl.value ===
            'true'
               ? true
               : false,
         hasNormalDayCare:
            this.formControlGroup.controls.hasNormalDayCareFieldControl
               .value === 'true'
               ? true
               : false,
         hasVacationDayCare:
            this.formControlGroup.controls.hasVacationDayCareFieldControl
               .value === 'true'
               ? true
               : false,
         hasResources:
            this.formControlGroup.controls.hasResourcesFieldControl.value ===
            'true'
               ? true
               : false,
      };
   }

   // Submit the form
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) return;

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
