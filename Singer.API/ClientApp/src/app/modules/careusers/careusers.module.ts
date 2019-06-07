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
})
export class CareUsersModule {}
