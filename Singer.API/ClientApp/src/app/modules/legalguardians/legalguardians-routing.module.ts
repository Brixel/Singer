import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { RegisterCareWizardComponent } from './components/register-care-wizard/register-care-wizard.component';

const routes: Routes = [
   {
      path: 'opvang',
      component: RegisterCareWizardComponent,
      canActivate: [],
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class LegalguardiansRoutingModule {}
