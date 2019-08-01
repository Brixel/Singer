import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';

// Data we pass along with the creation of the Mat-Dialog box
export interface SingerEventDetailsFormData {
   singerEventInstance: SingerEvent;
   isAdding: boolean;
}

@Component({
   selector: 'app-singerevent-details',
   templateUrl: './singerevent-details.component.html',
   styleUrls: ['./singerevent-details.component.css'],
})
export class SingerEventDetailsComponent implements OnInit {
   // Submit event for when the user submits the form
   @Output() submitEvent: EventEmitter<SingerEvent> = new EventEmitter();

   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;

   // Boolean to check if changes have been made when editing a user
   isChangesMade: boolean;

   ageGroups = AgeGroup;

   // Current care user instance
   currentSingerEventInstance: SingerEvent;

   //#region Binding properties for form:

   // Form placeholders
   nameFieldPlaceholder = 'Naam evenement.';
   descriptionFieldPlaceholder = 'Beschrijving evenement.';
   locationFieldPlaceholder = 'Locatie evenement';
   ageGroupsFieldPlaceholder = 'Leeftijdsgroepen';
   totalSizeFieldPlaceholder = 'Aantal toegelaten personen';
   currentSizeFieldPlaceholder = 'Hoeveelheid ingeschreven personen';
   priceFieldPlaceholder = 'Prijs';
   startRegistrationDateFieldPlaceholder = 'Start Datum Registratie';
   endRegistrationDateFieldPlaceholder = 'Eind Datum Registratie';
   finalCancelationDateFieldPlaceholder = 'Eind Datum Anulering';
   registrationOnDailyBasisFieldPlaceholder = 'Registratie op dagelijkse basis';
   startDateFieldPlaceholder = 'Start Datum Evenement';
   endDateFieldPlaceholder = 'Eind Datum Evenement';
   hasDayCareBeforeFieldPlaceholder = 'Opvang voor het evenement';
   dayCareBeforeStartTimeFieldPlaceholder = 'Start opvang voor het evenement';
   dayCareBeforeEndTimeFieldPlaceholder = 'Einde opvang voor het evenement';
   hasDayCareAfterFieldPlaceholder = 'Opvang na het evenement';
   dayCareAfterStartTimeFieldPlaceholder = 'Start opvang na het evenement';
   dayCareAfterEndTimeFieldPlaceholder = 'Einde opvang na het evenement';

   // Form control group
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      nameFieldControl: new FormControl('', [Validators.required]),
      descriptionFieldControl: new FormControl('', [Validators.required]),
      locationFieldControl: new FormControl('', [Validators.required]),
      ageGroupsFieldControl: new FormControl('', [Validators.required]),
      totalSizeFieldControl: new FormControl('', [Validators.required]),
      currentSizeFieldControl: new FormControl('', [Validators.required]),
      priceFieldControl: new FormControl('', [Validators.required]),
      startRegistrationDateFieldControl: new FormControl(null, [
         Validators.required,
      ]),
      endRegistrationDateFieldControl: new FormControl(null, [
         Validators.required,
      ]),
      finalCancelationDateFieldControl: new FormControl(null, [
         Validators.required,
      ]),
      registrationOnDailyBasisFieldControl: new FormControl('', [
         Validators.required,
      ]),
      startDateFieldControl: new FormControl(null, [Validators.required]),
      endDateFieldControl: new FormControl(null, [Validators.required]),
      hasDayCareBeforeFieldControl: new FormControl('', [Validators.required]),
      dayCareBeforeStartTimeFieldControl: new FormControl('', [
         Validators.required,
      ]),
      dayCareBeforeEndTimeFieldControl: new FormControl('', [
         Validators.required,
      ]),
      hasDayCareAfterFieldControl: new FormControl('', [Validators.required]),
      dayCareAfterStartTimeFieldControl: new FormControl('', [
         Validators.required,
      ]),
      dayCareAfterEndTimeFieldControl: new FormControl('', [
         Validators.required,
      ]),
   });

   //#endregion

   constructor(
      // dialogreference to close this dialog
      public dialogRef: MatDialogRef<SingerEventDetailsComponent>,
      // Singer event that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: SingerEventDetailsFormData
   ) {
      this.currentSingerEventInstance = data.singerEventInstance;
      this.isAdding = data.isAdding;
   }

   ngOnInit() {
      // If we are adding a new singer event then clear all fields
      // If we are editing an existing singer event then fill in the data

      if (this.isAdding) {
         this.resetFormControls();
         this.createEmptySingerEvent();
      } else {
         this.loadCurrentSingerEventInstanceValues();
      }
   }

   getRequiredFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required') ? 'Dit veld is verplicht' : '';
   }

   // Fill in the data of the current Singer Event instance
   private loadCurrentSingerEventInstanceValues() {
      this.formControlGroup.controls.nameFieldControl.reset(
         this.currentSingerEventInstance.name
      );
      this.formControlGroup.controls.descriptionFieldControl.reset(
         this.currentSingerEventInstance.description
      );
      this.formControlGroup.controls.locationFieldControl.reset(
         this.currentSingerEventInstance.location
      );
      this.formControlGroup.controls.ageGroupsFieldControl.reset(
         this.currentSingerEventInstance.ageGroups
      );
      this.formControlGroup.controls.totalSizeFieldControl.setValue(
         this.currentSingerEventInstance.totalSize
      );
      this.formControlGroup.controls.currentSizeFieldControl.reset(
         this.currentSingerEventInstance.currentSize
      );
      this.formControlGroup.controls.priceFieldControl.reset(
         this.currentSingerEventInstance.price
      );
      this.formControlGroup.controls.startRegistrationDateFieldControl.reset(
         this.currentSingerEventInstance.startRegistrationDate
      );
      this.formControlGroup.controls.endRegistrationDateFieldControl.reset(
         this.currentSingerEventInstance.endRegistrationDate
      );
      this.formControlGroup.controls.finalCancelationDateFieldControl.reset(
         this.currentSingerEventInstance.finalCancelationDate
      );
      this.formControlGroup.controls.registrationOnDailyBasisFieldControl.reset(
         this.currentSingerEventInstance.registrationOnDailyBasis
            ? 'true'
            : 'false'
      );
      this.formControlGroup.controls.startDateFieldControl.reset(
         this.currentSingerEventInstance.startDate
      );
      this.formControlGroup.controls.endDateFieldControl.reset(
         this.currentSingerEventInstance.endDate
      );
      this.formControlGroup.controls.hasDayCareBeforeFieldControl.reset(
         this.currentSingerEventInstance.hasDayCareBefore ? 'true' : 'false'
      );
      this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.reset(
         this.currentSingerEventInstance.dayCareBeforeStartTime
      );
      this.formControlGroup.controls.dayCareBeforeEndTimeFieldControl.reset(
         this.currentSingerEventInstance.dayCareBeforeEndTime
      );
      this.formControlGroup.controls.hasDayCareAfterFieldControl.reset(
         this.currentSingerEventInstance.hasDayCareAfter ? 'true' : 'false'
      );
      this.formControlGroup.controls.dayCareAfterStartTimeFieldControl.reset();
      this.formControlGroup.controls.dayCareAfterEndTimeFieldControl.reset(
         this.currentSingerEventInstance.dayCareAfterEndTime
      );
   }

   // Clear all form fields
   private resetFormControls() {
      this.formControlGroup.controls.nameFieldControl.reset();
      this.formControlGroup.controls.descriptionFieldControl.reset();
      this.formControlGroup.controls.locationFieldControl.reset();
      this.formControlGroup.controls.ageGroupsFieldControl.reset();
      this.formControlGroup.controls.totalSizeFieldControl.reset();
      this.formControlGroup.controls.currentSizeFieldControl.reset();
      this.formControlGroup.controls.priceFieldControl.reset();
      this.formControlGroup.controls.startRegistrationDateFieldControl.reset();
      this.formControlGroup.controls.endRegistrationDateFieldControl.reset();
      this.formControlGroup.controls.finalCancelationDateFieldControl.reset();
      this.formControlGroup.controls.registrationOnDailyBasisFieldControl.reset();
      this.formControlGroup.controls.startDateFieldControl.reset();
      this.formControlGroup.controls.endDateFieldControl.reset();
      this.formControlGroup.controls.hasDayCareBeforeFieldControl.reset();
      this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.reset();
      this.formControlGroup.controls.dayCareBeforeEndTimeFieldControl.reset();
      this.formControlGroup.controls.hasDayCareAfterFieldControl.reset();
      this.formControlGroup.controls.dayCareAfterStartTimeFieldControl.reset();
      this.formControlGroup.controls.dayCareAfterEndTimeFieldControl.reset();
   }

   createEmptySingerEvent() {
      this.currentSingerEventInstance = {
         id: '',
         name: '',
         description: '',
         location: '',
         ageGroups: [],
         totalSize: 0,
         currentSize: 0,
         price: 0,
         startRegistrationDate: new Date(),
         endRegistrationDate: new Date(),
         finalCancelationDate: new Date(),
         registrationOnDailyBasis: false,
         startDate: new Date(),
         endDate: new Date(),
         hasDayCareBefore: false,
         dayCareBeforeStartTime: { hours: 0, minutes: 0 },
         dayCareBeforeEndTime: { hours: 0, minutes: 0 },
         hasDayCareAfter: false,
         dayCareAfterStartTime: { hours: 0, minutes: 0 },
         dayCareAfterEndTime: { hours: 0, minutes: 0 },
      };
   }

   // If we are editing an existing user and there are no changes return false
   checkForChanges(): boolean {
      if (this.isAdding) return true;
      if (
         this.currentSingerEventInstance.name !==
         this.formControlGroup.controls.nameFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.description !==
         this.formControlGroup.controls.descriptionFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.location !==
         this.formControlGroup.controls.locationFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.ageGroups !==
         this.formControlGroup.controls.ageGroupsFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.totalSize !==
         this.formControlGroup.controls.totalSizeFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.currentSize !==
         this.formControlGroup.controls.currentSizeFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.price !==
         this.formControlGroup.controls.priceFieldControl.value
      ) {
         return true;
      }

      var instanceDate = new Date(
         this.currentSingerEventInstance.startRegistrationDate
      );
      var formDate = new Date(
         this.formControlGroup.controls.startRegistrationDateFieldControl.value
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

      instanceDate = new Date(
         this.currentSingerEventInstance.endRegistrationDate
      );
      formDate = new Date(
         this.formControlGroup.controls.endRegistrationDateFieldControl.value
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

      instanceDate = new Date(
         this.currentSingerEventInstance.finalCancelationDate
      );
      formDate = new Date(
         this.formControlGroup.controls.finalCancelationDateFieldControl.value
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
         this.currentSingerEventInstance.registrationOnDailyBasis !==
         (this.formControlGroup.controls.registrationOnDailyBasisFieldControl
            .value === 'true'
            ? true
            : false)
      ) {
         return true;
      }

      instanceDate = new Date(this.currentSingerEventInstance.startDate);
      formDate = new Date(
         this.formControlGroup.controls.startDateFieldControl.value
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

      instanceDate = new Date(this.currentSingerEventInstance.endDate);
      formDate = new Date(
         this.formControlGroup.controls.endDateFieldControl.value
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
         this.currentSingerEventInstance.hasDayCareBefore !==
         (this.formControlGroup.controls.hasDayCareBeforeFieldControl.value ===
         'true'
            ? true
            : false)
      ) {
         return true;
      }

      if (
         this.currentSingerEventInstance.dayCareBeforeStartTime !==
         this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.value
      ) {
         return true;
      }

      if (
         this.currentSingerEventInstance.dayCareBeforeEndTime !==
         this.formControlGroup.controls.dayCareBeforeEndTimeFieldControl.value
      ) {
         return true;
      }

      if (
         this.currentSingerEventInstance.hasDayCareAfter !==
         (this.formControlGroup.controls.hasDayCareAfterFieldControl.value ===
         'true'
            ? true
            : false)
      ) {
         return true;
      }

      if (
         this.currentSingerEventInstance.dayCareAfterStartTime !==
         this.formControlGroup.controls.dayCareAfterStartTimeFieldControl.value
      ) {
         return true;
      }

      if (
         this.currentSingerEventInstance.dayCareAfterEndTime !==
         this.formControlGroup.controls.dayCareAfterEndTimeFieldControl.value
      ) {
         return true;
      }
   }

   // Load form field values into current singer event instance
   private updateCurrentSingerEventInstance() {
      this.currentSingerEventInstance.name = this.formControlGroup.controls.nameFieldControl.value;
      this.currentSingerEventInstance.description = this.formControlGroup.controls.descriptionFieldControl.value;
      this.currentSingerEventInstance.location = this.formControlGroup.controls.locationFieldControl.value;
      this.currentSingerEventInstance.ageGroups = this.formControlGroup.controls.ageGroupsFieldControl.value;
      this.currentSingerEventInstance.totalSize = this.formControlGroup.controls.totalSizeFieldControl.value;
      this.currentSingerEventInstance.currentSize = this.formControlGroup.controls.currentSizeFieldControl.value;
      this.currentSingerEventInstance.price = this.formControlGroup.controls.priceFieldControl.value;
      this.currentSingerEventInstance.startRegistrationDate = this.formControlGroup.controls.startRegistrationDateFieldControl.value;
      this.currentSingerEventInstance.endRegistrationDate = this.formControlGroup.controls.endRegistrationDateFieldControl.value;
      this.currentSingerEventInstance.finalCancelationDate = this.formControlGroup.controls.finalCancelationDateFieldControl.value;
      this.currentSingerEventInstance.registrationOnDailyBasis =
         this.formControlGroup.controls.registrationOnDailyBasisFieldControl
            .value === 'true'
            ? true
            : false;
      this.currentSingerEventInstance.startDate = this.formControlGroup.controls.startDateFieldControl.value;
      this.currentSingerEventInstance.endDate = this.formControlGroup.controls.endDateFieldControl.value;
      this.currentSingerEventInstance.hasDayCareBefore =
         this.formControlGroup.controls.hasDayCareBeforeFieldControl.value ===
         'true'
            ? true
            : false;
      this.currentSingerEventInstance.dayCareBeforeStartTime = this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.value;
      this.currentSingerEventInstance.dayCareBeforeEndTime = this.formControlGroup.controls.dayCareBeforeEndTimeFieldControl.value;
      this.currentSingerEventInstance.hasDayCareAfter =
         this.formControlGroup.controls.hasDayCareAfterFieldControl.value ===
         'true'
            ? true
            : false;
      this.currentSingerEventInstance.dayCareAfterStartTime = this.formControlGroup.controls.dayCareAfterStartTimeFieldControl.value;
      this.currentSingerEventInstance.dayCareAfterEndTime = this.formControlGroup.controls.dayCareAfterEndTimeFieldControl.value;
   }

   // Submit the form
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) {
         return;
      }

      // Check for changes and determine of an API call is necesarry
      if (this.checkForChanges()) {
         this.updateCurrentSingerEventInstance();
         this.submitEvent.emit(this.currentSingerEventInstance);
      }
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
