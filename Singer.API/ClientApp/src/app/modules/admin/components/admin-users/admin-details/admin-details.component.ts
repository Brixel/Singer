import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { AdminUser } from 'src/app/modules/core/models/adminuser.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
   selector: 'app-admin-details',
   templateUrl: './admin-details.component.html',
   styleUrls: ['./admin-details.component.css'],
})
export class AdminDetailsComponent implements OnInit {
   @Output() submitEvent: EventEmitter<AdminUser> = new EventEmitter();

   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;

   // Boolean to check if changes have been made when editing a user
   isChangesMade: boolean;

   ageGroups = AgeGroup;

   // Current care user instance
   adminUser: AdminUser = <AdminUser>{};

   //#region Binding properties for form:

   // Form placeholders
   firstNameFieldPlaceholder = 'Voornaam';
   lastNameFieldPlaceholder = 'Familienaam';
   emailFieldPlaceholder = 'Email';
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      firstNameFieldControl: new FormControl(this.adminUser.firstName, [
         Validators.required,
      ]),
      lastNameFieldControl: new FormControl(this.adminUser.lastName, [
         Validators.required,
      ]),
      emailFieldControl: new FormControl(this.adminUser.email, [
         Validators.email,
         Validators.required,
      ]),
   });
   constructor(
      public dialogRef: MatDialogRef<AdminDetailsComponent>,
      // Care user that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: AdminUser
   ) {
      this.isAdding = data === null;
      if (!this.isAdding) {
         this.adminUser = data;
      }
   }

   ngOnInit() {
      if (!this.isAdding) {
         this.formControlGroup.controls['firstNameFieldControl'].setValue(
            this.adminUser.firstName
         );
         this.formControlGroup.controls['lastNameFieldControl'].setValue(
            this.adminUser.lastName
         );
         this.formControlGroup.controls['emailFieldControl'].setValue(
            this.adminUser.email
         );
      }
   }

   //#region Error messages for required fields
   getRequiredFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required') ? 'Dit veld is verplicht' : '';
   }
   getEmailFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required')
         ? 'Dit veld is verplicht'
            ? formControl.hasError('email')
            : 'Dit is een ongeldig e-mail adres'
         : '';
   }
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) {
         return;
      }
      this.adminUser.firstName = this.formControlGroup.get(
         'firstNameFieldControl'
      ).value;
      this.adminUser.lastName = this.formControlGroup.get(
         'lastNameFieldControl'
      ).value;
      this.adminUser.email = this.formControlGroup.get(
         'emailFieldControl'
      ).value;
      // Check for changes and determine of an API call is necesarry
      this.submitEvent.emit(this.adminUser);
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
