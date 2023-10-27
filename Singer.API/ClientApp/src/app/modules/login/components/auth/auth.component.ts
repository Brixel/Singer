import { UntypedFormGroup, UntypedFormControl } from '@angular/forms';
import { OnInit, Component, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { MSAL_GUARD_CONFIG, MsalBroadcastService, MsalGuardConfiguration, MsalService } from '@azure/msal-angular';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';
import {
   AuthenticationResult,
   InteractionStatus,
   InteractionType,
   PopupRequest,
   RedirectRequest,
} from '@azure/msal-browser';

@Component({
   selector: 'app-auth-component',
   templateUrl: './auth.component.html',
   styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
   isIframe = false;
   loginDisplay = false;
   private readonly _destroying$ = new Subject<void>();
   private returnUrl: string;
   constructor(
      private _router: Router,
      private _activated: ActivatedRoute,
      private _loadingService: LoadingService,
      @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
      private authService: MsalService,
      private msalBroadcastService: MsalBroadcastService
   ) {}

   ngOnInit() {
      this.isIframe = window !== window.parent && !window.opener;

      this.msalBroadcastService.inProgress$
         .pipe(
            filter((status: InteractionStatus) => status === InteractionStatus.None),
            takeUntil(this._destroying$)
         )
         .subscribe(() => {
            this.setLoginDisplay();
         });
   }

   setLoginDisplay() {
      this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
   }

   login() {
      if (this.msalGuardConfig.interactionType === InteractionType.Popup) {
         if (this.msalGuardConfig.authRequest) {
            this.authService
               .loginPopup({ ...this.msalGuardConfig.authRequest } as PopupRequest)
               .subscribe((response: AuthenticationResult) => {
                  this.authService.instance.setActiveAccount(response.account);
               });
         } else {
            this.authService.loginPopup().subscribe((response: AuthenticationResult) => {
               this.authService.instance.setActiveAccount(response.account);
            });
         }
      } else {
         if (this.msalGuardConfig.authRequest) {
            this.authService.loginRedirect({ ...this.msalGuardConfig.authRequest } as RedirectRequest);
         } else {
            this.authService.loginRedirect();
         }
      }
   }

   logout() {
      if (this.msalGuardConfig.interactionType === InteractionType.Popup) {
         this.authService.logoutPopup({
            postLogoutRedirectUri: '/',
            mainWindowRedirectUri: '/',
         });
      } else {
         this.authService.logoutRedirect({
            postLogoutRedirectUri: '/',
         });
      }
   }

   ngOnDestroy(): void {
      this._destroying$.next(undefined);
      this._destroying$.complete();
   }
}
