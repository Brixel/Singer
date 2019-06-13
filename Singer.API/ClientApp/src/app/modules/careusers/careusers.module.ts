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
import { CareUserDetailsComponent } from './components/care-user-details/care-user-details.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

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
      MatToolbarModule,
      ReactiveFormsModule,
      FormsModule,
      MatDialogModule,
      MatDividerModule
   ],
   entryComponents: [
      CareUserDetailsComponent
   ]
})
export class CareUsersModule {}
