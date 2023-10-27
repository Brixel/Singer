import { Component } from '@angular/core';
import { UserInfoService } from 'src/app/modules/core/services/user-info/user-info.service';
import { UserDescriptionDTO } from 'src/app/modules/core/models/userdescription.model';
import { ConfirmComponent, ConfirmRequest } from '../../confirm/confirm.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
   selector: 'app-account-info-summary',
   templateUrl: './account-info-summary.component.html',
   styleUrls: ['./account-info-summary.component.css'],
})
export class AccountInfoSummaryComponent {
   user: UserDescriptionDTO;
   isAdmin: boolean;

   constructor(private userInfoService: UserInfoService, private dialog: MatDialog, private _snackBar: MatSnackBar) {
      this.userInfoService.getUserInfo().subscribe((res) => {
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
            // this.authService.requestPasswordReset(this.user.id);
            this._snackBar.open(
               `Er is een e-mail naar ${this.user.email} verstuurd met verdere instructies om uw wachtwoord kunt wijzigen.`,
               'OK',
               {
                  duration: 10000,
               }
            );
         }
      });
   }
}
