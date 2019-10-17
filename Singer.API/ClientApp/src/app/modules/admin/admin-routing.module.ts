import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from '../core/services/auth.guard';
import { OverviewComponent } from './components/careusers/overview/overview.component';
import { FullscreenOverlayContainer } from '@angular/cdk/overlay';
import { AdminListComponent } from './components/admin-users/admin-list/admin-list.component';
import { LegalguardianOverviewComponent } from './components/legalguardians/legalguardian-overview/legalguardian-overview.component';
import { SingerEventOverviewComponent } from './components/singerevents/singerevent-overview/singerevent-overview.component'
import { MainComponent } from 'src/app/main.component';

const routes: Routes = [
   {
      path: 'admin',
      component: MainComponent,
      canActivate:[AuthGuard],
      children:[

         {
            path:'beheerders',
            component: AdminListComponent,
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
         }]
   },

];
@NgModule({
   imports: [RouterModule.forChild(routes)],
   exports: [RouterModule]
})
export class AdminRoutingModule {}
