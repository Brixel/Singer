import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AboutComponent } from './components/about/about.component';

const routes: Routes = [
   {
       path: '',
       component: DashboardComponent
   },
   {
      path: 'about',
      component: AboutComponent
   }
];
@NgModule({
   imports: [RouterModule.forChild(routes)]
})

export class DashboardRoutingModule{}
