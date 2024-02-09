export const isIE =
   window.navigator.userAgent.indexOf('MSIE ') > -1 || window.navigator.userAgent.indexOf('Trident/') > -1;

export interface IB2CPolicyNames {
   signUpSignIn: string;
   editProfile: string;
   resetPassword: string;
}

export interface IB2CPolicies {
   authorities: {
      signUpSignIn: {
         authority: string;
      };
      editProfile: {
         authority: string;
      };
      resetPassword: {
         authority: string;
      };
   };
   authorityDomain: string;
   b2cPolicyNames: IB2CPolicyNames;
}

// export const endpoint = 'https://localhost:5001/';

export interface IProtectedResources {
   [name: string]: {
      endpoint: string;
      scopes: string[];
   };
}

export const loginRequest = {
   scopes: [],
};
