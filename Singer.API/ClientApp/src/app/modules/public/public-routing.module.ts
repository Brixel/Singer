import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from '../core/services/auth.guard';

const routes: Routes = [
   {
      path: '',
      // component: OverviewComponent,
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
})
export class AdminRoutingModule {}
