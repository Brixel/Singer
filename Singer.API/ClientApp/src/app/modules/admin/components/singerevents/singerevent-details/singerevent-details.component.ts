import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
   SingerEvent,
   SingerEventLocation,
} from 'src/app/modules/core/models/singerevent.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';

// Data we pass along with the creation of the Mat-Dialog box
export interface SingerEventDetailsFormData {
   singerEventInstance: SingerEvent;
   isAdding: boolean;
   availableLocations: SingerEventLocation[];
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

   ageGroups = Object.keys(AgeGroup).filter(
      k => typeof AgeGroup[k as any] === 'number'
   );

   // Current care user instance
   currentSingerEventInstance: SingerEvent;

   selectedLocation: SingerEventLocation;

   availableLocations: SingerEventLocation[];

   //#region Binding properties for form:

   // Form placeholders
   titleFieldPlaceholder = 'Naam evenement.';
   descriptionFieldPlaceholder = 'Beschrijving evenement.';
   locationFieldPlaceholder = 'Locatie evenement';
   allowedAgeGroupsFieldPlaceholder = 'Leeftijdsgroepen';
   maxRegistrantsFieldPlaceholder = 'Aantal toegelaten personen';
   costFieldPlaceholder = 'Prijs';
   startRegistrationDateFieldPlaceholder = 'Start Datum Registratie';
   endRegistrationDateFieldPlaceholder = 'Eind Datum Registratie';
   finalCancellationDateFieldPlaceholder = 'Eind Datum Anulering';
   registrationOnDailyBasisFieldPlaceholder = 'Registratie op dagelijkse basis';
   startDateFieldPlaceholder = 'Start Datum Evenement';
   endDateFieldPlaceholder = 'Eind Datum Evenement';
   dailyStartTimePlaceholder = 'Start Tijd Evenement';
   dailyEndTimePlaceholder = 'Eind Tijd Evenement';
   hasDayCareBeforeFieldPlaceholder = 'Opvang voor het evenement';
   dayCareBeforeStartTimeFieldPlaceholder = 'Start opvang voor het evenement';
   dayCareBeforeEndTimeFieldPlaceholder = 'Einde opvang voor het evenement';
   hasDayCareAfterFieldPlaceholder = 'Opvang na het evenement';
   dayCareAfterStartTimeFieldPlaceholder = 'Start opvang na het evenement';
   dayCareAfterEndTimeFieldPlaceholder = 'Einde opvang na het evenement';

   // Form control group
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      titleFieldControl: new FormControl('', [Validators.required]),
      descriptionFieldControl: new FormControl('', [Validators.required]),
      locationFieldControl: new FormControl(null, [Validators.required]),
      allowedAgeGroupsFieldControl: new FormControl('', [Validators.required]),
      maxRegistrantsFieldControl: new FormControl('', [Validators.required]),
      costFieldControl: new FormControl('', [Validators.required]),
      startRegistrationDateFieldControl: new FormControl(null, [
         Validators.required,
      ]),
      endRegistrationDateFieldControl: new FormControl(null, [
         Validators.required,
      ]),
      finalCancellationDateFieldControl: new FormControl(null, [
         Validators.required,
      ]),
      registrationOnDailyBasisFieldControl: new FormControl('', [
         Validators.required,
      ]),
      startDateFieldControl: new FormControl(null, [Validators.required]),
      endDateFieldControl: new FormControl(null, [Validators.required]),
      dailyStartTimeFieldControl: new FormControl('', [Validators.required]),
      dailyEndTimeFieldControl: new FormControl(null, [Validators.required]),
      hasDayCareBeforeFieldControl: new FormControl('', [Validators.required]),
      dayCareBeforeStartTimeFieldControl: new FormControl(),
      dayCareBeforeEndTimeFieldControl: new FormControl(),
      hasDayCareAfterFieldControl: new FormControl('', [Validators.required]),
      dayCareAfterStartTimeFieldControl: new FormControl(),
      dayCareAfterEndTimeFieldControl: new FormControl(),
   });

   careBeforeRequired() {
      if (
         this.formControlGroup.controls.hasDayCareBeforeFieldControl.value ===
         'true'
      ) {
         return Validators.required;
      }
   }

   allowedAgeGroupsFormControlArray: FormArray = new FormArray([
      new FormControl('', [Validators.required]),
   ]);
   selectedAgeGroups: AgeGroup[];

   addAllowedAgeGroupsFormControlArrayToFormGroup() {
      this.formControlGroup.registerControl(
         'allowedAgeGroupsFormControlArray',
         this.allowedAgeGroupsFormControlArray
      );
   }

   addFormControlToAllowedAgeGroupsFormControlArray() {
      this.allowedAgeGroupsFormControlArray.push(
         new FormControl('', [Validators.required])
      );
   }

   //#endregion

   constructor(
      // dialogreference to close this dialog
      public dialogRef: MatDialogRef<SingerEventDetailsComponent>,
      // Singer event that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: SingerEventDetailsFormData
   ) {
      this.currentSingerEventInstance = data.singerEventInstance;
      this.isAdding = data.isAdding;
      this.availableLocations = data.availableLocations;
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

      // TODO: The commented code below attempts to set the before/after
      // start times of daycare as required fields, only when before/after
      // daycare has been set to true, this is however not working.

      // this.formControlGroup
      //    .get('hasDayCareBeforeFieldControl')
      //    .valueChanges.subscribe(hasDayCareBefore => {
      //       console.log(hasDayCareBefore);
      //       let before = this.formControlGroup.get(
      //          'dayCareBeforeStartTimeFieldControl'
      //       );
      //       let after = this.formControlGroup.get(
      //          'dayCareBeforeEndTimeFieldControl'
      //       );
      //       if (hasDayCareBefore) {
      //          before.setValidators([Validators.required]);
      //          after.setValidators([Validators.required]);
      //       } else {
      //          before.clearValidators();
      //          after.clearValidators();
      //       }
      //       before.updateValueAndValidity();
      //       after.updateValueAndValidity();
      //    });
   }

   getRequiredFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required') ? 'Dit veld is verplicht' : '';
   }

   compareLocations(
      locationX: SingerEventLocation,
      locationY: SingerEventLocation
   ) {
      return locationX.id === locationY.id;
   }
   compareAgeGroups(ageGroupX: number, ageGroupY: string) {
      return AgeGroup[ageGroupX] === ageGroupY;
   }

   // Fill in the data of the current Singer Event instance
   private loadCurrentSingerEventInstanceValues() {
      this.formControlGroup.controls.titleFieldControl.reset(
         this.currentSingerEventInstance.title
      );
      this.formControlGroup.controls.descriptionFieldControl.reset(
         this.currentSingerEventInstance.description
      );
      this.selectedLocation = this.availableLocations.find(
         x => x.id === this.currentSingerEventInstance.location.id
      );
      this.formControlGroup.controls.locationFieldControl.setValue(
         this.selectedLocation
      );
      this.selectedAgeGroups = this.currentSingerEventInstance.allowedAgeGroups;
      this.formControlGroup.controls.allowedAgeGroupsFieldControl.setValue(
         this.selectedAgeGroups
      );
      this.formControlGroup.controls.maxRegistrantsFieldControl.setValue(
         this.currentSingerEventInstance.maxRegistrants
      );
      this.formControlGroup.controls.costFieldControl.reset(
         this.currentSingerEventInstance.cost
      );
      this.formControlGroup.controls.startRegistrationDateFieldControl.reset(
         this.currentSingerEventInstance.startRegistrationDate
      );
      this.formControlGroup.controls.endRegistrationDateFieldControl.reset(
         this.currentSingerEventInstance.endRegistrationDate
      );
      this.formControlGroup.controls.finalCancellationDateFieldControl.reset(
         this.currentSingerEventInstance.finalCancellationDate
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
      this.formControlGroup.controls.dailyStartTimeFieldControl.reset(
         this.currentSingerEventInstance.dailyStartTime
      );
      this.formControlGroup.controls.dailyEndTimeFieldControl.reset(
         this.currentSingerEventInstance.dailyEndTime
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
      this.formControlGroup.controls.dayCareAfterStartTimeFieldControl.reset(
         this.currentSingerEventInstance.dayCareAfterStartTime
      );
      this.formControlGroup.controls.dayCareAfterEndTimeFieldControl.reset(
         this.currentSingerEventInstance.dayCareAfterEndTime
      );
   }

   // Clear all form fields
   private resetFormControls() {
      this.formControlGroup.controls.titleFieldControl.reset();
      this.formControlGroup.controls.descriptionFieldControl.reset();
      this.formControlGroup.controls.locationFieldControl.reset();
      this.formControlGroup.controls.allowedAgeGroupsFieldControl.reset();
      this.formControlGroup.controls.maxRegistrantsFieldControl.reset();
      this.formControlGroup.controls.costFieldControl.reset();
      this.formControlGroup.controls.startRegistrationDateFieldControl.reset();
      this.formControlGroup.controls.endRegistrationDateFieldControl.reset();
      this.formControlGroup.controls.finalCancellationDateFieldControl.reset();
      this.formControlGroup.controls.registrationOnDailyBasisFieldControl.reset();
      this.formControlGroup.controls.startDateFieldControl.reset();
      this.formControlGroup.controls.endDateFieldControl.reset();
      this.formControlGroup.controls.dailyStartTimeFieldControl.reset();
      this.formControlGroup.controls.dailyEndTimeFieldControl.reset();
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
         title: '',
         description: '',
         location: new SingerEventLocation(),
         allowedAgeGroups: [],
         maxRegistrants: 0,
         currentRegistrants: 0,
         cost: 0,
         startRegistrationDate: new Date(),
         endRegistrationDate: new Date(),
         finalCancellationDate: new Date(),
         registrationOnDailyBasis: false,
         startDate: new Date(),
         endDate: new Date(),
         dailyStartTime: new Date(),
         dailyEndTime: new Date(),
         hasDayCareBefore: false,
         dayCareBeforeStartTime: new Date(),
         dayCareBeforeEndTime: new Date(),
         hasDayCareAfter: false,
         dayCareAfterStartTime: new Date(),
         dayCareAfterEndTime: new Date(),
      };
   }

   // If we are editing an existing user and there are no changes return false
   checkForChanges(): boolean {
      if (this.isAdding) {
         return true;
      }

      if (
         this.currentSingerEventInstance.title !==
         this.formControlGroup.controls.titleFieldControl.value
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
         this.currentSingerEventInstance.location.name !==
         this.formControlGroup.controls.locationFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.allowedAgeGroups !==
         this.formControlGroup.controls.allowedAgeGroupsFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.maxRegistrants !==
         this.formControlGroup.controls.maxRegistrantsFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentSingerEventInstance.cost !==
         this.formControlGroup.controls.costFieldControl.value
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
         this.currentSingerEventInstance.finalCancellationDate
      );
      formDate = new Date(
         this.formControlGroup.controls.finalCancellationDateFieldControl.value
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
         this.currentSingerEventInstance.dailyStartTime !==
         this.formControlGroup.controls.dailyStartTimeFieldControl.value
      ) {
         return true;
      }

      if (
         this.currentSingerEventInstance.dailyEndTime !==
         this.formControlGroup.controls.dailyEndTimeFieldControl.value
      ) {
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
      this.currentSingerEventInstance.title = this.formControlGroup.controls.titleFieldControl.value;
      this.currentSingerEventInstance.description = this.formControlGroup.controls.descriptionFieldControl.value;
      this.currentSingerEventInstance.location = this.formControlGroup.controls.locationFieldControl.value;
      this.currentSingerEventInstance.allowedAgeGroups = this.formControlGroup.controls.allowedAgeGroupsFieldControl.value;
      this.currentSingerEventInstance.maxRegistrants = this.formControlGroup.controls.maxRegistrantsFieldControl.value;
      this.currentSingerEventInstance.cost = this.formControlGroup.controls.costFieldControl.value;
      this.currentSingerEventInstance.startRegistrationDate = this.formControlGroup.controls.startRegistrationDateFieldControl.value;
      this.currentSingerEventInstance.endRegistrationDate = this.formControlGroup.controls.endRegistrationDateFieldControl.value;
      this.currentSingerEventInstance.finalCancellationDate = this.formControlGroup.controls.finalCancellationDateFieldControl.value;
      this.currentSingerEventInstance.registrationOnDailyBasis =
         this.formControlGroup.controls.registrationOnDailyBasisFieldControl
            .value === 'true'
            ? true
            : false;
      this.currentSingerEventInstance.startDate = this.formControlGroup.controls.startDateFieldControl.value;
      this.currentSingerEventInstance.endDate = this.formControlGroup.controls.endDateFieldControl.value;
      this.currentSingerEventInstance.dailyStartTime = this.formControlGroup.controls.dailyStartTimeFieldControl.value;
      this.currentSingerEventInstance.dailyEndTime = this.formControlGroup.controls.dailyEndTimeFieldControl.value;
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
