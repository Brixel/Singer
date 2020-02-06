import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from './services/auth.guard';
import { MainComponent } from 'src/app/main.component';
import { RegistrationOverviewComponent } from './components/registration-overview/registration-overview.component';

const routes: Routes = [
   {
      path: '',
      component: MainComponent,
      canActivate: [AuthGuard],
      children: [
         {
            path: 'registratie-overzicht',
            component: RegistrationOverviewComponent,
         },
      ],
   },
];
@NgModule({
   imports: [RouterModule.forChild(routes)],
   exports: [RouterModule],
})
export class CoreRoutingModule {}
