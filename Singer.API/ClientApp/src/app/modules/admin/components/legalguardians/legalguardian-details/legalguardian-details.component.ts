import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MaterialModule } from '../../../../../material.module';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

// Data we pass along with the creation of the Mat-Dialog box
export interface LegalGuardianDetailsFormData {
   legalGuardianInstance: LegalGuardian;
   isAdding: boolean;
}

@Component({
   selector: 'app-legalguardian-details',
   templateUrl: './legalguardian-details.component.html',
   styleUrls: ['./legalguardian-details.component.css'],
})
export class LegalguardianDetailsComponent implements OnInit {
   // Submit event for when the user submits the form
   @Output() submitEvent: EventEmitter<LegalGuardian> = new EventEmitter();

   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;

   // Boolean to check if changes have been made when editing a user
   isChangesMade: boolean;

   // Current legal guardian instance
   currentLegalGuardianInstance: LegalGuardian;

   //#region Binding properties for form:

   // Form placeholders
   firstNameFieldPlaceholder = 'Voornaam';
   lastNameFieldPlaceholder = 'Familienaam';
   birthDateFieldPlaceholder = 'Geboortedatum';
   addressFieldPlaceholder = 'Adres';
   userNameFieldPlaceholder = 'Gebruikersnaam';
   emailFieldPlaceholder = 'E-mail';
   phoneNumberFieldPlaceholder = 'Telefoon nr.';
   gsmFieldPlaceholder = 'Gsm nr.';

   // Min and Max dates for the birthday datepicker
   birthDateDatePickerMinDate: Date = new Date(1900, 0, 1);
   birthDateDatePickerMaxDate: Date = new Date();

   // Form control group
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      firstNameFieldControl: new FormControl('', [Validators.required]),
      lastNameFieldControl: new FormControl('', [Validators.required]),
      birthDateFieldControl: new FormControl(null, [Validators.required]),
      addressFieldControl: new FormControl('', [Validators.required]),
      userNameFieldControl: new FormControl('', [Validators.required]),
      emailFieldControl: new FormControl('', [
         Validators.required,
         Validators.email,
      ]),
      phoneNumberFieldControl: new FormControl('', [Validators.required]),
      gsmFieldControl: new FormControl('', []),
   });

   //#endregion

   constructor(
      // dialogreference to close this dialog
      public dialogRef: MatDialogRef<LegalguardianDetailsComponent>,
      // Care user that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: LegalGuardianDetailsFormData
   ) {
      this.currentLegalGuardianInstance = data.legalGuardianInstance;
      this.isAdding = data.isAdding;
   }

   ngOnInit() {
      // If we are adding a new user then clear all fields
      // If we are editing an existing user then fill in his data

      if (this.isAdding) {
         this.resetFormControls();
         this.createEmptyGuardian();
      } else {
         this.loadCurrentLegalGuardianInstanceValues();
      }
   }

   //#region Error messages for required fields
   getRequiredFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required') ? 'Dit veld is verplicht' : '';
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
   private loadCurrentLegalGuardianInstanceValues() {
      this.formControlGroup.controls.firstNameFieldControl.reset(
         this.currentLegalGuardianInstance.firstName
      );
      this.formControlGroup.controls.lastNameFieldControl.reset(
         this.currentLegalGuardianInstance.lastName
      );
      this.formControlGroup.controls.birthdayFieldControl.reset(
         this.currentLegalGuardianInstance.birthDate
      );
      this.formControlGroup.controls.addressFieldControl.reset(
         this.currentLegalGuardianInstance.address
      );
      this.formControlGroup.controls.userNameFieldControl.reset(
         this.currentLegalGuardianInstance.userName
      );
      this.formControlGroup.controls.emailFieldControl.reset(
         this.currentLegalGuardianInstance.email
      );
      this.formControlGroup.controls.phoneNumberFieldControl.reset(
         this.currentLegalGuardianInstance.phoneNumber
      );
      this.formControlGroup.controls.gsmFieldControl.reset(
         this.currentLegalGuardianInstance.gsm
      );
   }

   // Clear all form fields
   private resetFormControls() {
      this.formControlGroup.controls.firstNameFieldControl.reset();
      this.formControlGroup.controls.lastNameFieldControl.reset();
      this.formControlGroup.controls.birthdayFieldControl.reset();
      this.formControlGroup.controls.addressFieldControl.reset();
      this.formControlGroup.controls.userNameFieldControl.reset();
      this.formControlGroup.controls.emailFieldControl.reset();
      this.formControlGroup.controls.phoneNumberFieldControl.reset();
      this.formControlGroup.controls.gsmFieldControl.reset();
   }

   createEmptyGuardian() {
      this.currentLegalGuardianInstance = {
         id: '',
         firstName: '',
         lastName: '',
         email: '',
         userName: '',
         birthDate: new Date(),
         address: '',
         phoneNumber: '',
         gsm: '',
      };
   }

   // If we are editing an existing user and there are no changes return false
   checkForChanges(): boolean {
      if (this.isAdding) return true;

      if (
         this.currentLegalGuardianInstance.firstName !==
         this.formControlGroup.controls.firstNameFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentLegalGuardianInstance.lastName !==
         this.formControlGroup.controls.lastNameFieldControl.value
      ) {
         return true;
      }

      const instanceDate = new Date(
         this.currentLegalGuardianInstance.birthDate
      );
      const formDate = new Date(
         this.formControlGroup.controls.birthDateFieldControl.value
      );

      if (instanceDate.getFullYear() !== formDate.getFullYear()) {
         return true;
      }
      if (instanceDate.getMonth() !== formDate.getMonth()) {
         return true;
      }
      if (instanceDate.getDay() !== formDate.getDay()) {
         return true;
      }

      if (
         this.currentLegalGuardianInstance.address !==
         this.formControlGroup.controls.addressFieldControl.value
      ) {
         return true;
      }

      if (
         this.currentLegalGuardianInstance.userName !==
         this.formControlGroup.controls.userNameFieldControl.value
      ) {
         return true;
      }

      if (
         this.currentLegalGuardianInstance.email !==
         this.formControlGroup.controls.emailFieldControl.value
      ) {
         return true;
      }

      if (
         this.currentLegalGuardianInstance.phoneNumber !==
         this.formControlGroup.controls.phoneNumberFieldControl.value
      ) {
         return true;
      }

      if (
         this.currentLegalGuardianInstance.gsm !==
         this.formControlGroup.controls.gsmFieldControl.value
      ) {
         return true;
      }

      return false;
   }

   // Load form field values into current legal guardian instance
   private updateCurrentLegalGuardianInstance() {
      this.currentLegalGuardianInstance.firstName = this.formControlGroup.controls.firstNameFieldControl.value;
      this.currentLegalGuardianInstance.lastName = this.formControlGroup.controls.lastNameFieldControl.value;
      this.currentLegalGuardianInstance.birthDate = this.formControlGroup.controls.birthDateFieldControl.value;
      this.currentLegalGuardianInstance.address = this.formControlGroup.controls.addressFieldControl.value;
      this.currentLegalGuardianInstance.userName = this.formControlGroup.controls.userNameFieldControl.value;
      this.currentLegalGuardianInstance.email = this.formControlGroup.controls.emailFieldControl.value;
      this.currentLegalGuardianInstance.phoneNumber = this.formControlGroup.controls.phoneNumberFieldControl.value;
      this.currentLegalGuardianInstance.gsm = this.formControlGroup.controls.gsmFieldControl.value;
   }

   // Submit the form
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) { return; }

      // Check for changes and determine of an API call is necesarry
      if (this.checkForChanges()) {
         this.updateCurrentLegalGuardianInstance();
         this.submitEvent.emit(this.currentLegalGuardianInstance);
      }
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
