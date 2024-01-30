import { Injectable } from '@angular/core';
import { ObservableStore } from '@codewithdan/observable-store';
import { IB2CPolicies, IB2CPolicyNames } from './auth-config';

@Injectable({ providedIn: 'root' })
export class B2PCPolicyStore extends ObservableStore<IB2CPolicies> {
   constructor() {
      super({});
   }

   load(authority: string, tenant: string, b2cPolicyNames: IB2CPolicyNames) {
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
         authorityDomain: authority,
         b2cPolicyNames: b2cPolicyNames,
      });
   }

   getPolicies(): IB2CPolicies {
      return this.getState();
   }
}
