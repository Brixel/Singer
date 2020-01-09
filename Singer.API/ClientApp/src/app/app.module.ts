import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { MaterialModule } from './material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { JwtModule, JwtHelperService } from '@auth0/angular-jwt';
import { MainComponent } from './main.component';
import { AuthService } from './modules/core/services/auth.service';
import { AuthGuard } from './modules/core/services/auth.guard';
import { AuthInterceptor } from './modules/core/services/auth-interceptor';
import { NavMenuComponent } from './modules/core/components/nav-menu/nav-menu.component';
import {
   NativeDateModule,
   MAT_DATE_LOCALE,
   DateAdapter,
   MAT_DATE_FORMATS,
} from '@angular/material';
import { AdminModule } from './modules/admin/admin.module';
import { MomentDateAdapter } from '@angular/material-moment-adapter';

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
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
         },
      }),
   ],
   providers: [
      JwtHelperService,
      AuthGuard,
      {
         provide: HTTP_INTERCEPTORS,
         useClass: AuthInterceptor,
         multi: true,
      },
      {
         provide: APP_INITIALIZER,
         useFactory: initializeApp,
         deps: [AuthService],
         multi: true,
      },
      BrowserAnimationsModule,
      { provide: MAT_DATE_LOCALE, useValue: 'nl-BE' },
      { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
      { provide: DateAdapter, useClass: MomentDateAdapter },
   ],
   bootstrap: [AppComponent],
})
export class AppModule {}

export function initializeApp(authService: AuthService) {
   return () => {
      authService.restore();
   };
}
