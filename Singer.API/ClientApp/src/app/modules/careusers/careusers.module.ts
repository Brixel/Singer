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
   MatNativeDateModule,
   MatButtonModule,
   MatCardModule,
   MatIconModule,
   MatToolbarModule,
   MatDialogModule,
   MatDividerModule,
} from '@angular/material';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUserService as CareUserService } from "../core/services/care-users-api/careusers.service";
import { ApiService } from '../core/services/api.service';
import { AgegroupPipe } from '../core/services/agegroup.pipe';
import { CoreModule } from '../core/core.module';
import { CareUserDetailsComponent } from './components/care-user-details/care-user-details.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

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
      MatNativeDateModule,
      MatButtonModule,
      MatCardModule,
      MatIconModule,
      MatToolbarModule,
      ReactiveFormsModule,
      FormsModule,
      MatDialogModule,
      MatDividerModule
   ],
   entryComponents: [
      CareUserDetailsComponent
   ],
   providers:[CareUserProxy, CareUserService, ApiService, AgegroupPipe]
})
export class CareUsersModule {}
