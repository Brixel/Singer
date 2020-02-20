import { Component, OnInit } from '@angular/core';
import { UserInfoService } from 'src/app/modules/core/services/user-info/user-info.service';
import { RelatedCareUserDTO } from 'src/app/modules/core/models/userdescription.model';

@Component({
   selector: 'app-account-info-careusers-list',
   templateUrl: './account-info-careusers-list.component.html',
   styleUrls: ['./account-info-careusers-list.component.css'],
})
export class AccountInfoCareusersListComponent implements OnInit {
   columns: number;
   careUsers: RelatedCareUserDTO[] = [];
   constructor(private userInfoService: UserInfoService) {
      this.userInfoService.getUserInfo().subscribe(res => {
         this.careUsers = res.careUsers;
      });
   }

   ngOnInit(): void {
      this.columns = this.calculateColumns(window.innerWidth);
   }

   onResize(event) {
      this.columns = this.calculateColumns(event.target.innerWidth);
   }

   calculateColumns(width: number): number {
      switch (true) {
         case width >= 1200:
            return 2;
         case width >= 800:
            return 2;
         case width >= 400:
            return 1;
         default:
            return 1;
      }
   }
}
