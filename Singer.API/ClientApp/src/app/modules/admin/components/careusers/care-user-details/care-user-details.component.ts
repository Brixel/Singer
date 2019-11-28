import {
   Component,
   OnInit,
   Output,
   EventEmitter,
   Inject,
   ViewChild,
} from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import {
   startWith,
   debounceTime,
   switchMap,
   catchError,
   map,
} from 'rxjs/operators';
import { of, Observable, BehaviorSubject } from 'rxjs';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import {
   MatDatepicker,
   DateAdapter,
   MAT_DATE_LOCALE,
   MAT_DATE_FORMATS,
} from '@angular/material';
import {
   MomentDateAdapter,
   MAT_MOMENT_DATE_FORMATS,
} from '@angular/material-moment-adapter';
import { SingerEventLocation } from 'src/app/modules/core/models/singer-event-location.dto';
import { SingerEventLocationService } from 'src/app/modules/core/services/singerevents-api/singerevent-location.service';
import { isNullOrUndefined } from 'util';
import { MY_FORMATS } from 'src/app/modules/core/core.module';
import {
   dateNotAfter,
   dateNotBefore,
} from 'src/app/modules/core/utils/custom-date-validators';

// Data we pass along with the creation of the Mat-Dialog box
export interface CareUserDetailsFormData {
   careUserInstance: CareUser;
   isAdding: boolean;
   displayContactFields: boolean;
}
@Component({
   selector: 'app-care-user-details',
   templateUrl: './care-user-details.component.html',
   styleUrls: ['./care-user-details.component.css'],
   providers: [{ provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }],
})
export class CareUserDetailsComponent implements OnInit {
   // Submit event for when the user submits the form

   @ViewChild(MatDatepicker) picker: MatDatepicker<Date>;

   @Output() submitEvent: EventEmitter<CareUser> = new EventEmitter();

   // Boolean to decide if we are adding a new user or editing an existing one
   isAdding: boolean;
   displayContactFields: boolean;

   // Boolean to check if changes have been made when editing a user
   isChangesMade: boolean;

   ageGroups = AgeGroup;

   // Current care user instance
   currentCareUserInstance: CareUser;

   selectedNormalDaycareLocation: SingerEventLocation;
   selectedVacationDaycareLocation: SingerEventLocation;
   private availableLocationsSubject = new BehaviorSubject<SingerEventLocation[]>([]);
   availableLocations$ = this.availableLocationsSubject.asObservable();
   //#region Binding properties for form:

   // Form placeholders
   readonly firstNameFieldPlaceholder = 'Voornaam';
   readonly lastNameFieldPlaceholder = 'Familienaam';
   readonly birthdayFieldPlaceholder = 'Geboortedatum';
   readonly caseNumberFieldPlaceholder = 'Dossiernr';
   readonly ageGroupFieldPlaceholder = 'Leeftijdsgroep';
   readonly isExternFieldPlaceholder = 'Klas of extern';
   readonly hasTrajectoryFieldPlaceholder = 'Trajectfunctie';
   readonly normalDaycareLocationFieldPlaceholder = 'Opvang normaal';
   readonly vacationDaycareLocationFieldPlaceholder = 'Opvang vakantie';
   readonly hasResourcesFieldPlaceholder = 'Voldoende middelen';

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
      caseNumberFieldControl: new FormControl('', [
         Validators.required,
      ]),
      ageGroupFieldControl: new FormControl('', [Validators.required]),
      isExternFieldControl: new FormControl('', [Validators.required]),
      hasTrajectoryFieldControl: new FormControl('', [Validators.required]),
      normalDaycareLocationFieldControl: new FormControl('', [
         Validators.required,
      ]),
      vacationDaycareLocationFieldControl: new FormControl('', [
         Validators.required,
      ]),
      hasResourcesFieldControl: new FormControl('', [Validators.required]),
      legalGuardianUsersSearchFieldcontrol: new FormControl(),
   });

   public legalGuardianUsersAutoComplete$: Observable<LegalGuardian[]> = null;
   breakpoint: number;

   //#endregion

   constructor(
      // dialogreference to close this dialog
      public dialogRef: MatDialogRef<CareUserDetailsComponent>,
      // Care user that we want to edit
      @Inject(MAT_DIALOG_DATA) public data: CareUserDetailsFormData,
      private _legalGuardianUserService: LegalguardiansService,
      private _singerEventLocationService: SingerEventLocationService
   ) {
      this.currentCareUserInstance = data.careUserInstance;
      this.isAdding = data.isAdding;
      this.displayContactFields = data.displayContactFields;
   }

   ngOnInit() {
      this._singerEventLocationService
         .fetchSingerEventLocationsData('asc', 'name', 0, 1000, '')
         .subscribe(res => this.availableLocationsSubject.next(res.items));
      this._loadInstance();

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
      this.breakpoint = window.innerWidth <= 500 ? 1 : 3;

      if (this.currentCareUserInstance.legalGuardianUsers === null) {
         this.currentCareUserInstance.legalGuardianUsers = new Array<
            LegalGuardian
         >();
      }
      this.currentCareUserInstance.legalGuardianUsersToAdd = new Array<
         string
      >();
      this.currentCareUserInstance.legalGuardianUsersToRemove = new Array<
         string
      >();
   }

   private _loadInstance() {
      // If we are adding a new user then clear all fields
      // If we are editing an existing user then fill in his data
      if (this.isAdding) {
         this.resetFormControls();
         this.createEmptyUser();
      } else {
         this.loadCurrentCareUserInstanceValues();
      }
   }

   //#region Error messages for required fields
   getRequiredFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required') ? 'Dit veld is verplicht' : '';
   }

   getEmailFieldErrorMessage() {
      return this.formControlGroup.controls.emailFieldControl.hasError(
         'required'
      )
         ? 'Dit veld is verplicht'
         : this.formControlGroup.controls.emailFieldControl.hasError('email')
         ? 'Dit is geen geldig email adres'
         : '';
   }
   //#endregion

   // Fill in the data of the current care usrers instance
   private loadCurrentCareUserInstanceValues() {
      this.formControlGroup.controls.firstNameFieldControl.reset(
         this.currentCareUserInstance.firstName
      );
      this.formControlGroup.controls.lastNameFieldControl.reset(
         this.currentCareUserInstance.lastName
      );
      this.formControlGroup.controls.birthdayFieldControl.reset(
         this.currentCareUserInstance.birthDay
      );
      this.formControlGroup.controls.caseNumberFieldControl.reset(
         this.currentCareUserInstance.caseNumber
      );
      this.formControlGroup.controls.ageGroupFieldControl.setValue(
         this.currentCareUserInstance.ageGroup
      );
      this.formControlGroup.controls.isExternFieldControl.reset(
         this.currentCareUserInstance.isExtern ? 'true' : 'false'
      );
      this.formControlGroup.controls.hasTrajectoryFieldControl.reset(
         this.currentCareUserInstance.hasTrajectory ? 'true' : 'false'
      );
      this.formControlGroup.controls.hasResourcesFieldControl.reset(
         this.currentCareUserInstance.hasResources ? 'true' : 'false'
      );
   }

   // Clear all form fields
   private resetFormControls() {
      this.formControlGroup.controls.firstNameFieldControl.reset();
      this.formControlGroup.controls.lastNameFieldControl.reset();
      this.formControlGroup.controls.birthdayFieldControl.reset();
      this.formControlGroup.controls.caseNumberFieldControl.reset();
      this.formControlGroup.controls.ageGroupFieldControl.reset();
      this.formControlGroup.controls.isExternFieldControl.reset();
      this.formControlGroup.controls.hasTrajectoryFieldControl.reset();
      this.formControlGroup.controls.normalDaycareLocationFieldControl.reset();
      this.formControlGroup.controls.vacationDaycareLocationFieldControl.reset();
      this.formControlGroup.controls.hasResourcesFieldControl.reset();
   }

   createEmptyUser() {
      this.currentCareUserInstance = {
         id: '',
         firstName: '',
         lastName: '',
         email: '',
         userName: '',
         birthDay: new Date(),
         caseNumber: '',
         ageGroup: AgeGroup.Toddler,
         isExtern: false,
         hasTrajectory: false,
         hasResources: false,
         legalGuardianUsersToAdd: [],
         legalGuardianUsersToRemove: [],
         legalGuardianUsers: [],
      };
   }

   // If we are editing an existing user and there are no changes return false
   checkForChanges(): boolean {
      if (this.isAdding) { return true; }
      if (
         this.currentCareUserInstance.firstName !==
         this.formControlGroup.controls.firstNameFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.lastName !==
         this.formControlGroup.controls.lastNameFieldControl.value
      ) {
         return true;
      }

      const instanceDate = new Date(this.currentCareUserInstance.birthDay);
      const formDate = new Date(
         this.formControlGroup.controls.birthdayFieldControl.value
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
         this.currentCareUserInstance.caseNumber !==
         this.formControlGroup.controls.caseNumberFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.ageGroup !==
         this.formControlGroup.controls.ageGroupFieldControl.value
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.isExtern !==
         (this.formControlGroup.controls.isExternFieldControl.value === 'true'
            ? true
            : false)
      ) {
         return true;
      }
      if (
         this.currentCareUserInstance.hasTrajectory !==
         (this.formControlGroup.controls.hasTrajectoryFieldControl.value ===
         'true'
            ? true
            : false)
      ) {
         return true;
      }

      if (
         this.currentCareUserInstance.hasResources !==
         (this.formControlGroup.controls.hasResourcesFieldControl.value ===
         'true'
            ? true
            : false)
      ) {
         return true;
      }
      if (
         (this.currentCareUserInstance.legalGuardianUsersToAdd !== undefined &&
            this.currentCareUserInstance.legalGuardianUsersToAdd.length > 0) ||
         (this.currentCareUserInstance.legalGuardianUsersToRemove !==
            undefined &&
            this.currentCareUserInstance.legalGuardianUsersToRemove.length > 0)
      ) {
         return true;
      }
      return false;
   }

   legalGuardianUserLookup(value: string): Observable<LegalGuardian[]> {
      return this._legalGuardianUserService
         .fetchLegalGuardiansData('asc', 'firstName', 0, 15, value)
         .pipe(
            map(res =>
               res.items.filter(
                  i =>
                     !this.currentCareUserInstance.legalGuardianUsers.some(
                        u => u.id === i.id
                     )
               )
            ),
            catchError(_ => {
               return of(null);
            })
         );
   }

   addLegalGuardianUser(legalGuardianUser: LegalGuardian, event: any = null) {
      if (event === null || !event.isUserInput) {
         return;
      }
      if (
         this.currentCareUserInstance.legalGuardianUsersToRemove.indexOf(
            legalGuardianUser.id
         ) > -1
      ) {
         this.currentCareUserInstance.legalGuardianUsersToRemove.splice(
            this.currentCareUserInstance.legalGuardianUsersToRemove.indexOf(
               legalGuardianUser.id
            )
         );
      } else {
         this.currentCareUserInstance.legalGuardianUsersToAdd.push(
            legalGuardianUser.id
         );
      }

      this.currentCareUserInstance.legalGuardianUsers.push(legalGuardianUser);
      this.formControlGroup.controls.legalGuardianUsersSearchFieldcontrol.reset();
   }

   deleteLegalGuardianUser(legalGuardianUser: LegalGuardian) {
      if (
         this.currentCareUserInstance.legalGuardianUsersToAdd.indexOf(
            legalGuardianUser.id
         ) > -1
      ) {
         this.currentCareUserInstance.legalGuardianUsersToAdd.splice(
            this.currentCareUserInstance.legalGuardianUsersToAdd.indexOf(
               legalGuardianUser.id
            )
         );
      } else {
         this.currentCareUserInstance.legalGuardianUsersToRemove.push(
            legalGuardianUser.id
         );
      }

      if (this.currentCareUserInstance.legalGuardianUsers !== null) {
         this.currentCareUserInstance.legalGuardianUsers = this.currentCareUserInstance.legalGuardianUsers.filter(
            u => u.id !== legalGuardianUser.id
         );
      }

      this.formControlGroup.controls.legalGuardianUsersSearchFieldcontrol.reset();
   }

   onResize(event) {
      this.breakpoint = event.target.innerWidth <= 500 ? 1 : 3;
   }

   // Load form field values into current care user instance
   private updateCurrentCareUserInstance() {
      this.currentCareUserInstance.firstName = this.formControlGroup.controls.firstNameFieldControl.value;
      this.currentCareUserInstance.lastName = this.formControlGroup.controls.lastNameFieldControl.value;
      this.currentCareUserInstance.birthDay = this.formControlGroup.controls.birthdayFieldControl.value;
      this.currentCareUserInstance.caseNumber = this.formControlGroup.controls.caseNumberFieldControl.value;
      this.currentCareUserInstance.ageGroup = this.formControlGroup.controls.ageGroupFieldControl.value;
      this.currentCareUserInstance.isExtern =
         this.formControlGroup.controls.isExternFieldControl.value === 'true'
            ? true
            : false;
      this.currentCareUserInstance.hasTrajectory =
         this.formControlGroup.controls.hasTrajectoryFieldControl.value ===
         'true'
            ? true
            : false;
      this.currentCareUserInstance.hasResources =
         this.formControlGroup.controls.hasResourcesFieldControl.value ===
         'true'
            ? true
            : false;
   }

   compareLocations(
      locationX: SingerEventLocation,
      locationY: SingerEventLocation
   ) {
      return locationX.id === locationY.id;
   }

   // Submit the form
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) {
         return;
      }

      // Check for changes and determine of an API call is necesarry
      if (this.checkForChanges()) {
         this.updateCurrentCareUserInstance();
         this.submitEvent.emit(this.currentCareUserInstance);
      }
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
