import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from '../core/services/auth.guard';
import { OverviewComponent } from './components/careusers/overview/overview.component';
import { FullscreenOverlayContainer } from '@angular/cdk/overlay';

const routes: Routes = [
   {
      path: '',
      redirectTo: '/dashboard',
      pathMatch: 'full',
   },
   {
      path: 'zorggebruikers',
      component: OverviewComponent,
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class AdminRoutingModule {}
