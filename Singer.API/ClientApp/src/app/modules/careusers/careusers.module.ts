import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CareUsersRoutingModule } from './careusers-routing.module';
import { OverviewComponent } from './components/overview/overview.component';
import {
   MatTableModule,
   MatPaginatorModule,
   MatSortModule,
   MatFormFieldModule,
   MatInputModule,
   MatOptionModule,
   MatSelectModule,
   MatProgressSpinnerModule,
   MatDatepickerModule,
   MatButtonModule,
   MatCardModule,
   MatIconModule,
   MatToolbarModule,
   MatDialogModule,
   MatDividerModule,
} from '@angular/material';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUserService as CareUserService } from '../core/services/care-users-api/careusers.service';
import { ApiService } from '../core/services/api.service';
import { AgegroupPipe } from '../core/services/agegroup.pipe';
import { CoreModule } from '../core/core.module';
import { CareUserDetailsComponent } from './components/care-user-details/care-user-details.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';

@NgModule({
   declarations: [OverviewComponent, CareUserDetailsComponent],
   imports: [
      CoreModule,
      CommonModule,
      CareUsersRoutingModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatFormFieldModule,
      MatInputModule,
      MatOptionModule,
      MatSelectModule,
      MatCardModule,
      MatProgressSpinnerModule,
      MatDatepickerModule,
      MatButtonModule,
      MatCardModule,
      MatIconModule,
      MatToolbarModule,
      ReactiveFormsModule,
      FormsModule,
      MatDialogModule,
      MatDividerModule,
      MatMomentDateModule
   ],
   entryComponents: [
      CareUserDetailsComponent
   ],
   providers: [
      CareUserProxy,
      CareUserService,
      ApiService,
      AgegroupPipe,
      { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }]
})
export class CareUsersModule {}
