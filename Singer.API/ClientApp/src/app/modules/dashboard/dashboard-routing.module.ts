import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AboutComponent } from '../core/components/about/about.component';

import { AccountInfoCareusersListComponent } from '../shared/components/account-info/account-info-careusers-list/account-info-careusers-list.component';
import { AccountInfoSummaryComponent } from '../shared/components/account-info/account-info-summary/account-info-summary.component';
import { RegistrationOverviewComponent } from './components/registration-overview/registration-overview.component';

const routes: Routes = [
   {
      path: '',
      component: DashboardComponent,
   },
   {
      path: 'about',
      component: AboutComponent,
      canActivate: [],
   },
   {
      path: 'account/account-info',
      component: AccountInfoSummaryComponent,
      canActivate: [],
   },
   {
      path: 'account/zorggebruikers',
      component: AccountInfoCareusersListComponent,
      canActivate: [],
   },
   {
      path: 'registratie-overzicht',
      component: RegistrationOverviewComponent,
      canActivate: [],
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class DashboardRoutingModule {}
