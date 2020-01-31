import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AboutComponent } from './components/about/about.component';
import { AuthGuard } from '../core/services/auth.guard';
import { AccountInfoPageComponent } from '../shared/components/account-info/account-info-page/account-info-page.component';
import { AccountInfoCareusersListComponent } from '../shared/components/account-info/account-info-careusers-list/account-info-careusers-list.component';

const routes: Routes = [
   {
      path: '',
      component: DashboardComponent,
   },
   {
      path: '/about',
      component: AboutComponent,
      canActivate: [AuthGuard],
   },
   {
      path: 'account/account-info',
      component: AccountInfoPageComponent,
      canActivate: [AuthGuard],
   },
   {
      path: 'account/zorggebruikers',
      component: AccountInfoCareusersListComponent,
      canActivate: [AuthGuard],
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class DashboardRoutingModule {}
