import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdminUserService } from 'src/app/modules/admin/services/admin-user.service';
import { LegalguardiansService } from '../legal-guardians-api/legalguardians.service';

@Injectable({
   providedIn: 'root',
})
export class AccountInfoService {
   private userInfoURL = this.baseUrl + 'connect/userinfo';
   user: any;

   constructor(
      private http: HttpClient,
      @Inject('BASE_URL') private baseUrl: string,
      private adminService: AdminUserService,
      private legalGuardianService: LegalguardiansService
   ) {}

   getUserInfo(): any {
      this.http.get(this.userInfoURL).subscribe(res => {
         let userId = (res as any).sub;
         if ((res as any).role === 'Administrator') {
            this.adminService.getAdmin(userId).subscribe(res => {
               this.user = res;
            });
         } else if ((res as any).role === 'LegalGuardian') {
            this.legalGuardianService.getLegalGuardian(userId).subscribe(res => {
               this.user = res;
            })
         }
      });
   }
}
