import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import {
   startWith,
   debounceTime,
   switchMap,
   catchError,
   map,
} from 'rxjs/operators';
import { of, Observable } from 'rxjs';

// Data we pass along with the creation of the Mat-Dialog box
export interface LegalGuardianDetailsFormData {
   legalGuardianInstance: LegalGuardian;
   displayLinkedUserFields: boolean;
}

@Component({
   selector: 'app-legalguardian-details',
   templateUrl: './legalguardian-details.component.html',
   styleUrls: ['./legalguardian-details.component.css'],
})
export class LegalguardianDetailsComponent implements OnInit {

   // Submit event for when the user submits the form
   @Output() submitEvent: EventEmitter<LegalGuardian> = new EventEmitter();
   @Output() deleteEvent: EventEmitter<LegalGuardian> = new EventEmitter();

   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;
   // Boolean to decide if we want to show the careuser fields
   displayLinkedUserFields: boolean;

   // Current legal guardian instance
   currentLegalGuardianInstance: LegalGuardian;

   // For holding result from careuser lookup change events
   public careUsersAutoComplete$: Observable<CareUser[]> = null;

   // Number of colums for careuser user cards
   columns: number;

   // Form placeholders
   readonly firstNameFieldPlaceholder = 'Voornaam';
   readonly lastNameFieldPlaceholder = 'Familienaam';
   readonly addressFieldPlaceholder = 'Straat en huisnummer (+ busnr)';
   readonly postalCodeFieldPlaceholder = 'Postcode';
   readonly cityFieldPlaceholder = 'Gemeente';
   readonly countryFieldPlaceholder = 'Land';
   readonly emailFieldPlaceholder = 'E-mail';

   // Form validation values
   readonly maxNameLength = 100;
   readonly minNameLength = 2;
   readonly maxAddressLength = 100;
   readonly maxPostalCodeLength = 10;
   readonly maxCityLength = 100;
   readonly maxCountryLength = 100;
   readonly maxEmailLength = 255;
   readonly nameRegex = /^[\w'À-ÿ][\w' À-ÿ]*[\w'À-ÿ]+$/;

   // Form control group
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      firstNameFieldControl: new FormControl('', [
         Validators.required,
         Validators.minLength(this.minNameLength),
         Validators.maxLength(this.maxNameLength),
         Validators.pattern(this.nameRegex),
      ]),
      lastNameFieldControl: new FormControl('', [
         Validators.required,
         Validators.minLength(this.minNameLength),
         Validators.maxLength(this.maxNameLength),
         Validators.pattern(this.nameRegex),
      ]),
      addressFieldControl: new FormControl('', [
         Validators.required,
         Validators.maxLength(this.maxAddressLength),
      ]),
      postalCodeFieldControl: new FormControl('', [
         Validators.required,
         Validators.maxLength(this.maxPostalCodeLength),
      ]),
      cityFieldControl: new FormControl('', [
         Validators.required,
         Validators.maxLength(this.maxCityLength),
      ]),
      countryFieldControl: new FormControl('', [
         Validators.required,
         Validators.maxLength(this.maxCountryLength),
      ]),
      emailFieldControl: new FormControl('', [
         Validators.required,
         Validators.maxLength(this.maxEmailLength),
         Validators.email,
      ]),
      careUsersSearchFieldcontrol: new FormControl(),
   });

   constructor(
      // Dialogreference to close this dialog
      public dialogRef: MatDialogRef<LegalguardianDetailsComponent>,
      private _careUserService: CareUserService,
      // Care user that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: LegalGuardianDetailsFormData
   ) {
      this.currentLegalGuardianInstance = data.legalGuardianInstance;
      this.isAdding = data.legalGuardianInstance === null;
      this.displayLinkedUserFields = data.displayLinkedUserFields;
   }

   ngOnInit() {
      //Load current LegalGuardian instance
      this._loadInstance();

      // Subscribe to CareUser lookup events
      this.careUsersAutoComplete$ = this.formControlGroup.controls[
         'careUsersSearchFieldcontrol'
      ].valueChanges.pipe(
         startWith(''),
         debounceTime(300),
         switchMap(value => {
            if (typeof value === 'string') {
               return this.careUserLookup(value);
            } else {
               return of(null);
            }
         })
      );

      this.clearCareUserArrays();

      // Set colums for careuser user cards
      this.columns = this.calculateColumns(window.innerWidth);
   }

   private _loadInstance() {
      // If we are adding a new user then clear all fields
      // If we are editing an existing user then fill in his data
      if (this.isAdding) {
         this.formControlGroup.reset();
         this.currentLegalGuardianInstance = new LegalGuardian();
      }
      else {
         this.loadCurrentLegalGuardianInstanceValues();
      }
   }

   // Fill in the data of the current legal guardian instance
   private loadCurrentLegalGuardianInstanceValues() {
      this.formControlGroup.controls.firstNameFieldControl.reset(this.currentLegalGuardianInstance.firstName);
      this.formControlGroup.controls.lastNameFieldControl.reset(this.currentLegalGuardianInstance.lastName);
      this.formControlGroup.controls.addressFieldControl.reset(this.currentLegalGuardianInstance.address);
      this.formControlGroup.controls.postalCodeFieldControl.reset(this.currentLegalGuardianInstance.postalCode);
      this.formControlGroup.controls.cityFieldControl.reset(this.currentLegalGuardianInstance.city);
      this.formControlGroup.controls.countryFieldControl.reset(this.currentLegalGuardianInstance.country);
      this.formControlGroup.controls.emailFieldControl.reset(this.currentLegalGuardianInstance.email);
   }

   // Clear the currentLegalGuardianInstance careuser properties
   private clearCareUserArrays() {
      if (this.currentLegalGuardianInstance.careUsers === null) {
         this.currentLegalGuardianInstance.careUsers = new Array<CareUser>();
      }
      this.currentLegalGuardianInstance.careUsersToAdd = new Array<string>();
      this.currentLegalGuardianInstance.careUsersToRemove = new Array<string>();
   }

   careUserLookup(value: string): Observable<CareUser[]> {
      return this._careUserService
         .fetchCareUsersData('asc', 'firstName', 0, 15, value)
         .pipe(
            map(res =>
               res.items.filter(
                  i =>
                     !this.currentLegalGuardianInstance.careUsers.some(
                        u => u.id === i.id
                     )
               )
            ),
            catchError(_ => {
               return of(null);
            })
         );
   }

   addCareUser(careUser: CareUser, event: any = null) {
      if (event === null || !event.isUserInput) {
         return;
      }
      if (this.currentLegalGuardianInstance.careUsersToRemove.indexOf(careUser.id) > -1) {
         this.currentLegalGuardianInstance.careUsersToRemove.splice(
            this.currentLegalGuardianInstance.careUsersToRemove.indexOf(careUser.id)
         );
      } else {
         this.currentLegalGuardianInstance.careUsersToAdd.push(careUser.id);
      }

      this.currentLegalGuardianInstance.careUsers.push(careUser);
      this.formControlGroup.controls.careUsersSearchFieldcontrol.reset();
   }

   deleteCareUser(careUser: CareUser) {
      if (this.currentLegalGuardianInstance.careUsersToAdd.indexOf(careUser.id) > -1) {
         this.currentLegalGuardianInstance.careUsersToAdd.splice(
            this.currentLegalGuardianInstance.careUsersToAdd.indexOf(careUser.id)
         );
      } else {
         this.currentLegalGuardianInstance.careUsersToRemove.push(careUser.id);
      }

      if (this.currentLegalGuardianInstance.careUsers !== null) {
         this.currentLegalGuardianInstance.careUsers = this.currentLegalGuardianInstance.careUsers.filter(
            u => u.id !== careUser.id
         );
      }

      this.formControlGroup.controls.careUsersSearchFieldcontrol.reset();
   }

   onResize(event) {
      this.columns = this.calculateColumns(event.target.innerWidth);
   }

   calculateColumns(width: number): number {
      switch (true) {
         case width >= 1200:
            return 3;
         case width >= 800:
            return 2;
         case width >= 400:
            return 1;
         default:
            return 1;
      }
   }

   // Load form field values into current legal guardian instance
   private updateCurrentLegalGuardianInstance() {
      this.currentLegalGuardianInstance.firstName = this.formControlGroup.controls.firstNameFieldControl.value;
      this.currentLegalGuardianInstance.lastName = this.formControlGroup.controls.lastNameFieldControl.value;
      this.currentLegalGuardianInstance.address = this.formControlGroup.controls.addressFieldControl.value;
      this.currentLegalGuardianInstance.postalCode = this.formControlGroup.controls.postalCodeFieldControl.value;
      this.currentLegalGuardianInstance.city = this.formControlGroup.controls.cityFieldControl.value;
      this.currentLegalGuardianInstance.country = this.formControlGroup.controls.countryFieldControl.value;
      this.currentLegalGuardianInstance.email = this.formControlGroup.controls.emailFieldControl.value;
   }

   // Submit the form
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) {
         return;
      }

      this.updateCurrentLegalGuardianInstance();
      this.submitEvent.emit(this.currentLegalGuardianInstance);
      this.closeForm();
   }

   emitDeleteEvent() {
      this.deleteEvent.emit(this.currentLegalGuardianInstance);
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
