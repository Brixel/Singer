import { Component, OnInit, ViewChild } from '@angular/core';
import { AddFamilyWizardStep } from 'src/app/modules/core/models/add-family-wizard-step';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { MatSnackBar, MatDialog, MatStepper } from '@angular/material';
import { AddFamilyWizardDataSource } from './add-family-wizard-datasource';
import { LegalguardianDetailsComponent } from '../legalguardians/legalguardian-details/legalguardian-details.component';
import { CareUserDetailsComponent } from '../careusers/care-user-details/care-user-details.component';
import { Router } from '@angular/router';

@Component({
   selector: 'app-add-family-wizard',
   templateUrl: './add-family-wizard.component.html',
   styleUrls: ['./add-family-wizard.component.css'],
})
export class AddFamilyWizardComponent implements OnInit {

   @ViewChild('stepper') matStepper:MatStepper;

   wizardSteps: AddFamilyWizardStep[] = [
      {
         stepLabel: 'Start',
         topText:
            'Welkom bij de Familie toevoegen wizard, hier kan je gemakkelijk niewe Voogden en Zorggebruikers gelijktijdig inschrijven.',
         mainButtonText: 'Laten we beginnen',
         UserTypeToAdd: null,
      },
      {
         stepLabel: 'Voogden Toevoegen',
         topText: 'Voeg hier uw Voogden toe.',
         mainButtonText: 'Voogd Toevoegen',
         UserTypeToAdd: LegalGuardian,
      },
      {
         stepLabel: 'Zorggebruikers Toevoegen',
         topText: 'Voeg hier uw Zorggebruikers toe.',
         mainButtonText: 'Zorggebruiker Toevoegen',
         UserTypeToAdd: CareUser,
      },
      {
         stepLabel: 'Klaar',
         topText:
            'Gefeliciteerd u hebt succesvol nieuwe Voogden en Zorggebruikers toegevoegd.',
         mainButtonText: 'Terug naar Dashboard',
         UserTypeToAdd: null,
      },
   ];

   dataSource: AddFamilyWizardDataSource;

   constructor(
      public dialog: MatDialog,
      private snackBar: MatSnackBar,
      private router: Router,
      private wizardDataSource: AddFamilyWizardDataSource,
      ) {
         this.dataSource = this.wizardDataSource;
      }

   ngOnInit() {}

   handleMainButtonClick(index) {
      if(index === 0) {
         this.matStepper.next();
      }
      if(index > 0 && index < this.wizardSteps.length - 1){
         this.addUser(index);
      }
      if(index === this.wizardSteps.length - 1){
         this.router.navigateByUrl('/dashboard');
      }
   }

   addUser(index: number){
      if(this.wizardSteps[index].UserTypeToAdd == LegalGuardian) {
         this.addLegalGuardian();
      }
      if(this.wizardSteps[index].UserTypeToAdd == CareUser) {
         this.addCareUser();
      }
   }

   addLegalGuardian(): void {
      const dialogRef = this.dialog.open(LegalguardianDetailsComponent, {
         data: { legalGuardianInstance: null, isAdding: true },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe(
         (result: LegalGuardian) => {
            // Add the legal guardian
            this.dataSource.addLegalGuardian(result).subscribe(
               res => {
                  this.snackBar.open(
                     `${result.firstName} ${result.lastName} werd toegevoegd als voogd.`,
                     'OK',
                     { duration: 2000 }
                  );
               },
               err => {
                  this.handleApiError(err);
               }
            );
         }
      );
   }

   addCareUser(): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: { careUserInstance: null, isAdding: true },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         this.dataSource.addCareUser(result).subscribe(
            _ => {
               this.snackBar.open(
                  `Gebruiker ${result.firstName} ${result.lastName} werd toegevoegd.`,
                  'OK',
                  { duration: 2000 }
               );
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   handleApiError(err: any) {
      if (typeof err === 'string') {
         this.snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         let messages = [];
         for (var k in err) {
            messages.push(err[k]);
         }
         this.snackBar.open(
            `⚠ Er zijn fouten opgetreden bij het opslagen:\n${messages.join(
               '\n'
            )}`,
            'OK',
            {
               panelClass: 'multi-line-snackbar',
            }
         );
      }
   }
}
