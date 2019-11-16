import { Component, OnInit, ViewChild } from '@angular/core';
import { AddFamilyWizardStep } from 'src/app/modules/core/models/add-family-wizard-step';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { MatSnackBar, MatDialog, MatStepper } from '@angular/material';
import { LegalguardianDetailsComponent } from '../legalguardians/legalguardian-details/legalguardian-details.component';
import { CareUserDetailsComponent } from '../careusers/care-user-details/care-user-details.component';
import { Router } from '@angular/router';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';

@Component({
   selector: 'app-add-family-wizard',
   templateUrl: './add-family-wizard.component.html',
   styleUrls: ['./add-family-wizard.component.css'],
})
export class AddFamilyWizardComponent implements OnInit {
   @ViewChild('stepper') matStepper: MatStepper;

   wizardSteps: AddFamilyWizardStep[] = [
      {
         stepLabel: 'Start',
         topText:
            'Welkom bij de Familie toevoegen wizard, hier kan je gemakkelijk niewe Voogden en Zorggebruikers gelijktijdig inschrijven.',
         addButtonText: '',
         middleButtonText: 'Laten we beginnen',
         backArrow: false,
         forwardArrow: false,
         UserTypeToAdd: null,
      },
      {
         stepLabel: 'Voogden Toevoegen',
         topText: 'Voeg hier uw Voogden toe.',
         addButtonText: 'Voogd Toevoegen',
         middleButtonText: '',
         backArrow: true,
         forwardArrow: true,
         UserTypeToAdd: LegalGuardian,
      },
      {
         stepLabel: 'Zorggebruikers Toevoegen',
         topText: 'Voeg hier uw Zorggebruikers toe.',
         addButtonText: 'Zorggebruiker Toevoegen',
         middleButtonText: 'Voltooien',
         backArrow: true,
         forwardArrow: false,
         UserTypeToAdd: CareUser,
      },
      {
         stepLabel: 'Klaar',
         topText:
            'Gefeliciteerd u hebt succesvol nieuwe Voogden en Zorggebruikers toegevoegd.',
         addButtonText: '',
         middleButtonText: 'Terug naar Dashboard',
         backArrow: false,
         forwardArrow: false,
         UserTypeToAdd: null,
      },
   ];

   legalGuardians: LegalGuardian[] = [];
   careUsers: CareUser[] = [];

   constructor(
      public dialog: MatDialog,
      private snackBar: MatSnackBar,
      private router: Router,
      private careUserService: CareUserService,
      private legalguardiansService: LegalguardiansService
   ) {}

   ngOnInit() {}

   moveStepperBackward() {
      this.matStepper.previous();
   }

   moveStepperForward() {
      if (this.legalGuardians.length < 0) {
         this.matStepper.next();
      } else {
         this.snackBar.open('⚠ U moet eerst Voogden toevoegen.', 'OK', {
            duration: 2000,
         });
      }
   }

   handleAddButtonClick(index: number) {
      this.addUser(index);
   }

   handleMiddleButtonClick(index: number) {
      if (index === 0) {
         this.matStepper.next();
      }
      if (index === 2) {
         this.linkUsers();
      }
      if (index === this.wizardSteps.length - 1) {
         this.router.navigateByUrl('/dashboard');
      }
   }

   addUser(index: number) {
      if (this.wizardSteps[index].UserTypeToAdd == LegalGuardian) {
         this.addLegalGuardian();
      }
      if (this.wizardSteps[index].UserTypeToAdd == CareUser) {
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
            this.legalguardiansService.createLegalGuardian(result).subscribe(
               res => {
                  //Save result localy for linking users
                  result.id = res.id;
                  this.legalGuardians.push(result);

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
         this.careUserService.createCareUser(result).subscribe(
            res => {
               //Save result localy for linking users
               result.id = res.id;
               this.careUsers.push(result);

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

   linkUsers(): void {
      //collect all the careUser id's
      var careUsersToAdd: string[] = [''];
      this.careUsers.forEach(careUser => {
         careUsersToAdd.push(careUser.id);
      });

      var linkingSuccesfull: boolean = true;

      // update the legalguardians
      this.legalGuardians.forEach(legalGuardian => {
         //Add the careusers to the updateDTO
         legalGuardian.careUsersToAdd = careUsersToAdd;

         // Update the legal guardian
         this.legalguardiansService
            .updateLegalGuardian(legalGuardian)
            .subscribe(
               res => {},
               err => {
                  linkingSuccesfull = false;
                  this.handleApiError(err);
               }
            );
      });

      // If linking proceeded without errors
      if (linkingSuccesfull) {
         this.snackBar.open(
            'De Voogden en Zorgebruikers werden succesvol gekoppeld.',
            'OK',
            { duration: 2000 }
         );
      }
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
