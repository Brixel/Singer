import { Injectable } from '@angular/core';
import { ObservableStore } from '@codewithdan/observable-store';
import { IB2CPolicies, b2cPolicyNames } from './auth-config';

@Injectable({ providedIn: 'root' })
export class B2PCPolicyStore extends ObservableStore<IB2CPolicies> {
   constructor() {
      super({});
   }

   load(authority: string, tenant: string) {
      this.setState({
         authorities: {
            signUpSignIn: {
               authority: `https://${authority}/${tenant}.onmicrosoft.com/${b2cPolicyNames.signUpSignIn}`,
            },
            editProfile: {
               authority: `https://${authority}/${tenant}.onmicrosoft.com/${b2cPolicyNames.editProfile}`,
            },
            resetPassword: {
               authority: `https://${authority}/${tenant}.onmicrosoft.com/${b2cPolicyNames.resetPassword}`,
            },
         },
         // authorityDomain: 'vzwstijn.b2clogin.com',
         authorityDomain: authority,
      });
   }

   getPolicies(): IB2CPolicies {
      return this.getState();
   }
}
