import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { AdminUser } from 'src/app/modules/core/models/adminuser.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';
import {
   FormGroup,
   FormControl,
   Validators,
   AbstractControl,
} from '@angular/forms';
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
   readonly firstNameFieldPlaceholder = 'Voornaam';
   readonly lastNameFieldPlaceholder = 'Familienaam';
   readonly emailFieldPlaceholder = 'Email';
   // Form validation values
   readonly maxNameLength = 100;
   readonly minNameLength = 2;
   readonly maxEmailLength = 255;
   readonly nameRegex = /^[\w'À-ÿ][\w' À-ÿ]*[\w'À-ÿ]+$/;

   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      firstNameFieldControl: new FormControl(this.adminUser.firstName, [
         Validators.required,
         Validators.maxLength(this.maxNameLength),
         Validators.minLength(this.minNameLength),
         Validators.pattern(this.nameRegex),
      ]),
      lastNameFieldControl: new FormControl(this.adminUser.lastName, [
         Validators.required,
         Validators.maxLength(this.maxNameLength),
         Validators.minLength(this.minNameLength),
         Validators.pattern(this.nameRegex),
      ]),
      emailFieldControl: new FormControl(this.adminUser.email, [
         Validators.required,
         Validators.maxLength(this.maxEmailLength),
         Validators.email,
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
