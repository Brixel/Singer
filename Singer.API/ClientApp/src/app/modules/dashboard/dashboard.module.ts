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
import { EventSearchComponent } from './components/event-search/event-search.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../core/services/api.service';
import { SharedModule } from '../shared/shared.module';
import { AdminMenuComponent } from './components/admin-menu/admin-menu.component';

@NgModule({
   declarations: [
      DashboardComponent,
      HomeComponent,
      EventListComponent,
      EventCardComponent,
      EventSearchComponent,
      AdminMenuComponent,
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
