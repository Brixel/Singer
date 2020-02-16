import { Component, OnInit, ViewChild } from '@angular/core';
import { AddRegisterCareWizardStep } from './add-register-care-wizard-step';
import {
   SearchCareUserDialogComponent,
   RelatedCareUser,
} from '../search-care-user-dialog/search-care-user-dialog.component';
import { MatDialog, MatSnackBar, MatStepper, MatButtonToggleGroup, MatButtonToggleChange } from '@angular/material';
import { OwlDateTimeInlineComponent } from 'ng-pick-datetime';

@Component({
   selector: 'app-register-care-wizard',
   templateUrl: './register-care-wizard.component.html',
   styleUrls: ['./register-care-wizard.component.css'],
})
export class RegisterCareWizardComponent implements OnInit {
   wizardSteps: AddRegisterCareWizardStep[] = [
      {
         stepLabel: 'Start',
         topText:
            'Welkom bij de Familie toevoegen wizard, hier kan je gemakkelijk niewe voogden en zorggebruikers gelijktijdig inschrijven.',
         addButtonText: '',
         middleButtonText: 'Laten we beginnen',
         backArrow: false,
         forwardArrow: true,
      },

      {
         stepLabel: 'Type opvang',
         topText: 'Maak uw keuze voor het type opvang',
         addButtonText: '',
         middleButtonText: 'Selecteer zorggebruikers',
         backArrow: true,
         forwardArrow: false,
      },
      {
         stepLabel: 'Zorggebruikers toevoegen',
         topText: 'Voeg hier uw zorggebruikers toe.',
         addButtonText: 'Zorggebruiker toevoegen',
         middleButtonText: 'Datum selecteren',
         backArrow: true,
         forwardArrow: false,
      },
      {
         stepLabel: 'Opvang aanvragen',
         topText: 'Selecteer de opvangdatum',
         addButtonText: 'Opvangdatum selecteren',
         middleButtonText: 'Samenvatting',
         backArrow: true,
         forwardArrow: false,
      },

      {
         stepLabel: 'Klaar',
         topText: 'Aanvraag klaar om in te dienen',
         addButtonText: '',
         middleButtonText: 'Terug naar Dashboard',
         backArrow: false,
         forwardArrow: false,
      },
   ];

   @ViewChild('stepper') stepper: MatStepper;
   @ViewChild('datetimepicker') datetimepicker: OwlDateTimeInlineComponent<Date>;

   dayCareType: DayCareTypes;
   DayCareTypes = DayCareTypes;

   careUsers: RelatedCareUser[] = [];

   selectedMoments: Date[] = [];
   minDate = new Date();

   dateFilter = (date: Date): boolean => {
      const day = date.getDay();

      // Prevent Saturday and Sunday from being selected.
      return day !== 0 && day !== 6;
   };
   selectedCareType: any;

   constructor(public dialog: MatDialog, private _snackBar: MatSnackBar) {}

   ngOnInit() {}

   handleAddButtonClick() {
      this.addCareUser();
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

   handleMiddleButtonClick(selectedIndex: number) {
      if (selectedIndex === 0) {
         this.stepper.next();
      }
      if (selectedIndex === 1) {
         this.validateCareType();
      }

      if (selectedIndex === 2) {
         this.validateCareUsers();
      }
      if (selectedIndex === 3) {
         this.validateDates();
      }
   }
   validateCareType() {
      console.log(this.dayCareType);
      if (this.dayCareType !== undefined) {
         this.selectedCareType = this.dayCareType;

         this.stepper.next();
      } else {
         this._snackBar.open('Selecteer een opvang type', 'OK');
         return;
      }
   }
   validateCareUsers() {
      console.log(this.careUsers);
      if (this.careUsers.length === 0) {
         this._snackBar.open('Selecteer minstens één zorggebruiker om verder te gaan', 'OK');
         return;
      }

      this.stepper.next();
   }
   validateDates() {
      const selectedDateTimeValues = this.datetimepicker.selecteds;
      if (selectedDateTimeValues.length !== 2) {
         return;
      } else {
         this.selectedMoments = selectedDateTimeValues;

         this.stepper.next();
      }
   }

   moveStepperForward() {
      this.stepper.next();
   }

   onChangeCareType($event: MatButtonToggleChange) {
      this.dayCareType = <DayCareTypes>$event.value;
   }
}

export enum DayCareTypes {
   NightCare = 1,
   DayCare = 2,
}
