import { Injectable } from '@angular/core';
import { LogLevel, Configuration, BrowserCacheLocation } from '@azure/msal-browser';
import { ObservableStore } from '@codewithdan/observable-store';
import { IB2CPolicies, isIE } from './auth-config';

@Injectable({ providedIn: 'root' })
export class MSALStateStore extends ObservableStore<Configuration> {
   constructor() {
      super({});
   }

   load(b2cPolicies: IB2CPolicies, clientId: string, redirectUri: string) {
      this.setState({
         auth: {
            // clientId: '4ea3a07f-5db9-4290-b930-88806df40e9d',
            clientId,
            authority: b2cPolicies.authorities.signUpSignIn.authority,
            redirectUri,
            postLogoutRedirectUri: redirectUri,
            knownAuthorities: [b2cPolicies.authorityDomain],
         },
         cache: {
            cacheLocation: BrowserCacheLocation.LocalStorage,
            storeAuthStateInCookie: isIE,
         },
         system: {
            loggerOptions: {
               loggerCallback: (logLevel, message, containsPii) => {
                  console.log(message);
               },
               logLevel: LogLevel.Info,
               piiLoggingEnabled: false,
            },
         },
      });
   }

   getConfiguration(): Configuration {
      const state = this.getState();
      console.log(state);
      return state;
   }
}
