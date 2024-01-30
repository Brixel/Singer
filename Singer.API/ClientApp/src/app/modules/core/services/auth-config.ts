export const isIE =
   window.navigator.userAgent.indexOf('MSIE ') > -1 || window.navigator.userAgent.indexOf('Trident/') > -1;
export const b2cPolicyNames = {
   signUpSignIn: '',
   editProfile: '',
   resetPassword: '',
};

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
}

export const endpoint = 'https://localhost:5001/api/';

export const protectedResources = {
   default: {
      endpoint: `${endpoint}*`,
      scopes: ['https://vzwstijn.onmicrosoft.com/4ea3a07f-5db9-4290-b930-88806df40e9d/Events.Read'],
   },
};

export const loginRequest = {
   scopes: [],
};
