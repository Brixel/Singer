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
   MatProgressSpinnerModule
} from '@angular/material';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUsersService as CareUsersService } from '../core/services/care-users-api/care-users-api.service';
import { ApiService } from '../core/services/api.service';

@NgModule({
   declarations: [OverviewComponent],
   imports: [
      CommonModule,
      CareUsersRoutingModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatFormFieldModule,
      MatInputModule,
      MatProgressSpinnerModule,
   ],
   providers:[CareUserProxy, CareUsersService, ApiService]
})
export class CareUsersModule {}
