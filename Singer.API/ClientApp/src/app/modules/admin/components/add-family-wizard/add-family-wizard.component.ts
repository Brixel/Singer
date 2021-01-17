import { Component, ViewChild } from '@angular/core';
import { AddFamilyWizardStep } from 'src/app/modules/core/models/add-family-wizard-step';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';
import { LegalguardianDetailsComponent } from '../legalguardians/legalguardian-details/legalguardian-details.component';
import { CareUserDetailsComponent } from '../careusers/careuser-details/care-user-details.component';
import { Router } from '@angular/router';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';

@Component({
   selector: 'app-add-family-wizard',
   templateUrl: './add-family-wizard.component.html',
   styleUrls: ['./add-family-wizard.component.css'],
})
export class AddFamilyWizardComponent {
   @ViewChild('stepper', { static: true }) matStepper: MatStepper;

   wizardSteps: AddFamilyWizardStep[] = [
      {
         stepLabel: 'Start',
         topText:
            'Welkom bij de Familie toevoegen wizard, hier kan je gemakkelijk niewe voogden en zorggebruikers gelijktijdig inschrijven.',
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
         topText: 'Gefeliciteerd u hebt succesvol nieuwe Voogden en Zorggebruikers toegevoegd.',
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

   moveStepperBackward() {
      this.matStepper.previous();
   }

   moveStepperForward() {
      if (this.legalGuardians.length > 0) {
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
         if (this.linkUsers()) {
            this.moveStepperForward();
         }
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
         data: {
            legalGuardianInstance: null,
            displayLinkedUserFields: false,
         },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: LegalGuardian) => {
         // Add the legal guardian
         this.legalguardiansService.createLegalGuardian(result).subscribe(
            res => {
               //Save result localy for linking users
               result.id = res.id;
               this.legalGuardians.push(result);

               this.snackBar.open(`${result.firstName} ${result.lastName} werd toegevoegd als voogd.`, 'OK', {
                  duration: 2000,
               });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   editLegalGuardian(legalGuardian: LegalGuardian): void {
      //Dereference legalGuardian to avoid updating local instance when API might refuse the update
      const deRefLegalGuardian = { ...legalGuardian };
      const dialogRef = this.dialog.open(LegalguardianDetailsComponent, {
         data: {
            legalGuardianInstance: deRefLegalGuardian,
            displayLinkedUserFields: false,
         },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: LegalGuardian) => {
         //Update the legal guardian
         this.legalguardiansService.updateLegalGuardian(result).subscribe(
            res => {
               //Update the local instance
               let legalGuardianIndex = this.legalGuardians.findIndex(guardian => guardian.id == result.id);
               this.legalGuardians[legalGuardianIndex] = result;

               this.snackBar.open(`${result.firstName} ${result.lastName} werd aangepast.`, 'OK', { duration: 2000 });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   deleteLegalGuardian(legalGuardian: LegalGuardian): void {}

   addCareUser(): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: {
            careUserInstance: null,
            displayLinkedUserFields: false,
         },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         this.careUserService.createCareUser(result).subscribe(
            res => {
               //Save result localy for linking users
               result.id = res.id;
               this.careUsers.push(result);

               this.snackBar.open(`Gebruiker ${result.firstName} ${result.lastName} werd toegevoegd.`, 'OK', {
                  duration: 2000,
               });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   editCareUser(careUser: CareUser): void {
      //Dereference careUser to avoid updating local instance when API might refuse the update
      const deRefCareUser = { ...careUser };
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: {
            careUserInstance: deRefCareUser,
            displayLinkedUserFields: false,
         },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         // Update the Careuser
         this.careUserService.updateUser(result).subscribe(
            res => {
               //Update the local instance
               let careUserIndex = this.careUsers.findIndex(user => user.id == result.id);
               this.careUsers[careUserIndex] = result;

               this.snackBar.open(`Gebruiker ${result.firstName} ${result.lastName} werd aangepast.`, 'OK', {
                  duration: 2000,
               });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   deleteCareUser(careUser: CareUser): void {}

   linkUsers(): boolean {
      //collect all the careUser id's
      const careUsersToAdd = this.careUsers.map(careUser => careUser.id);

      var linkingSuccesfull: boolean = true;

      // update the legalguardians
      this.legalGuardians.forEach(legalGuardian => {
         //Add the careusers to the updateDTO
         legalGuardian.careUsersToAdd = careUsersToAdd;

         // Update the legal guardian
         this.legalguardiansService.updateLegalGuardian(legalGuardian).subscribe(
            res => {},
            err => {
               linkingSuccesfull = false;
               this.handleApiError(err);
            }
         );
      });

      // If linking proceeded without errors
      if (linkingSuccesfull && careUsersToAdd.length > 0) {
         this.snackBar.open('De Voogden en Zorgebruikers werden succesvol gekoppeld.', 'OK', { duration: 2000 });
      }
      return linkingSuccesfull;
   }

   handleApiError(err: any) {
      if (typeof err === 'string') {
         this.snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         let messages = [];
         for (var k in err) {
            messages.push(err[k]);
         }
         this.snackBar.open(`⚠ Er zijn fouten opgetreden bij het opslagen:\n${messages.join('\n')}`, 'OK', {
            panelClass: 'multi-line-snackbar',
         });
      }
   }
}
