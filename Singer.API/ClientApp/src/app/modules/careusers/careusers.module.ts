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
} from '@angular/material';
import { CareUserDetailsComponent } from './components/care-user-details/care-user-details.component';
import { OverlayModule } from '@angular/cdk/overlay';

@NgModule({
   declarations: [OverviewComponent, CareUserDetailsComponent],
   imports: [
      CommonModule,
      CareUsersRoutingModule,
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
      OverlayModule,
      MatCardModule
   ],
})
export class CareUsersModule {}
