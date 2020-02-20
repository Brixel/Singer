import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdminUserService } from 'src/app/modules/admin/services/admin-user.service';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { UserInfoService } from 'src/app/modules/core/services/user-info/user-info.service';
import { UserDescriptionDTO } from 'src/app/modules/core/models/userdescription.model';

@Component({
   selector: 'app-account-info-page',
   templateUrl: './account-info-page.component.html',
   styleUrls: ['./account-info-page.component.css'],
})
export class AccountInfoPageComponent {
   user: UserDescriptionDTO;
   isAdmin: boolean;

   constructor(private userInfoService: UserInfoService) {
      this.userInfoService.getUserInfo().subscribe(res => {
         this.user = res;
      });
   }
}
