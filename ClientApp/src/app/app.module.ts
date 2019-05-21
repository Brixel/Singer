import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { MaterialModule } from './material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AboutComponent } from './components/about/about.component';

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      HomeComponent,
      CounterComponent,
      FetchDataComponent,
      AboutComponent,
      AuthComponent
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      JwtModule.forRoot({
         config: {
            tokenGetter: () => localStorage.getItem('token'),
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
      MaterialModule,
      BrowserAnimationsModule,
   ],
   bootstrap: [AppComponent],
})
export class AppModule {}
