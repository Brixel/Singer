import { Component, OnInit } from '@angular/core';
import { AccountInfoService } from 'src/app/modules/core/services/account-info/account-info.service';

@Component({
  selector: 'app-account-info-page',
  templateUrl: './account-info-page.component.html',
  styleUrls: ['./account-info-page.component.css']
})
export class AccountInfoPageComponent implements OnInit {

   user: any;

  constructor(private accountInfoService: AccountInfoService) { }

  ngOnInit() {
     this.user = this.accountInfoService.getUserInfo();
  }

}
