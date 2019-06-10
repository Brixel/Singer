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
} from '@angular/material';
import { CareUserDetailsComponent } from './components/care-user-details/care-user-details.component';

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
      MatCardModule,
      MatIconModule,
      MatToolbarModule
   ],
})
export class CareUsersModule {}
