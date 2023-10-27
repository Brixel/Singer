import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { CareUserOverviewComponent } from './components/careusers/careuser-overview/care-user-overview.component';
import { AdminOverviewComponent } from './components/admin-users/admin-overview/admin-overview.component';
import { LegalguardianOverviewComponent } from './components/legalguardians/legalguardian-overview/legalguardian-overview.component';
import { SingerEventOverviewComponent } from './components/singerevents/singerevent-overview/singerevent-overview.component';
import { AddFamilyWizardComponent } from './components/add-family-wizard/add-family-wizard.component';
import { MainComponent } from 'src/app/main.component';
import { PendingRegistrationsComponent } from './components/pending-registrations/pending-registrations.component';
import { PendingActionsComponent } from './components/pending-actions/pending-actions.component';
import { LocationOverviewComponent } from './components/location-overview/location-overview.component';

const routes: Routes = [
   {
      path: 'admin',
      component: MainComponent,
      canActivate: [],
      children: [
         {
            path: 'beheerders',
            component: AdminOverviewComponent,
         },
         {
            path: 'zorggebruikers',
            component: CareUserOverviewComponent,
         },
         {
            path: 'voogden',
            component: LegalguardianOverviewComponent,
         },
         {
            path: 'evenementen',
            component: SingerEventOverviewComponent,
         },
         {
            path: 'familie-toevoegen',
            component: AddFamilyWizardComponent,
         },
         {
            path: 'te-verwerken-inschrijvingen',
            component: PendingRegistrationsComponent,
         },
         {
            path: 'te-verwerken-emails',
            component: PendingActionsComponent,
         },
         {
            path: 'locaties',
            component: LocationOverviewComponent,
         },
      ],
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
   exports: [RouterModule],
})
export class AdminRoutingModule {}
