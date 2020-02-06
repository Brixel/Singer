import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { MainComponent } from './main.component';
import { RegistrationOverviewComponent } from './modules/core/components/registration-overview/registration-overview.component';

const routes: Routes = [
   { path: 'login', loadChildren: './modules/login/login.module#LoginModule' },
   {
      path: 'auth',
      loadChildren: './modules/login/login.module#LoginModule',
   },
   {
      path: 'dashboard',
      loadChildren: './modules/dashboard/dashboard.module#DashboardModule',
      component: MainComponent,
   },
   {
      path: 'registratie-overzicht',
      component: RegistrationOverviewComponent,
   },
   { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];

@NgModule({
   imports: [RouterModule.forRoot(routes)],
   exports: [RouterModule],
})
export class AppRoutingModule {}
