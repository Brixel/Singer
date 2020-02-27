import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { MaterialModule } from 'src/app/material.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { HomeComponent } from './components/home/home.component';
import { RouterModule } from '@angular/router';
import { EventCardComponent } from './components/event-card/event-card.component';
import { EventListComponent } from './components/event-list/event-list.component';
import { CoreModule } from '../core/core.module';
import { EventFilterComponent } from './components/event-filter/event-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../core/services/api.service';
import { SharedModule } from '../shared/shared.module';
import { AdminMenuComponent } from './components/admin-menu/admin-menu.component';
import { RegistrationOverviewComponent } from './components/registration-overview/registration-overview.component';

@NgModule({
   declarations: [
      DashboardComponent,
      HomeComponent,
      EventListComponent,
      EventCardComponent,
      EventFilterComponent,
      AdminMenuComponent,
      RegistrationOverviewComponent,
   ],
   imports: [
      CoreModule,
      CommonModule,
      MaterialModule,
      DashboardRoutingModule,
      RouterModule,
      ReactiveFormsModule,
      FormsModule,
      SharedModule,
   ],
   providers: [ApiService],
})
export class DashboardModule {}
