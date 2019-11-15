import { Component, OnInit } from '@angular/core';
import { AddFamilyWizardStep } from 'src/app/modules/core/models/add-family-wizard-step';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { MatSnackBar } from '@angular/material';
import { AddFamilyWizardDataSource } from './add-family-wizard-datasource';

@Component({
   selector: 'app-add-family-wizard',
   templateUrl: './add-family-wizard.component.html',
   styleUrls: ['./add-family-wizard.component.css'],
})
export class AddFamilyWizardComponent implements OnInit {
   wizardSteps: AddFamilyWizardStep[] = [
      {
         stepLabel: 'Start',
         topText:
            'Welkom bij de Familie toevoegen wizard, hier kan je gemakkelijk niewe Voogden en Zorggebruikers gelijktijdig inschrijven.',
         mainButtonText: 'Laten we beginnen',
      },
      {
         stepLabel: 'Voogden Toevoegen',
         topText: 'Voeg hier uw Voogden toe.',
         mainButtonText: 'Voogd Toevoegen',
      },
      {
         stepLabel: 'Zorggebruikers Toevoegen',
         topText: 'Voeg hier uw Zorggebruikers toe.',
         mainButtonText: 'Zorggebruiker Toevoegen',
      },
      {
         stepLabel: 'Klaar',
         topText:
            'Gefeliciteerd u hebt succesvol nieuwe Voogden en Zorggebruikers toegevoegd.',
         mainButtonText: 'Terug naar Dashboard',
      },
   ];

   dataSource: AddFamilyWizardDataSource;

   constructor(private snackBar: MatSnackBar) {}

   ngOnInit() {}
}
