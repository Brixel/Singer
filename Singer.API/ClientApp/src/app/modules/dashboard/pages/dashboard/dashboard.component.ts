import { Component, OnInit } from '@angular/core';
import { MsalService, MsalBroadcastService } from '@azure/msal-angular';
import { EventMessage, EventType, AuthenticationResult, InteractionStatus } from '@azure/msal-browser';
import { filter } from 'rxjs/operators';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-dashboard',
   templateUrl: './dashboard.component.html',
   styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
   loginDisplay: boolean = false;
   isAdmin = false;
   constructor(
      private msalService: MsalService,
      private authService: AuthService,
      private msalBroadcastService: MsalBroadcastService
   ) {}

   ngOnInit() {
      this.msalBroadcastService.msalSubject$
         .pipe(filter((msg: EventMessage) => msg.eventType === EventType.LOGIN_SUCCESS))
         .subscribe((result: EventMessage) => {
            console.log(result);
            const payload = result.payload as AuthenticationResult;
            this.msalService.instance.setActiveAccount(payload.account);
         });

      this.msalBroadcastService.inProgress$
         .pipe(filter((status: InteractionStatus) => status === InteractionStatus.None))
         .subscribe(() => {
            this.setLoginDisplay();
         });

      this.authService.isAdmin$.subscribe((res) => {
         this.isAdmin = res;
      });
   }
   setLoginDisplay() {
      this.loginDisplay = this.msalService.instance.getAllAccounts().length > 0;
   }
}
