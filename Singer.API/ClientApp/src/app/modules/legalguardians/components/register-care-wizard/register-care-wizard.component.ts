import { Component, ViewChild } from '@angular/core';
import { AddRegisterCareWizardStep } from './add-register-care-wizard-step';
import {
   SearchCareUserDialogComponent,
   RelatedCareUser,
} from '../search-care-user-dialog/search-care-user-dialog.component';
import { MatButtonToggleChange } from '@angular/material/button-toggle';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { CareRegistrationService } from '../../services/care-registration.service';
import { OwlDateTimeInlineComponent } from '@danielmoncada/angular-datetime-picker';

@Component({
   selector: 'app-register-care-wizard',
   templateUrl: './register-care-wizard.component.html',
   styleUrls: ['./register-care-wizard.component.css'],
})
export class RegisterCareWizardComponent {
   wizardSteps: AddRegisterCareWizardStep[] = [
      {
         stepLabel: 'Start',
         topText: 'Welkom bij de Registratie wizard, hier kan je gemakkelijk zorggebruikers inschrijven voor opvang.',
         addButtonText: '',
         middleButtonText: 'Laten we beginnen',
         backArrow: false,
         forwardArrow: false,
      },

      {
         stepLabel: 'Type opvang',
         topText: 'Maak uw keuze voor het type opvang',
         addButtonText: '',
         middleButtonText: '',
         backArrow: true,
         forwardArrow: false,
      },
      {
         stepLabel: 'Zorggebruikers toevoegen',
         topText: 'Voeg hier uw zorggebruikers toe.',
         addButtonText: 'Zorggebruiker toevoegen',
         middleButtonText: '',
         backArrow: true,
         forwardArrow: true,
      },
      {
         stepLabel: 'Opvang aanvragen',
         topText: 'Selecteer de opvangdatum',
         addButtonText: '',
         middleButtonText: '',
         backArrow: true,
         forwardArrow: true,
      },
      {
         stepLabel: 'Overzicht',
         topText: 'Bekijk hier of alle gegevens kloppen.',
         addButtonText: '',
         middleButtonText: 'Aanvraag indienen',
         backArrow: true,
         forwardArrow: false,
      },
      {
         stepLabel: 'Klaar',
         topText: 'Uw aanvraag is succesvol ontvangen.',
         addButtonText: '',
         middleButtonText: 'Terug naar Dashboard',
         backArrow: false,
         forwardArrow: false,
      },
   ];

   @ViewChild('stepper', { static: true }) stepper: MatStepper;
   @ViewChild('datetimepicker') datetimepicker: OwlDateTimeInlineComponent<Date>;

   dayCareType: EventRegistrationTypes;
   DayCareTypes = EventRegistrationTypes;

   careUsers: RelatedCareUser[] = [];

   selectedMoments: Date[] = [];
   minDate = new Date();

   dateFilter = (date: Date): boolean => {
      const day = date.getDay();

      // Prevent Saturday and Sunday from being selected.
      return day !== 0 && day !== 6;
   };
   selectedCareType: EventRegistrationTypes;

   constructor(
      private careRegistrationService: CareRegistrationService,
      public dialog: MatDialog,
      private _snackBar: MatSnackBar,
      private router: Router
   ) { }

   moveStepperBackward() {
      this.stepper.previous();
   }

   moveStepperForward(index: number) {
      if (index === 0) {
         this.stepper.next();
      }

      if (index === 1) {
         this.validateCareType();
      }

      if (index === 2) {
         this.validateCareUsers();
      }

      if (index === 3) {
         this.validateDates();
      }

      if (index === 4) {
         this.registerCare();
      }

      if (index === this.wizardSteps.length - 1) {
         this.router.navigateByUrl('/dashboard');
      }
   }

   handleAddButtonClick() {
      this.addCareUser();
   }

   onChangeCareType($event: MatButtonToggleChange) {
      this.dayCareType = <EventRegistrationTypes>$event.value;
   }

   addCareUser(): void {
      const dialogRef = this.dialog.open(SearchCareUserDialogComponent, {
         width: '80vw',
      });

      dialogRef.afterClosed().subscribe(res => {
         if (res) {
            if (!this.careUsers.map(c => c.id).includes(res.id)) {
               this.careUsers.push(res);
            }
         }
      });
   }

   onRemoveCareUser(relatedUser: RelatedCareUser) {
      this.careUsers = this.careUsers.filter(x => x !== relatedUser);
   }

   validateCareType() {
      if (this.dayCareType !== undefined) {
         this.selectedCareType = this.dayCareType;
         this.stepper.next();
      } else {
         this._snackBar.open('Selecteer een opvang type', 'OK');
         return;
      }
   }

   validateCareUsers() {
      if (this.careUsers.length === 0) {
         this._snackBar.open('Selecteer minstens één zorggebruiker om verder te gaan', 'OK');
         return;
      }

      this.stepper.next();
   }

   validateDates() {
      const selectedDateTimeValues = this.datetimepicker.selecteds;
      if (
         selectedDateTimeValues.length !== 2 ||
         selectedDateTimeValues[0] === null ||
         selectedDateTimeValues[1] === null
      ) {
         this._snackBar.open('Selecteer een begin en eind datum om verder te gaan', 'OK');
         return;
      } else {
         if (this.selectedCareType === EventRegistrationTypes.DayCare) {
            const orderedDates = selectedDateTimeValues.sort((a, b) => a.getTime() - b.getTime());
            const firstDate = orderedDates[0];
            const lastDate = orderedDates[orderedDates.length - 1];
            if (firstDate.getHours() < 8) {
               this._snackBar.open('Dagopvang is alleen mogelijk tussen 8:00 en 18:00', 'OK');
               return;
            }
            if (lastDate.getHours() > 18 || (lastDate.getHours() === 18 && lastDate.getMinutes() !== 0)) {
               this._snackBar.open('Dagopvang is alleen mogelijk tussen 8:00 en 18:00', 'OK');
               return;
            }
         }
         this.selectedMoments = selectedDateTimeValues;

         this.stepper.next();
      }
   }

   registerCare() {
      const careUserIds = this.careUsers.map(cu => cu.id);
      this.careRegistrationService
         .createCareRegistration(this.selectedCareType, this.selectedMoments, careUserIds)
         .subscribe(res => {
            this.stepper.next();
            this._snackBar.open('Aanvraag ingediend', 'OK', { duration: 2000 });
         });
   }
}

export enum EventRegistrationTypes {
   // EventSlotDriven = 1, // Not in use, since only possible via other services

   DayCare = 2,
   NightCare = 3,
}
