import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { MainComponent } from './main.component';
import { BrowserUtils } from '@azure/msal-browser';
import { MsalGuard, MsalRedirectComponent } from '@azure/msal-angular';

const routes: Routes = [
   { path: 'login', loadChildren: () => import('./modules/login/login.module').then((m) => m.LoginModule) },
   {
      // Needed for handling redirect after login
      path: 'auth',
      component: MsalRedirectComponent,
   },
   {
      path: 'dashboard',
      loadChildren: () => import('./modules/dashboard/dashboard.module').then((m) => m.DashboardModule),
      component: MainComponent,
   },
   {
      path: 'voogden',
      loadChildren: () => import('./modules/legalguardians/legalguardians.module').then((m) => m.LegalguardiansModule),
      canActivate: [MsalGuard],
      component: MainComponent,
   },
   { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];

@NgModule({
   imports: [
      RouterModule.forRoot(routes, {
         relativeLinkResolution: 'legacy', // Don't perform initial navigation in iframes or popups
         initialNavigation: !BrowserUtils.isInIframe() && !BrowserUtils.isInPopup() ? 'enabledNonBlocking' : 'disabled', // Set to enabledBlocking to use Angular Universal
      }),
   ],
   exports: [RouterModule],
})
export class AppRoutingModule {}
