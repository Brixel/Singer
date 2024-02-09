import { InjectionToken, NgModule, APP_INITIALIZER } from '@angular/core';
import { IPublicClientApplication, PublicClientApplication, LogLevel, InteractionType } from '@azure/msal-browser';
import {
   MsalGuard,
   MsalInterceptor,
   MsalBroadcastService,
   MsalInterceptorConfiguration,
   MsalModule,
   MsalService,
   MSAL_GUARD_CONFIG,
   MSAL_INSTANCE,
   MSAL_INTERCEPTOR_CONFIG,
   MsalGuardConfiguration,
   ProtectedResourceScopes,
} from '@azure/msal-angular';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { MSALStateStore } from './modules/core/services/msal.state.store';
import { ConfigurationService } from './modules/core/services/clientconfiguration.service';
import { B2PCPolicyStore } from './modules/core/services/b2cpolicy.state.store';
import { distinctUntilChanged } from 'rxjs/operators';

export function loggerCallback(logLevel: LogLevel, message: string) {
   console.log(message);
}

export function MSALInstanceFactory(config: MSALStateStore): IPublicClientApplication {
   const configuration = config.getConfiguration();
   return new PublicClientApplication({
      auth: configuration.auth,
      cache: configuration.cache,
      system: {
         loggerOptions: {
            loggerCallback,
            logLevel: LogLevel.Info,
            piiLoggingEnabled: false,
         },
      },
   });
}

export function MSALInterceptorConfigFactory(configurationService: ConfigurationService): MsalInterceptorConfiguration {
   const protectedResources = configurationService.configuration.protectedResources;
   const protectedResourceMap = new Map<string, Array<string | ProtectedResourceScopes> | null>();
   protectedResourceMap.set(protectedResources.default.endpoint, [...protectedResources.default.scopes]);
   // protectedResourceMap.set(protectedResources.registrationsList.endpoint, [
   //    ...protectedResources.registrationsList.scopes,
   // ]);
   // protectedResourceMap.set(protectedResources.registrationsSearch.endpoint, [
   //    ...protectedResources.registrationsSearch.scopes,
   // ]);
   // protectedResourceMap.set(protectedResources.locations.endpoint, [...protectedResources.locations.scopes]);

   return {
      interactionType: InteractionType.Redirect,
      protectedResourceMap,
   };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
   return {
      interactionType: InteractionType.Redirect,
   };
}

@NgModule({
   providers: [],
   imports: [MsalModule],
})
export class MsalConfigDynamicModule {
   static forRoot() {
      return {
         ngModule: MsalConfigDynamicModule,
         providers: [
            ConfigurationService,
            MSALStateStore,
            {
               provide: APP_INITIALIZER,
               useFactory: initializerFactory,
               deps: [ConfigurationService, MSALStateStore, B2PCPolicyStore],
               multi: true,
            },
            {
               provide: MSAL_INSTANCE,
               useFactory: MSALInstanceFactory,
               deps: [MSALStateStore],
            },
            {
               provide: MSAL_GUARD_CONFIG,
               useFactory: MSALGuardConfigFactory,
            },
            {
               provide: MSAL_INTERCEPTOR_CONFIG,
               useFactory: MSALInterceptorConfigFactory,
               deps: [ConfigurationService],
            },
            MsalService,
            MsalGuard,
            MsalBroadcastService,
            {
               provide: HTTP_INTERCEPTORS,
               useClass: MsalInterceptor,
               multi: true,
            },
         ],
      };
   }
}

export function initializerFactory(
   env: ConfigurationService,
   msalStateStore: MSALStateStore,
   b2cPolicyStateStore: B2PCPolicyStore
): any {
   const promise = env.load().then((value) => {
      b2cPolicyStateStore.stateChanged.pipe(distinctUntilChanged()).subscribe((x) => {
         if (x) {
            msalStateStore.load(x, value.client_id, '/');
         }
      });
      b2cPolicyStateStore.load(value.authority, value.tenant, value.b2cPolicies);

      console.log('finished getting configurations dynamically.');
   });
   return () => promise;
}
