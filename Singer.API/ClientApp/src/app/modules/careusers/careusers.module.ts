import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CareUsersRoutingModule } from './careusers-routing.module';
import { OverviewComponent } from './components/overview/overview.component';
import {
   MatTableModule,
   MatPaginatorModule,
   MatSortModule,
} from '@angular/material';
import { MatButtonModule } from '@angular/material';
import { MatIconModule } from '@angular/material';

@NgModule({
   declarations: [OverviewComponent],
   imports: [
      CommonModule,
      CareUsersRoutingModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatButtonModule,
      MatIconModule,
   ],
})
export class CareUsersModule {}
