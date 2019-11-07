import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { MaterialModule } from 'src/app/material.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { HomeComponent } from './components/shared-components/home/home.component';
import { AboutComponent } from './components/shared-components/about/about.component';
import { RouterModule } from '@angular/router';
import { EventCardComponent } from './components/shared-components/event-card/event-card.component';
import { EventListComponent } from './components/shared-components/event-list/event-list.component';
import { CoreModule } from '../core/core.module';
import { EventSearchComponent } from './components/shared-components/event-search/event-search.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../core/services/api.service';
import { SharedModule } from '../shared/shared.module';
import { AdminMenuComponent } from './components/admin-components/admin-menu/admin-menu.component';

@NgModule({
   declarations: [
      DashboardComponent,
      HomeComponent,
      AboutComponent,
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
