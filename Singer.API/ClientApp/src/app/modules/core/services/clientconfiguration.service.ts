import { Injectable } from '@angular/core';
import { IB2CPolicyNames, IProtectedResources } from './auth-config';
@Injectable({
   providedIn: 'root',
})
export class ConfigurationService {
   configuration: Configuration;
   constructor() {}
   load(): Promise<Configuration> {
      const jsonFile = `assets/config.json`;
      // using fetch bypasses the HTTP_INTERCEPTOR, which only works with the httpClient of Angular
      return fetch(jsonFile)
         .then((r) => r.json())
         .then((c: Configuration) => {
            this.configuration = c;
            return c;
         });
   }
}

export interface Configuration {
   authority: string;
   tenant: string;
   client_id: string;
   applicationinsights_intrumentationkey: string;
   b2cPolicies: IB2CPolicyNames;
   protectedResources: IProtectedResources;
}
