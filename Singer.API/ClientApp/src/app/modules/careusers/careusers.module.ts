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
   MatProgressSpinnerModule,
   MatCardModule
} from '@angular/material';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUsersService as CareUsersService } from '../core/services/care-users-api/care-users-api.service';
import { ApiService } from '../core/services/api.service';
import { AgegroupPipe } from '../core/services/agegroup.pipe';
import { CoreModule } from '../core/core.module';

@NgModule({
   declarations: [OverviewComponent],
   imports: [
      CoreModule,
      CommonModule,
      CareUsersRoutingModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatFormFieldModule,
      MatInputModule,
      MatCardModule,
      MatProgressSpinnerModule,
   ],
   providers:[CareUserProxy, CareUsersService, ApiService, AgegroupPipe]
})
export class CareUsersModule {}
