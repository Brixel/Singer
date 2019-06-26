import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { MainComponent } from './main.component';
import { AuthGuard } from './modules/core/services/auth.guard';

const routes: Routes = [
   { path: 'login',
      loadChildren: './modules/login/login.module#LoginModule'
   },
   {
      path: 'dashboard',
      loadChildren: './modules/dashboard/dashboard.module#DashboardModule',
      component: MainComponent,
   },
   {
      path: 'zorggebruikers',
      loadChildren: './modules/careusers/careusers.module#CareUsersModule',
      component: MainComponent,
      canActivate:[AuthGuard]
   },
   { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];

@NgModule({
   imports: [RouterModule.forRoot(routes)],
   exports: [RouterModule],
})
export class AppRoutingModule {}
