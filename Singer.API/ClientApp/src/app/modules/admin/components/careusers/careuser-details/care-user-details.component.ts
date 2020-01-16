import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';
import { startWith, debounceTime, switchMap, catchError, map } from 'rxjs/operators';
import { of, Observable, BehaviorSubject } from 'rxjs';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { SingerEventLocationService } from 'src/app/modules/core/services/singerevents-api/singerevent-location.service';
import { dateNotAfter, dateNotBefore } from 'src/app/modules/core/utils/custom-date-validators';
import { SingerEventLocation } from 'src/app/modules/core/models/singerevent.model';

// Data we pass along with the creation of the M at-Dialog box
export interface CareUserDetailsFormData {
   careUserInstance: CareUser;
   displayLinkedUserFields: boolean;
}
@Component({
   selector: 'app-care-user-details',
   templateUrl: './care-user-details.component.html',
   styleUrls: ['./care-user-details.component.css'],
})
export class CareUserDetailsComponent implements OnInit {
   // Submit event for when the user submits the form
   @Output() submitEvent: EventEmitter<CareUser> = new EventEmitter();

   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;
   // Boolean to decide if we want to dispaly the legalguardian Fields
   displayLinkedUserFields: boolean;

   // Current care user instance
   currentCareUserInstance: CareUser;

   // Available Agegroups
   ageGroups = AgeGroup;

   // Available DayCare Locations
   private availableLocationsSubject = new BehaviorSubject<SingerEventLocation[]>([]);
   availableLocations$ = this.availableLocationsSubject.asObservable();

   // For holding result from Legalguardian lookup change events
   legalGuardianUsersAutoComplete$: Observable<LegalGuardian[]> = null;

   // Number of colums for legalguardian user cards
   columns: number;

   // Form placeholders
   readonly firstNameFieldPlaceholder = 'Voornaam';
   readonly lastNameFieldPlaceholder = 'Familienaam';
   readonly birthdayFieldPlaceholder = 'Geboortedatum';
   readonly caseNumberFieldPlaceholder = 'Dossiernr';
   readonly ageGroupFieldPlaceholder = 'Leeftijdsgroep';
   readonly isExternFieldPlaceholder = 'Klas of extern';
   readonly hasTrajectoryFieldPlaceholder = 'Trajectfunctie';

   // Form validation values
   readonly minBirthday: Date = new Date(1900, 0, 1);
   readonly maxBirthday: Date = new Date();
   readonly maxNameLength = 100;
   readonly minNameLength = 2;
   readonly maxEmailLength = 255;
   readonly nameRegex = /^[\w'À-ÿ][\w' À-ÿ]*[\w'À-ÿ]+$/;

   // Form control group
   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      firstNameFieldControl: new FormControl('', [
         Validators.required,
         Validators.maxLength(this.maxNameLength),
         Validators.minLength(this.minNameLength),
         Validators.pattern(this.nameRegex),
      ]),
      lastNameFieldControl: new FormControl('', [
         Validators.required,
         Validators.maxLength(this.maxNameLength),
         Validators.minLength(this.minNameLength),
         Validators.pattern(this.nameRegex),
      ]),
      birthdayFieldControl: new FormControl(null, [
         Validators.required,
         dateNotBefore(this.minBirthday),
         dateNotAfter(this.maxBirthday),
      ]),
      caseNumberFieldControl: new FormControl('', [Validators.required]),
      ageGroupFieldControl: new FormControl('', [Validators.required]),
      isExternFieldControl: new FormControl('', [Validators.required]),
      hasTrajectoryFieldControl: new FormControl('', [Validators.required]),
      legalGuardianUsersSearchFieldcontrol: new FormControl(),
   });

   constructor(
      // Dialogreference to close this dialog
      public dialogRef: MatDialogRef<CareUserDetailsComponent>,
      // Care user that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: CareUserDetailsFormData,
      private _legalGuardianUserService: LegalguardiansService,
      private _singerEventLocationService: SingerEventLocationService
   ) {
      this.currentCareUserInstance = data.careUserInstance;
      this.isAdding = data.careUserInstance === null;
      this.displayLinkedUserFields = data.displayLinkedUserFields;
   }

   ngOnInit() {
      //Load currentCareUser Instance
      this._loadInstance();

      // Load dayCare locations
      this._singerEventLocationService
         .fetchSingerEventLocationsData('asc', 'name', 0, 1000, '')
         .subscribe(res => this.availableLocationsSubject.next(res.items));

      // Subscribe to Legalguardian lookup events
      this.legalGuardianUsersAutoComplete$ = this.formControlGroup.controls[
         'legalGuardianUsersSearchFieldcontrol'
      ].valueChanges.pipe(
         startWith(''),
         debounceTime(300),
         switchMap(value => {
            if (typeof value === 'string') {
               return this.legalGuardianUserLookup(value);
            } else {
               return of(null);
            }
         })
      );

      this.clearLegalGuardianArrays();

      // Set colums for legalguardian user cards
      this.columns = this.calculateColumns(window.innerWidth);
   }

   private _loadInstance() {
      // If we are adding a new user then clear all fields
      // If we are editing an existing user then fill in his data
      if (this.isAdding) {
         this.formControlGroup.reset();
         this.currentCareUserInstance = new CareUser();
      } else {
         this.loadCurrentCareUserInstanceValues();
      }
   }

   // Fill in the data of the current care usrers instance
   private loadCurrentCareUserInstanceValues() {
      this.formControlGroup.controls.firstNameFieldControl.reset(this.currentCareUserInstance.firstName);
      this.formControlGroup.controls.lastNameFieldControl.reset(this.currentCareUserInstance.lastName);
      this.formControlGroup.controls.birthdayFieldControl.reset(this.currentCareUserInstance.birthDay);
      this.formControlGroup.controls.caseNumberFieldControl.reset(this.currentCareUserInstance.caseNumber);
      this.formControlGroup.controls.ageGroupFieldControl.setValue(this.currentCareUserInstance.ageGroup);
      this.formControlGroup.controls.isExternFieldControl.reset(
         this.currentCareUserInstance.isExtern ? 'true' : 'false'
      );
      this.formControlGroup.controls.hasTrajectoryFieldControl.reset(
         this.currentCareUserInstance.hasTrajectory ? 'true' : 'false'
      );
   }

   // Clear the currentCareUserInstance legalguardian properties
   private clearLegalGuardianArrays() {
      if (this.currentCareUserInstance.legalGuardianUsers === null) {
         this.currentCareUserInstance.legalGuardianUsers = new Array<LegalGuardian>();
      }
      this.currentCareUserInstance.legalGuardianUsersToAdd = new Array<string>();
      this.currentCareUserInstance.legalGuardianUsersToRemove = new Array<string>();
   }

   legalGuardianUserLookup(value: string): Observable<LegalGuardian[]> {
      return this._legalGuardianUserService.fetchLegalGuardiansData('asc', 'firstName', 0, 15, value).pipe(
         map(res => res.items.filter(i => !this.currentCareUserInstance.legalGuardianUsers.some(u => u.id === i.id))),
         catchError(_ => {
            return of(null);
         })
      );
   }

   addLegalGuardianUser(legalGuardianUser: LegalGuardian, event: any = null) {
      if (event === null || !event.isUserInput) {
         return;
      }
      if (this.currentCareUserInstance.legalGuardianUsersToRemove.indexOf(legalGuardianUser.id) > -1) {
         this.currentCareUserInstance.legalGuardianUsersToRemove.splice(
            this.currentCareUserInstance.legalGuardianUsersToRemove.indexOf(legalGuardianUser.id)
         );
      } else {
         this.currentCareUserInstance.legalGuardianUsersToAdd.push(legalGuardianUser.id);
      }

      this.currentCareUserInstance.legalGuardianUsers.push(legalGuardianUser);
      this.formControlGroup.controls.legalGuardianUsersSearchFieldcontrol.reset();
   }

   deleteLegalGuardianUser(legalGuardianUser: LegalGuardian) {
      if (this.currentCareUserInstance.legalGuardianUsersToAdd.indexOf(legalGuardianUser.id) > -1) {
         this.currentCareUserInstance.legalGuardianUsersToAdd.splice(
            this.currentCareUserInstance.legalGuardianUsersToAdd.indexOf(legalGuardianUser.id)
         );
      } else {
         this.currentCareUserInstance.legalGuardianUsersToRemove.push(legalGuardianUser.id);
      }

      if (this.currentCareUserInstance.legalGuardianUsers !== null) {
         this.currentCareUserInstance.legalGuardianUsers = this.currentCareUserInstance.legalGuardianUsers.filter(
            u => u.id !== legalGuardianUser.id
         );
      }

      this.formControlGroup.controls.legalGuardianUsersSearchFieldcontrol.reset();
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

   // Load form field values into current care user instance
   private updateCurrentCareUserInstance() {
      this.currentCareUserInstance.firstName = this.formControlGroup.controls.firstNameFieldControl.value;
      this.currentCareUserInstance.lastName = this.formControlGroup.controls.lastNameFieldControl.value;
      this.currentCareUserInstance.birthDay = this.formControlGroup.controls.birthdayFieldControl.value;
      this.currentCareUserInstance.caseNumber = this.formControlGroup.controls.caseNumberFieldControl.value;
      this.currentCareUserInstance.ageGroup = this.formControlGroup.controls.ageGroupFieldControl.value;
      this.currentCareUserInstance.isExtern =
         this.formControlGroup.controls.isExternFieldControl.value === 'true' ? true : false;
      this.currentCareUserInstance.hasTrajectory =
         this.formControlGroup.controls.hasTrajectoryFieldControl.value === 'true' ? true : false;
   }

   // Submit the form
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) {
         return;
      }

      this.updateCurrentCareUserInstance();
      this.submitEvent.emit(this.currentCareUserInstance);
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
