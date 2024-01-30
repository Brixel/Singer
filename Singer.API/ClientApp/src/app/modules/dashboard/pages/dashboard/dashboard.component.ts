import { Component, OnInit } from '@angular/core';
import { MsalService, MsalBroadcastService } from '@azure/msal-angular';
import { EventMessage, EventType, AuthenticationResult, InteractionStatus } from '@azure/msal-browser';
import { filter } from 'rxjs/operators';

@Component({
   selector: 'app-dashboard',
   templateUrl: './dashboard.component.html',
   styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
   loginDisplay: boolean = false;
   isAdmin = false;
   constructor(private authService: MsalService, private msalBroadcastService: MsalBroadcastService) {}

   ngOnInit() {
      this.msalBroadcastService.msalSubject$
         .pipe(filter((msg: EventMessage) => msg.eventType === EventType.LOGIN_SUCCESS))
         .subscribe((result: EventMessage) => {
            console.log(result);
            const payload = result.payload as AuthenticationResult;
            this.authService.instance.setActiveAccount(payload.account);
         });

      this.msalBroadcastService.inProgress$
         .pipe(filter((status: InteractionStatus) => status === InteractionStatus.None))
         .subscribe(() => {
            this.setLoginDisplay();
         });
   }
   setLoginDisplay() {
      this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
   }
}
