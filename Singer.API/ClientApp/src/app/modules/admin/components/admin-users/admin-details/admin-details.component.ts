import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AdminUser } from 'src/app/modules/core/models/adminuser.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-details',
  templateUrl: './admin-details.component.html',
  styleUrls: ['./admin-details.component.css']
})
export class AdminDetailsComponent implements OnInit {
   @Output() submitEvent: EventEmitter<AdminUser> = new EventEmitter();

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
   emailFieldPlaceholder = 'Email';
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      firstNameFieldControl: new FormControl('', [Validators.required]),
      lastNameFieldControl: new FormControl('', [Validators.required]),
      emailFieldControl: new FormControl('', [Validators.email, Validators.required])
   });
  constructor() { }

  ngOnInit() {
  }

   //#region Error messages for required fields
   getRequiredFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required')
         ? 'Dit veld is verplicht'
         : '';
   }
   getEmailFieldErrorMessage(formControl: FormControl){
      return formControl.hasError('required') ?

       'Dit veld is verplicht' ? formControl.hasError('email')
         : 'Dit is een ongeldig e-mail adres' : '';
   }


}
