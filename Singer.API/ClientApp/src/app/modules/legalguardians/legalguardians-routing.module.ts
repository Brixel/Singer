import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from '../core/services/auth.guard';
import { NightCareComponent } from './night-care/night-care.component';

const routes: Routes = [
   {
      path: '',
      component: NightCareComponent,
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class LegalguardiansRoutingModule {}
