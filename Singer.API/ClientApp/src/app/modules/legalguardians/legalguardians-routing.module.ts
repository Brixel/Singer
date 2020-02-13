import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from '../core/services/auth.guard';
import { RegisterCareWizardComponent } from './shared/register-care-wizard/register-care-wizard.component';

const routes: Routes = [
   {
      path: '',
      component: RegisterCareWizardComponent,
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class LegalguardiansRoutingModule {}
