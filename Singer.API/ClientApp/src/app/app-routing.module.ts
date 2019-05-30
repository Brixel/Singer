import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthComponent } from './components/auth/auth.component';
import { AuthGuard } from './core/services/auth.guard';

const routes: Routes = [
   { path: '', component: HomeComponent, pathMatch: 'full' },
   { path: 'counter', component: CounterComponent },
   { path: 'fetch-data', component: FetchDataComponent, canActivate:[AuthGuard] },
   { path: 'login', component: AuthComponent },
];

@NgModule({
   imports: [RouterModule.forRoot(routes)],
   exports: [RouterModule],
})
export class AppRoutingModule {}
