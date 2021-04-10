import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';
import * as moment from 'moment';
import { isNullOrUndefined } from 'util';
import { dateNotBefore } from 'src/app/modules/core/utils/custom-date-validators';
import { NgxMaterialTimepickerTheme } from 'ngx-material-timepicker';
import { SingerLocation } from 'src/app/modules/core/models/singer-location.model';

// Data we pass along with the creation of the Mat-Dialog box
export interface SingerEventDetailsFormData {
   singerEventInstance: SingerEvent;
   availableLocations: SingerLocation[];
}

@Component({
   selector: 'app-singerevent-details',
   templateUrl: './singerevent-details.component.html',
   styleUrls: ['./singerevent-details.component.css'],
})
export class SingerEventDetailsComponent implements OnInit {
   @Output() submitEvent: EventEmitter<SingerEvent> = new EventEmitter();
   @Output() deleteEvent: EventEmitter<SingerEvent> = new EventEmitter();

   // Boolean to decide if we are adding a new event or editing an existing one
   isAdding: boolean;

   // Available agegroups
   ageGroups = AgeGroup;

   // Current singer event instance
   currentSingerEventInstance: SingerEvent;

   selectedLocation: SingerLocation;

   availableLocations: SingerLocation[];

   allowedAgeGroupsFormControlArray: FormArray = new FormArray([new FormControl('', [Validators.required])]);

   selectedAgeGroups: AgeGroup[];

   // Color theme for the timepickers
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

   // Form placeholders
   readonly titleFieldPlaceholder = 'Naam evenement.';
   readonly descriptionFieldPlaceholder = 'Beschrijving evenement.';
   readonly locationFieldPlaceholder = 'Locatie evenement';
   readonly allowedAgeGroupsFieldPlaceholder = 'Leeftijdsgroepen';
   readonly maxRegistrantsFieldPlaceholder = 'Aantal toegelaten personen';
   readonly costFieldPlaceholder = 'Extra kost (0 is gratis)';
   readonly startRegistrationDateFieldPlaceholder = 'Startdatum registratie';
   readonly endRegistrationDateFieldPlaceholder = 'Einddatum registratie';
   readonly finalCancellationDateFieldPlaceholder = 'Uiterste annulatiedatum';
   readonly registrationOnDailyBasisFieldPlaceholder = 'Registratie op dagelijkse basis';
   readonly startDateFieldPlaceholder = 'Startdatum evenement';
   readonly endDateFieldPlaceholder = 'Einddatum evenement';
   readonly dailyStartTimePlaceholder = 'Starttijd evenement';
   readonly dailyEndTimePlaceholder = 'Eindtijd evenement';
   readonly hasDayCareBeforeFieldPlaceholder = 'Opvang voor het evenement';
   readonly dayCareBeforeStartTimeFieldPlaceholder = 'Start opvang voor het evenement';
   readonly dayCareBeforeEndTimeFieldPlaceholder = 'Einde opvang voor het evenement';
   readonly hasDayCareAfterFieldPlaceholder = 'Opvang na het evenement';
   readonly dayCareAfterStartTimeFieldPlaceholder = 'Start opvang na het evenement';
   readonly dayCareAfterEndTimeFieldPlaceholder = 'Einde opvang na het evenement';
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
      descriptionFieldControl: new FormControl('', [Validators.maxLength(this.maxDescriptionLength)]),
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
      startRegistrationDateFieldControl: new FormControl(new Date(), [Validators.required]),
      endRegistrationDateFieldControl: new FormControl(new Date(), [Validators.required]),
      finalCancellationDateFieldControl: new FormControl(new Date(), [Validators.required]),
      registrationOnDailyBasisFieldControl: new FormControl('', [Validators.required]),
      startDateFieldControl: new FormControl(new Date(), [Validators.required]),
      endDateFieldControl: new FormControl(new Date(), [Validators.required]),
      dailyStartTimeFieldControl: new FormControl('00:00', [Validators.required]),
      dailyEndTimeFieldControl: new FormControl('00:00', [Validators.required]),
      hasDayCareBeforeFieldControl: new FormControl('', [Validators.required]),
      dayCareBeforeStartTimeFieldControl: new FormControl('00:00'),
      hasDayCareAfterFieldControl: new FormControl('', [Validators.required]),
      dayCareAfterEndTimeFieldControl: new FormControl('00:00'),
   });

   careBeforeRequired() {
      if (this.formControlGroup.controls.hasDayCareBeforeFieldControl.value === 'true') {
         return Validators.required;
      }
   }

   addAllowedAgeGroupsFormControlArrayToFormGroup() {
      this.formControlGroup.registerControl('allowedAgeGroupsFormControlArray', this.allowedAgeGroupsFormControlArray);
   }

   addFormControlToAllowedAgeGroupsFormControlArray() {
      this.allowedAgeGroupsFormControlArray.push(new FormControl('', [Validators.required]));
   }

   constructor(
      // dialogreference to close this dialog
      public dialogRef: MatDialogRef<SingerEventDetailsComponent>,
      // Singer event that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: SingerEventDetailsFormData
   ) {
      this.currentSingerEventInstance = data.singerEventInstance;
      this.isAdding = data.singerEventInstance === null;
      this.availableLocations = data.availableLocations;
   }

   ngOnInit() {
      this._loadInstance();

      // Subscribe to value changes for startRegistrationDateField
      this.formControlGroup.controls.startRegistrationDateFieldControl.valueChanges.subscribe((res) => {
         const minDate = res;
         const endRegistrationControl = this.formControlGroup.controls.endRegistrationDateFieldControl;
         endRegistrationControl.setValidators([Validators.required, dateNotBefore(minDate)]);
         endRegistrationControl.updateValueAndValidity();

         const finalCancellationDateFieldControl = this.formControlGroup.controls.finalCancellationDateFieldControl;
         finalCancellationDateFieldControl.setValidators([dateNotBefore(minDate)]);
         finalCancellationDateFieldControl.updateValueAndValidity();
      });

      // Subscribe to value changes for startDateField
      this.formControlGroup.controls.startDateFieldControl.valueChanges.subscribe((res) => {
         const minDate = res;
         const endDateFieldControl = this.formControlGroup.controls.endDateFieldControl;
         endDateFieldControl.setValidators([Validators.required, dateNotBefore(minDate)]);
         endDateFieldControl.updateValueAndValidity();
      });

      // Subscribe to value changes for hasDayCareBeforeField
      this.formControlGroup.controls.hasDayCareBeforeFieldControl.valueChanges.subscribe((res: string) => {
         const hasDayCareBefore = res === 'true';
         const dayCareBeforeStartTimeFieldControl = this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl;
         if (hasDayCareBefore) {
            dayCareBeforeStartTimeFieldControl.setValidators([Validators.required]);
         } else {
            dayCareBeforeStartTimeFieldControl.clearValidators();
         }
         dayCareBeforeStartTimeFieldControl.reset();
         dayCareBeforeStartTimeFieldControl.updateValueAndValidity();
      });

      // Subscribe to value changes for hasDayCareAfterField
      this.formControlGroup.controls.hasDayCareAfterFieldControl.valueChanges.subscribe((res: string) => {
         const hasDayCareAfter = res === 'true';
         const dayCareAfterEndTimeFieldControl = this.formControlGroup.controls.dayCareAfterEndTimeFieldControl;
         if (hasDayCareAfter) {
            dayCareAfterEndTimeFieldControl.setValidators([Validators.required]);
         } else {
            dayCareAfterEndTimeFieldControl.clearValidators();
         }
         dayCareAfterEndTimeFieldControl.reset();
         dayCareAfterEndTimeFieldControl.updateValueAndValidity();
      });
   }

   private _loadInstance() {
      // If we are adding a new singer event then clear all fields
      // If we are editing an existing singer event then fill in the data
      if (this.isAdding) {
         this.formControlGroup.reset();
         this.currentSingerEventInstance = new SingerEvent();
      } else {
         this.loadCurrentSingerEventInstanceValues();
      }
   }

   // Fill in the data of the current Singer Event instance
   private loadCurrentSingerEventInstanceValues() {
      this.formControlGroup.controls.titleFieldControl.reset(this.currentSingerEventInstance.title);
      this.formControlGroup.controls.descriptionFieldControl.reset(this.currentSingerEventInstance.description);
      this.selectedLocation = this.availableLocations.find((x) => x.id === this.currentSingerEventInstance.location.id);
      this.formControlGroup.controls.locationFieldControl.setValue(this.selectedLocation);
      this.selectedAgeGroups = this.currentSingerEventInstance.allowedAgeGroups;
      this.formControlGroup.controls.allowedAgeGroupsFieldControl.setValue(this.selectedAgeGroups);
      this.formControlGroup.controls.maxRegistrantsFieldControl.setValue(
         this.currentSingerEventInstance.maxRegistrants
      );
      this.formControlGroup.controls.costFieldControl.reset(this.currentSingerEventInstance.cost);
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
         this.currentSingerEventInstance.registrationOnDailyBasis ? 'true' : 'false'
      );
      this.formControlGroup.controls.startDateFieldControl.reset(this.currentSingerEventInstance.startDateTime);
      this.formControlGroup.controls.endDateFieldControl.reset(this.currentSingerEventInstance.endDateTime);
      this.formControlGroup.controls.dailyStartTimeFieldControl.reset(
         moment.utc(this.currentSingerEventInstance.startDateTime).local().format('HH:mm')
      );
      this.formControlGroup.controls.dailyEndTimeFieldControl.reset(
         moment.utc(this.currentSingerEventInstance.endDateTime).local().format('HH:mm')
      );
      this.formControlGroup.controls.hasDayCareBeforeFieldControl.reset(
         this.currentSingerEventInstance.hasDayCareBefore ? 'true' : 'false'
      );
      this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.reset(
         moment.utc(this.currentSingerEventInstance.dayCareBeforeStartDateTime).local().format('HH:mm')
      );
      this.formControlGroup.controls.hasDayCareAfterFieldControl.reset(
         this.currentSingerEventInstance.hasDayCareAfter ? 'true' : 'false'
      );
      this.formControlGroup.controls.dayCareAfterEndTimeFieldControl.reset(
         moment.utc(this.currentSingerEventInstance.dayCareAfterEndDateTime).local().format('HH:mm')
      );
   }

   compareLocations(locationX: SingerLocation, locationY: SingerLocation) {
      return locationX.id === locationY.id;
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
         this.formControlGroup.controls.registrationOnDailyBasisFieldControl.value === 'true' ? true : false;
      this.currentSingerEventInstance.startDateTime = this._handleDateTimeFields(
         new Date(this.formControlGroup.controls.startDateFieldControl.value),
         this.formControlGroup.controls.dailyStartTimeFieldControl.value
      );
      this.currentSingerEventInstance.endDateTime = this._handleDateTimeFields(
         new Date(this.formControlGroup.controls.endDateFieldControl.value),
         this.formControlGroup.controls.dailyEndTimeFieldControl.value
      );
      this.currentSingerEventInstance.hasDayCareBefore =
         this.formControlGroup.controls.hasDayCareBeforeFieldControl.value === 'true' ? true : false;
      this.currentSingerEventInstance.dayCareBeforeStartDateTime = this._handleDateTimeFields(
         new Date(this.formControlGroup.controls.startDateFieldControl.value),
         this.formControlGroup.controls.dayCareBeforeStartTimeFieldControl.value
      );
      this.currentSingerEventInstance.hasDayCareAfter =
         this.formControlGroup.controls.hasDayCareAfterFieldControl.value === 'true' ? true : false;
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

      this.updateCurrentSingerEventInstance();
      this.submitEvent.emit(this.currentSingerEventInstance);
      this.closeForm();
   }

   // Delete the event
   submitDeleteEvent() {
      this.deleteEvent.emit(this.currentSingerEventInstance);
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
