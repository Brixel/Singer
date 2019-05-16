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
import { TestComponentComponent } from './components/test-component/test-component.component';
import { JwtModule, JwtHelperService } from '@auth0/angular-jwt';
import { AppRoutingModule } from './app-routing.module';
import { AuthService } from './core/services/auth.service';
import { AuthGuard } from './core/services/auth.guard';
import { AuthComponent } from './components/auth/auth.component';
import { AuthInterceptor } from './core/services/auth-interceptor';

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      HomeComponent,
      CounterComponent,
      FetchDataComponent,
      TestComponentComponent,
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
   ],
   bootstrap: [AppComponent],
})
export class AppModule {}
