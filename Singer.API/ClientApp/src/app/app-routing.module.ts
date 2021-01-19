import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { MainComponent } from './main.component';

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
      path: 'voogden',
      loadChildren: './modules/legalguardians/legalguardians.module#LegalguardiansModule',
      component: MainComponent,
   },
   { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];

@NgModule({
   imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
   exports: [RouterModule],
})
export class AppRoutingModule {}
