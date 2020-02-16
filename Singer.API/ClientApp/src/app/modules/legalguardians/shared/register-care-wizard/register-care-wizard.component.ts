import { Component, OnInit, ViewChild } from '@angular/core';
import { AddRegisterCareWizardStep } from './add-register-care-wizard-step';
import {
   SearchCareUserDialogComponent,
   RelatedCareUser,
} from '../search-care-user-dialog/search-care-user-dialog.component';
import { MatDialog, MatSnackBar, MatStepper, MatButtonToggleGroup } from '@angular/material';
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
         forwardArrow: true,
      },
      {
         stepLabel: 'Zorggebruikers toevoegen',
         topText: 'Voeg hier uw zorggebruikers toe.',
         addButtonText: 'Zorggebruiker toevoegen',
         middleButtonText: 'Datum selecteren',
         backArrow: true,
         forwardArrow: true,
      },
      {
         stepLabel: 'Opvang aanvragen',
         topText: 'Selecteer de opvangdatum',
         addButtonText: 'Opvangdatum selecteren',
         middleButtonText: 'Voltooien',
         backArrow: true,
         forwardArrow: true,
      },

      {
         stepLabel: 'Klaar',
         topText: 'Gefeliciteerd u hebt succesvol nieuwe Voogden en Zorggebruikers toegevoegd.',
         addButtonText: '',
         middleButtonText: 'Terug naar Dashboard',
         backArrow: false,
         forwardArrow: false,
      },
   ];

   @ViewChild('stepper') stepper: MatStepper;
   @ViewChild('dayCareTyype') dayCareType: MatButtonToggleGroup;
   @ViewChild('datetimepicker') datetimepicker: OwlDateTimeInlineComponent<Date>;
   careUsers: RelatedCareUser[] = [];

   selectedMoments: Date[] = [];
   minDate = new Date();

   dateFilter = (date: Date): boolean => {
      const day = date.getDay();

      // Prevent Saturday and Sunday from being selected.
      return day !== 0 && day !== 6;
   };

   constructor(public dialog: MatDialog) {}

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

   moveStepperForward() {
      if (this.stepper.selectedIndex === 3) {
         console.log(this.datetimepicker.values);
         const selectedDateTimeValues = this.datetimepicker.selecteds;
         if (selectedDateTimeValues.length !== 2) {
            return;
         } else {
            this.selectedMoments = selectedDateTimeValues;
         }
      }
      this.stepper.next();
   }
}
