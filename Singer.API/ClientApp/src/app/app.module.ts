import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

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
import { registerLocaleData } from '@angular/common';
import localeBe from '@angular/common/locales/be';
import { MsalGuardConfiguration, MsalRedirectComponent } from '@azure/msal-angular';
import { IPublicClientApplication, InteractionType, PublicClientApplication } from '@azure/msal-browser';
import { MSALStateStore } from './modules/core/services/msal.state.store';
import { MsalConfigDynamicModule } from './msal-config.dynamic.module';

// Import locale settings for Belgium
registerLocaleData(localeBe);

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
      BrowserModule,
      BrowserAnimationsModule,
      AppRoutingModule,
      AdminModule,
      HttpClientModule,
      MaterialModule,
      NativeDateModule,
      MsalConfigDynamicModule.forRoot(),
   ],
   providers: [
      { provide: MAT_DATE_LOCALE, useValue: 'nl-BE' },
      { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
      { provide: DateAdapter, useClass: MomentDateAdapter },
      // AppInsightsService,
   ],
   bootstrap: [AppComponent, MsalRedirectComponent],
})
export class AppModule {}
