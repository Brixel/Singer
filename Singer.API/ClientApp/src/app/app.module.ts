import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { DashboardComponent } from './modules/dashboard/pages/dashboard/dashboard.component';
import { MAT_DATE_FORMATS, NativeDateModule } from '@angular/material';

export const MY_FORMATS = {
   parse: {
     dateInput: 'D-MM-YYYY',
   },
   display: {
     dateInput: 'D-MM-YYYY'
   },
 };
export function tokenGetter():string {
   return localStorage.getItem('token');
 }
@NgModule({
   declarations: [
      AppComponent,
      MainComponent,
      NavMenuComponent
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      AppRoutingModule,
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
      AuthService,
      JwtHelperService,
      AuthGuard,
      {
         provide: HTTP_INTERCEPTORS,
         useClass: AuthInterceptor,
         multi: true,
      },
      {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS},
      BrowserAnimationsModule,
   ],
   bootstrap: [AppComponent],
})
export class AppModule {}
