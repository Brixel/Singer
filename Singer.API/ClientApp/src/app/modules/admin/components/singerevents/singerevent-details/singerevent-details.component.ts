import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
   SingerEvent,
   SingerEventLocation,
} from 'src/app/modules/core/models/singerevent.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';
import { MAT_DATE_FORMATS } from '@angular/material';
import * as moment from 'moment';
import { isNullOrUndefined } from 'util';
import { MY_FORMATS } from 'src/app/modules/core/core.module';
import { dateNotBefore } from 'src/app/modules/core/utils/custom-date-validators';
import { NgxMaterialTimepickerTheme } from 'ngx-material-timepicker';

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
   providers: [{ provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }],
})
export class SingerEventDetailsComponent implements OnInit {
   // Submit event for when the user submits the form
   @Output() submitEvent: EventEmitter<SingerEvent> = new EventEmitter();
   @Output() deleteEvent: EventEmitter<SingerEvent> = new EventEmitter();

   // Boolean to decide if we are adding a new event or editing an existing one
   isAdding: boolean;

   // Boolean to check if changes have been made when editing an event
   isChangesMade: boolean;

   ageGroups = Object.keys(AgeGroup).filter(
      k => typeof AgeGroup[k as any] === 'number'
   );

   // Current singer event instance
   currentSingerEventInstance: SingerEvent;

   selectedLocation: SingerEventLocation;

   availableLocations: SingerEventLocation[];

   singerTimePickerTheme: NgxMaterialTimepickerTheme = {
      container: {
         buttonColor: '#6a9de1',
      },
      dial: {
         dialBackgroundColor: '#4a88da',
      },
      clockFace: {
         clockHandColor: '#4a88da',
      },
   };

   allowedAgeGroupsFormControlArray: FormArray = new FormArray([
      new FormControl('', [Validators.required]),
   ]);
   selectedAgeGroups: AgeGroup[];

   //#region Binding properties for form:

   // Form placeholders
   readonly titleFieldPlaceholder = 'Naam evenement.';
   readonly descriptionFieldPlaceholder = 'Beschrijving evenement.';
   readonly locationFieldPlaceholder = 'Locatie evenement';
   readonly allowedAgeGroupsFieldPlaceholder = 'Leeftijdsgroepen';
   readonly maxRegistrantsFieldPlaceholder = 'Aantal toegelaten personen';
   readonly costFieldPlaceholder = 'Prijs (0 is gratis)';
   readonly startRegistrationDateFieldPlaceholder = 'Startdatum registratie';
   readonly endRegistrationDateFieldPlaceholder = 'Einddatum registratie';
   readonly finalCancellationDateFieldPlaceholder = 'Uiterste annulatiedatum';
   readonly registrationOnDailyBasisFieldPlaceholder =
      'Registratie op dagelijkse basis';
   readonly startDateFieldPlaceholder = 'Startdatum evenement';
   readonly endDateFieldPlaceholder = 'Einddatum evenement';
   readonly dailyStartTimePlaceholder = 'Starttijd evenement';
   readonly dailyEndTimePlaceholder = 'Eindtijd evenement';
   readonly hasDayCareBeforeFieldPlaceholder = 'Opvang voor het evenement';
   readonly dayCareBeforeStartTimeFieldPlaceholder =
      'Start opvang voor het evenement';
   readonly dayCareBeforeEndTimeFieldPlaceholder =
      'Einde opvang voor het evenement';
   readonly hasDayCareAfterFieldPlaceholder = 'Opvang na het evenement';
   readonly dayCareAfterStartTimeFieldPlaceholder =
      'Start opvang na het evenement';
   readonly dayCareAfterEndTimeFieldPlaceholder =
      'Einde opvang na het evenement';
   readonly confirmTitleFieldPlaceholder = 'Naam evenement';
   // Form validation values
   readonly maxTitleLength = 100;
   readonly minTitleLength = 2;
   readonly maxDescriptionLength = 10000;
   readonly maxMaxRegistrants = 2000000000;
   readonly minMaxRegistrants = 1;
   readonly maxCost = 2000000000;
   readonly minCost = 0;

   // Form control group
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      titleFieldControl: new FormControl('', [
         Validators.required,
         Validators.minLength(this.minTitleLength),
         Validators.maxLength(this.maxTitleLength),
      ]),
      descriptionFieldControl: new FormControl('', [
         Validators.maxLength(this.maxDescriptionLength),
      ]),
      locationFieldControl: new FormControl('', [Validators.required]),
      allowedAgeGroupsFieldControl: new FormControl('', [Validators.required]),
      maxRegistrantsFieldControl: new FormControl('', [
         Validators.required,
         Validators.max(this.maxMaxRegistrants),
         Validators.min(this.minMaxRegistrants),
      ]),
      costFieldControl: new FormControl('', [
         Validators.required,
         Validators.min(this.minCost),
         Validators.max(this.maxCost),
      ]),
      startRegistrationDateFieldControl: new FormControl(new Date(), [
         Validators.required,
      ]),
      endRegistrationDateFieldControl: new FormControl(new Date(), [
         Validators.required,
      ]),
      finalCancellationDateFieldControl: new FormControl(new Date(), [
         Validators.required,
      ]),
      registrationOnDailyBasisFieldControl: new FormControl('', [
         Validators.required,
      ]),
      startDateFieldControl: new FormControl(new Date(), [Validators.required]),
      endDateFieldControl: new FormControl(new Date(), [Validators.required]),
      dailyStartTimeFieldControl: new FormControl('00:00', [
         Validators.required,
      ]),
      dailyEndTimeFieldControl: new FormControl('00:00', [Validators.required]),
      hasDayCareBeforeFieldControl: new FormControl('', [Validators.required]),
      dayCareBeforeStartTimeFieldControl: new FormControl('00:00'),
      hasDayCareAfterFieldControl: new FormControl('', [Validators.required]),
      dayCareAfterEndTimeFieldControl: new FormControl('00:00'),
   });

   careBeforeRequired() {
      if (
         this.formControlGroup.controls.hasDayCareBeforeFieldControl.value ===
         'true'
      ) {
         return Validators.required;
      }
   }

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

      this.formControlGroup.controls.startRegistrationDateFieldControl.valueChanges.subscribe(
         res => {
            const minDate = res;
            const endRegistrationControl = this.formControlGroup.controls
               .endRegistrationDateFieldControl;
            endRegistrationControl.setValidators([
               Validators.required,
               dateNotBefore(minDate),
            ]);
            endRegistrationControl.updateValueAndValidity();

            const finalCancellationDateFieldControl = this.formControlGroup
               .controls.finalCancellationDateFieldControl;
            finalCancellationDateFieldControl.setValidators([
               dateNotBefore(minDate),
            ]);
            finalCancellationDateFieldControl.updateValueAndValidity();
         }
      );

      this.formControlGroup.controls.startDateFieldControl.valueChanges.subscribe(
         res => {
            const minDate = res;
            const endDateFieldControl = this.formControlGroup.controls
               .endDateFieldControl;
            endDateFieldControl.setValidators([
               Validators.required,
               dateNotBefore(minDate),
            ]);
            endDateFieldControl.updateValueAndValidity();
         }
      );

      this.formControlGroup.controls.hasDayCareBeforeFieldControl.valueChanges.subscribe(
         (res: string) => {
            const hasDayCareBefore = res === 'true';
            const dayCareBeforeStartTimeFieldControl = this.formControlGroup
               .controls.dayCareBeforeStartTimeFieldControl;
            if (hasDayCareBefore) {
               dayCareBeforeStartTimeFieldControl.setValidators([
                  Validators.required,
               ]);
            } else {
               dayCareBeforeStartTimeFieldControl.clearValidators();
            }
            dayCareBeforeStartTimeFieldControl.reset();
            dayCareBeforeStartTimeFieldControl.updateValueAndValidity();
         }
      );

      this.formControlGroup.controls.hasDayCareAfterFieldControl.valueChanges.subscribe(
         (res: string) => {
            const hasDayCareAfter = res === 'true';
            const dayCareAfterEndTimeFieldControl = this.formControlGroup
               .controls.dayCareAfterEndTimeFieldControl;
            if (hasDayCareAfter) {
               dayCareAfterEndTimeFieldControl.setValidators([
                  Validators.required,
               ]);
            } else {
               dayCareAfterEndTimeFieldControl.clearValidators();
            }
            dayCareAfterEndTimeFieldControl.reset();
            dayCareAfterEndTimeFieldControl.updateValueAndValidity();
         }
      );
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
         this.currentSingerEventInstance.startRegistrationDateTime
      );
      this.formControlGroup.controls.endRegistrationDateFieldControl.reset(
         this.currentSingerEventInstance.endRegistrationDateTime
      );
      this.formControlGroup.controls.finalCancellationDateFieldControl.reset(
         this.currentSingerEventInstance.finalCancellationDateTime
      );
      this.formControlGroup.controls.registrationOnDailyBasisFieldControl.reset(
         this.currentSingerEventInstance.registrationOnDailyBasis
            ? 'true'
            : 'false'
      );
      this.formControlGroup.controls.startDateFieldControl.reset(
         this.currentSingerEventInstance.startDateTime
      );
      this.formControlGroup.controls.endDateFieldControl.reset(
         this.currentSingerEventInstance.endDateTime
      );
      this.formControlGroup.controls.dailyStartTimeFieldControl.reset(
         moment
            .utc(this.currentSingerEventInstance.startDateTime)
            .local()
            .format('HH:mm')
      );
      this.formControlGroup.controls.dailyEndTimeFieldControl.reset(
         moment
            .utc(this.currentSingerEventInstance.endDateTime)
            .local()
            .format('HH:mm')
      );
      this.formControlGroup.controls.hasDayCareBeforeFieldControl.reset(
         this.currentSingerEventInstance.hasDayCareBefore ? 'true' : 'false'
      );
      this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.reset(
         moment
            .utc(this.currentSingerEventInstance.dayCareBeforeStartDateTime)
            .local()
            .format('HH:mm')
      );
      this.formControlGroup.controls.hasDayCareAfterFieldControl.reset(
         this.currentSingerEventInstance.hasDayCareAfter ? 'true' : 'false'
      );
      this.formControlGroup.controls.dayCareAfterEndTimeFieldControl.reset(
         moment
            .utc(this.currentSingerEventInstance.dayCareAfterEndDateTime)
            .local()
            .format('HH:mm')
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
      this.formControlGroup.controls.hasDayCareAfterFieldControl.reset();
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
         cost: 0,
         startRegistrationDateTime: new Date(),
         endRegistrationDateTime: new Date(),
         finalCancellationDateTime: new Date(),
         registrationOnDailyBasis: false,
         startDateTime: new Date(),
         endDateTime: new Date(),
         hasDayCareBefore: false,
         dayCareBeforeStartDateTime: new Date(),
         hasDayCareAfter: false,
         dayCareAfterEndDateTime: new Date(),
         eventSlots: [],
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

      let instanceDate = new Date(
         this.currentSingerEventInstance.startRegistrationDateTime
      );
      let formDate = new Date(
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
         this.currentSingerEventInstance.endRegistrationDateTime
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
         this.currentSingerEventInstance.finalCancellationDateTime
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

      instanceDate = new Date(this.currentSingerEventInstance.startDateTime);
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

      instanceDate = new Date(this.currentSingerEventInstance.endDateTime);
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
         moment(this.currentSingerEventInstance.startDateTime).format(
            'HH:mm'
         ) !== this.formControlGroup.controls.dailyStartTimeFieldControl.value
      ) {
         return true;
      }

      if (
         moment(this.currentSingerEventInstance.endDateTime).format('HH:mm') !==
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
         moment(
            this.currentSingerEventInstance.dayCareBeforeStartDateTime
         ).format('HH:mm') !==
         this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.value
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
         moment(this.currentSingerEventInstance.dayCareAfterEndDateTime).format(
            'HH:mm'
         ) !==
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

      this.currentSingerEventInstance.startRegistrationDateTime = this.formControlGroup.controls.startRegistrationDateFieldControl.value;
      this.currentSingerEventInstance.endRegistrationDateTime = this.formControlGroup.controls.endRegistrationDateFieldControl.value;
      this.currentSingerEventInstance.finalCancellationDateTime = this.formControlGroup.controls.finalCancellationDateFieldControl.value;
      this.currentSingerEventInstance.registrationOnDailyBasis =
         this.formControlGroup.controls.registrationOnDailyBasisFieldControl
            .value === 'true'
            ? true
            : false;

      this.currentSingerEventInstance.startDateTime = this._handleDateTimeFields(
         new Date(this.formControlGroup.controls.startDateFieldControl.value),
         this.formControlGroup.controls.dailyStartTimeFieldControl.value
      );
      this.currentSingerEventInstance.endDateTime = this._handleDateTimeFields(
         new Date(this.formControlGroup.controls.endDateFieldControl.value),
         this.formControlGroup.controls.dailyEndTimeFieldControl.value
      );
      this.currentSingerEventInstance.hasDayCareBefore =
         this.formControlGroup.controls.hasDayCareBeforeFieldControl.value ===
         'true'
            ? true
            : false;
      this.currentSingerEventInstance.dayCareBeforeStartDateTime = this._handleDateTimeFields(
         new Date(this.formControlGroup.controls.startDateFieldControl.value),
         this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.value
      );
      this.currentSingerEventInstance.hasDayCareAfter =
         this.formControlGroup.controls.hasDayCareAfterFieldControl.value ===
         'true'
            ? true
            : false;
      this.currentSingerEventInstance.dayCareAfterEndDateTime = this._handleDateTimeFields(
         new Date(this.formControlGroup.controls.endDateFieldControl.value),
         this.formControlGroup.controls.dayCareAfterEndTimeFieldControl.value
      );
   }

   private _handleDateTimeFields(dateField: Date, timeField: string): Date {
      if (isNullOrUndefined(dateField) || isNullOrUndefined(timeField)) {
         return new Date();
      }
      const timePieces = timeField.split(':');
      dateField.setHours(parseInt(timePieces[0]));
      dateField.setMinutes(parseInt(timePieces[1]));

      return dateField;
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

   submitDeleteEvent() {
      this.deleteEvent.emit(this.currentSingerEventInstance);
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
