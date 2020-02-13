import { Component, OnInit } from '@angular/core';
import { AddRegisterCareWizardStep } from './add-register-care-wizard-step';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { CareUser, RelatedCareUserDTO } from 'src/app/modules/core/models/careuser.model';
import {
   SearchCareUserDialogComponent,
   RelatedCareUser,
} from '../search-care-user-dialog/search-care-user-dialog.component';
import { MatDialog, MatSnackBar } from '@angular/material';

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

   currentWizardStep: AddRegisterCareWizardStep;

   careUsers: RelatedCareUser[] = [];

   constructor(public dialog: MatDialog, private snackBar: MatSnackBar) {}

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
}
