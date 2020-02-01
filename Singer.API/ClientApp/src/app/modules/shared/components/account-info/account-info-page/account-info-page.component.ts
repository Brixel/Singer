import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdminUserService } from 'src/app/modules/admin/services/admin-user.service';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';

@Component({
   selector: 'app-account-info-page',
   templateUrl: './account-info-page.component.html',
   styleUrls: ['./account-info-page.component.css'],
})
export class AccountInfoPageComponent {
   private userInfoURL = this.baseUrl + 'connect/userinfo';
   user: any;
   isAdmin: boolean;

   constructor(
      private http: HttpClient,
      @Inject('BASE_URL') private baseUrl: string,
      private adminService: AdminUserService,
      private legalGuardianService: LegalguardiansService
   ) {
      this.getUserInfo();
   }

   getUserInfo(): any {
      this.http.get(this.userInfoURL).subscribe(res => {
         let userId = (res as any).sub;
         if ((res as any).role === 'Administrator') {
            this.isAdmin = true;
            this.adminService.getAdmin(userId).subscribe(res => {
               this.user = res;
            });
         } else if ((res as any).role !== 'Administrator') {
            this.legalGuardianService.getLegalGuardian(userId).subscribe(res => {
               this.user = res;
            });
         }
      });
   }
}
