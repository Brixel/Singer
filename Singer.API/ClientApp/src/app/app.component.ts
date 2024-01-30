import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';

import {
   AuthenticationResult,
   EventMessage,
   EventType,
   InteractionStatus,
   InteractionType,
   PopupRequest,
   RedirectRequest,
} from '@azure/msal-browser';
import { MsalService, MsalBroadcastService, MSAL_GUARD_CONFIG, MsalGuardConfiguration } from '@azure/msal-angular';
import { b2cPolicyNames } from './modules/core/services/auth-config';
import { B2PCPolicyStore } from './modules/core/services/b2cpolicy.state.store';

interface Payload extends AuthenticationResult {
   idTokenClaims: {
      tfp?: string;
   };
}
@Component({
   selector: 'app-root',
   templateUrl: './app.component.html',
   styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy {
   title = 'Microsoft identity platform';
   isIframe = false;
   loginDisplay = false;
   private readonly _destroying$ = new Subject<void>();

   constructor(
      @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
      private authService: MsalService,
      private msalBroadcastService: MsalBroadcastService,
      private b2cPolicyStateStore: B2PCPolicyStore
   ) {}

   ngOnInit(): void {
      this.isIframe = window !== window.parent && !window.opener;
      this.setLoginDisplay();

      this.authService.instance.enableAccountStorageEvents(); // Optional - This will enable ACCOUNT_ADDED and ACCOUNT_REMOVED events emitted when a user logs in or out of another tab or window
      this.msalBroadcastService.inProgress$
         .pipe(
            filter((status: InteractionStatus) => status === InteractionStatus.None),
            takeUntil(this._destroying$)
         )
         .subscribe(() => {
            this.setLoginDisplay();
            this.checkAndSetActiveAccount();
         });

      this.msalBroadcastService.msalSubject$
         .pipe(
            filter(
               (msg: EventMessage) =>
                  msg.eventType === EventType.LOGIN_SUCCESS || msg.eventType === EventType.ACQUIRE_TOKEN_SUCCESS
            ),
            takeUntil(this._destroying$)
         )
         .subscribe((result: EventMessage) => {
            let payload: Payload = <AuthenticationResult>result.payload;

            /**
             * For the purpose of setting an active account for UI update, we want to consider only the auth response resulting
             * from SUSI flow. "tfp" claim in the id token tells us the policy (NOTE: legacy policies may use "acr" instead of "tfp").
             * To learn more about B2C tokens, visit https://docs.microsoft.com/en-us/azure/active-directory-b2c/tokens-overview
             */
            if (payload.idTokenClaims['tfp'] === b2cPolicyNames.editProfile) {
               window.alert('Profile has been updated successfully. \nPlease sign-in again.');
               return this.logout();
            }

            return result;
         });

      this.msalBroadcastService.msalSubject$
         .pipe(
            filter(
               (msg: EventMessage) =>
                  msg.eventType === EventType.LOGIN_FAILURE || msg.eventType === EventType.ACQUIRE_TOKEN_FAILURE
            ),
            takeUntil(this._destroying$)
         )
         .subscribe((result: EventMessage) => {
            // Add your auth error handling logic here
         });
   }

   setLoginDisplay() {
      this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
   }

   checkAndSetActiveAccount() {
      /**
       * If no active account set but there are accounts signed in, sets first account to active account
       * To use active account set here, subscribe to inProgress$ first in your component
       * Note: Basic usage demonstrated. Your app may require more complicated account selection logic
       */
      let activeAccount = this.authService.instance.getActiveAccount();

      if (!activeAccount && this.authService.instance.getAllAccounts().length > 0) {
         let accounts = this.authService.instance.getAllAccounts();
         this.authService.instance.setActiveAccount(accounts[0]);
      }
   }

   login(userFlowRequest?: RedirectRequest | PopupRequest) {
      if (this.msalGuardConfig.interactionType === InteractionType.Popup) {
         if (this.msalGuardConfig.authRequest) {
            this.authService
               .loginPopup({
                  ...this.msalGuardConfig.authRequest,
                  ...userFlowRequest,
               } as PopupRequest)
               .subscribe((response: AuthenticationResult) => {
                  this.authService.instance.setActiveAccount(response.account);
               });
         } else {
            this.authService.loginPopup(userFlowRequest).subscribe((response: AuthenticationResult) => {
               this.authService.instance.setActiveAccount(response.account);
            });
         }
      } else {
         if (this.msalGuardConfig.authRequest) {
            this.authService.loginRedirect({
               ...this.msalGuardConfig.authRequest,
               ...userFlowRequest,
            } as RedirectRequest);
         } else {
            this.authService.loginRedirect(userFlowRequest);
         }
      }
   }

   logout() {
      if (this.msalGuardConfig.interactionType === InteractionType.Popup) {
         this.authService.logoutPopup({
            mainWindowRedirectUri: '/',
         });
      } else {
         this.authService.logoutRedirect();
      }
   }

   editProfile() {
      let editProfileFlowRequest: RedirectRequest | PopupRequest = {
         authority: this.b2cPolicyStateStore.getPolicies().authorities.editProfile.authority,
         scopes: [],
      };

      this.login(editProfileFlowRequest);
   }

   // unsubscribe to events when component is destroyed
   ngOnDestroy(): void {
      this._destroying$.next(undefined);
      this._destroying$.complete();
   }
}
