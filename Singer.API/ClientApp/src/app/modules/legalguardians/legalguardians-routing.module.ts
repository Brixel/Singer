import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { RegisterCareWizardComponent } from './components/register-care-wizard/register-care-wizard.component';
import { MsalGuard } from '@azure/msal-angular';

const routes: Routes = [
   {
      path: 'opvang',
      component: RegisterCareWizardComponent,
      canActivate: [MsalGuard],
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class LegalguardiansRoutingModule {}
