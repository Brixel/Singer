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
}
