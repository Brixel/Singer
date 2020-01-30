import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AboutComponent } from '../shared/components/about/about.component';
import { AuthGuard } from '../core/services/auth.guard';

const routes: Routes = [
   {
      path: '',
      component: DashboardComponent,
   },
   {
      path: 'about',
      component: AboutComponent,
      canActivate: [AuthGuard],
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class DashboardRoutingModule {}
