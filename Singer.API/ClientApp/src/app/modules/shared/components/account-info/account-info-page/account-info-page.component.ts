import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdminUserService } from 'src/app/modules/admin/services/admin-user.service';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { UserInfoService } from 'src/app/modules/core/services/user-info/user-info.service';
import { UserDescriptionDTO } from 'src/app/modules/core/models/userdescription.model';
import { ConfirmComponent, ConfirmRequest } from '../../confirm/confirm.component';
import { MatDialog, MatSnackBar } from '@angular/material';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-account-info-page',
   templateUrl: './account-info-page.component.html',
   styleUrls: ['./account-info-page.component.css'],
})
export class AccountInfoPageComponent {
   user: UserDescriptionDTO;
   isAdmin: boolean;

   constructor(
      private userInfoService: UserInfoService,
      private dialog: MatDialog,
      private authService: AuthService,
      private _snackBar: MatSnackBar
   ) {
      this.userInfoService.getUserInfo().subscribe(res => {
         this.user = res;
      });
   }

   changePassword() {
      const dialogRef = this.dialog.open(ConfirmComponent, {
         data: <ConfirmRequest>{
            confirmMessage: `Wilt u uw wachtwoord wijzigen?`,
         },
      });
      dialogRef.afterClosed().subscribe((isConfirmed: boolean) => {
         if (isConfirmed) {
            this.authService.requestPasswordReset(this.user.id);
            this._snackBar.open(`Er is een e-mail naar ${this.user.email} verstuurd.`, 'OK', {
               duration: 2000,
            });
         }
      });
   }
}
