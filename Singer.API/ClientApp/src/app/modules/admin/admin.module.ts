import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { OverviewComponent } from './components/careusers/overview/overview.component';
import { MaterialModule } from '../../material.module';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUserService as CareUserService } from '../core/services/care-users-api/careusers.service';
import { ApiService } from '../core/services/api.service';
import { AgegroupPipe } from '../core/services/agegroup.pipe';
import { CoreModule } from '../core/core.module';
import { CareUserDetailsComponent } from './components/careusers/care-user-details/care-user-details.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { KeysPipe } from '../core/services/keys.pipe';
import { EventsOverviewComponent } from './components/events/events-overview/events-overview.component';
import { MatTableModule, MatPaginatorModule, MatSortModule } from '@angular/material';

@NgModule({
   declarations: [OverviewComponent, CareUserDetailsComponent, EventsOverviewComponent],
   imports: [
      CoreModule,
      CommonModule,
      AdminRoutingModule,
      MaterialModule,
      ReactiveFormsModule,
      FormsModule,
      MatMomentDateModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
   ],
   entryComponents: [
      CareUserDetailsComponent
   ],
   providers: [
      CareUserProxy,
      CareUserService,
      ApiService,
      AgegroupPipe,
      KeysPipe,
      { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }]
})
export class AdminModule { }
