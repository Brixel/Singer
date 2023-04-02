import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { AdminUser } from 'src/app/modules/core/models/adminuser.model';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
   selector: 'app-admin-details',
   templateUrl: './admin-details.component.html',
   styleUrls: ['./admin-details.component.css'],
})
export class AdminDetailsComponent implements OnInit {
   // Submit event for when the user submits the form
   @Output() submitEvent: EventEmitter<AdminUser> = new EventEmitter();
   @Output() deleteEvent: EventEmitter<AdminUser> = new EventEmitter();

   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;

   // Boolean to check if changes have been made when editing a user
   isChangesMade: boolean;

   // Current admin user instance
   adminUser: AdminUser;

   // Form placeholders
   readonly firstNameFieldPlaceholder = 'Voornaam';
   readonly lastNameFieldPlaceholder = 'Familienaam';
   readonly emailFieldPlaceholder = 'Email';

   // Form validation values
   readonly maxNameLength = 100;
   readonly minNameLength = 2;
   readonly maxEmailLength = 255;
   readonly nameRegex = /^[\w'À-ÿ][\w' À-ÿ]*[\w'À-ÿ]+$/;

   // Form control group
   formControlGroup: UntypedFormGroup = new UntypedFormGroup({
      // Form controls
      firstNameFieldControl: new UntypedFormControl([
         Validators.required,
         Validators.maxLength(this.maxNameLength),
         Validators.minLength(this.minNameLength),
         Validators.pattern(this.nameRegex),
      ]),
      lastNameFieldControl: new UntypedFormControl([
         Validators.required,
         Validators.maxLength(this.maxNameLength),
         Validators.minLength(this.minNameLength),
         Validators.pattern(this.nameRegex),
      ]),
      emailFieldControl: new UntypedFormControl([
         Validators.required,
         Validators.maxLength(this.maxEmailLength),
         Validators.email,
      ]),
   });

   constructor(
      // Dialogreference to close this dialog
      public dialogRef: MatDialogRef<AdminDetailsComponent>,
      // Admin user that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: AdminUser
   ) {
      this.adminUser = data;
      this.isAdding = data === null;
   }

   ngOnInit() {
      // If we are adding a new user then clear all fields
      // If we are editing an existing user then fill in his data
      if (this.isAdding) {
         this.formControlGroup.reset();
         this.adminUser = <AdminUser>{};
      } else {
         this.loadCurrentAdminUserInstanceValues();
      }
   }

   private loadCurrentAdminUserInstanceValues() {
      this.formControlGroup.controls['firstNameFieldControl'].setValue(this.adminUser.firstName);
      this.formControlGroup.controls['lastNameFieldControl'].setValue(this.adminUser.lastName);
      this.formControlGroup.controls['emailFieldControl'].setValue(this.adminUser.email);
   }

   // Load form field values into current admin user instance
   private updateCurrentAdminUserInstance() {
      this.adminUser.firstName = this.formControlGroup.get('firstNameFieldControl').value;
      this.adminUser.lastName = this.formControlGroup.get('lastNameFieldControl').value;
      this.adminUser.email = this.formControlGroup.get('emailFieldControl').value;
   }

   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) {
         return;
      }

      this.updateCurrentAdminUserInstance();
      this.submitEvent.emit(this.adminUser);
      this.closeForm();
   }

   emitDeleteEvent() {
      this.deleteEvent.emit(this.adminUser);
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
