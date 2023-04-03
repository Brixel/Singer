import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { MainComponent } from './main.component';

const routes: Routes = [
   { path: 'login', loadChildren: () => import('./modules/login/login.module').then(m => m.LoginModule) },
   {
      path: 'auth',
      loadChildren: () => import('./modules/login/login.module').then(m => m.LoginModule),
   },
   {
      path: 'dashboard',
      loadChildren: () => import('./modules/dashboard/dashboard.module').then(m => m.DashboardModule),
      component: MainComponent,
   },
   {
      path: 'voogden',
      loadChildren: () => import('./modules/legalguardians/legalguardians.module').then(m => m.LegalguardiansModule),
      component: MainComponent,
   },
   { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];

@NgModule({
   imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
   exports: [RouterModule],
})
export class AppRoutingModule {}
