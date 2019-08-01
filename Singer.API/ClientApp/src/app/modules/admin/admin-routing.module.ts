import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from '../core/services/auth.guard';
import { OverviewComponent } from './components/careusers/overview/overview.component';
import { LegalguardianOverviewComponent } from './components/legalguardians/legalguardian-overview/legalguardian-overview.component';
import { SingerEventOverviewComponent } from './components/singerevents/singerevent-overview/singerevent-overview.component'

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
   {
      path: 'voogden',
      component: LegalguardianOverviewComponent,
   },
   {
      path: 'evenementen',
      component: SingerEventOverviewComponent,
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class AdminRoutingModule {}
