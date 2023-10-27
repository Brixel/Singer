import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// import { ApplicationInsightsModule, AppInsightsService } from '@markpieszak/ng-application-insights';
import { AppComponent } from './app.component';
import { MaterialModule } from './material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { MainComponent } from './main.component';

import { NavMenuComponent } from './modules/core/components/nav-menu/nav-menu.component';
import { NativeDateModule, MAT_DATE_LOCALE, DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { AdminModule } from './modules/admin/admin.module';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { ConfigurationService } from './modules/core/services/clientconfiguration.service';
import { ApplicationInsightsService } from './modules/core/services/applicationinsights.service';
import { registerLocaleData } from '@angular/common';
import localeBe from '@angular/common/locales/be';
import { MsalGuard, MsalInterceptor, MsalModule, MsalRedirectComponent } from '@azure/msal-angular';
import { InteractionType, PublicClientApplication } from '@azure/msal-browser';
import { msalConfig, protectedResources } from './modules/core/services/auth-config';

// Import locale settings for Belgium
registerLocaleData(localeBe);

export function tokenGetter(): string {
   return localStorage.getItem('token');
}
export const MY_FORMATS = {
   parse: {
      dateInput: 'D-MM-YYYY',
   },
   display: {
      dateInput: 'D-MM-YYYY',
      monthYearLabel: 'MMM YYYY',
   },
};

@NgModule({
   declarations: [AppComponent, MainComponent, NavMenuComponent],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      AppRoutingModule,
      AdminModule,
      HttpClientModule,
      MaterialModule,
      BrowserAnimationsModule,
      NativeDateModule,
      MsalModule.forRoot(
         new PublicClientApplication(msalConfig),
         {
            // The routing guard configuration.
            interactionType: InteractionType.Redirect,
            authRequest: {
               scopes: protectedResources.todoListApi.scopes,
            },
         },
         {
            // MSAL interceptor configuration.
            // The protected resource mapping maps your web API with the corresponding app scopes. If your code needs to call another web API, add the URI mapping here.
            interactionType: InteractionType.Redirect,
            protectedResourceMap: new Map([
               [protectedResources.todoListApi.endpoint, protectedResources.todoListApi.scopes],
            ]),
         }
      ),
      // ApplicationInsightsModule.forRoot({
      //    instrumentationKeySetLater: true,
      // }),
   ],
   providers: [
      {
         provide: HTTP_INTERCEPTORS,
         useClass: MsalInterceptor,
         multi: true,
      },
      MsalGuard,
      {
         provide: APP_INITIALIZER,
         useFactory: initializeApp,
         deps: [ConfigurationService, ApplicationInsightsService],
         multi: true,
      },
      BrowserAnimationsModule,
      { provide: MAT_DATE_LOCALE, useValue: 'nl-BE' },
      { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
      { provide: DateAdapter, useClass: MomentDateAdapter },
      // AppInsightsService,
   ],
   bootstrap: [AppComponent, MsalRedirectComponent],
})
export class AppModule {}

export function initializeApp(
   configurationService: ConfigurationService,
   applicationInsightService: ApplicationInsightsService
) {
   return () => {
      configurationService
         .load()
         .toPromise()
         .then(() => {
            applicationInsightService.init();
         });
   };
}
