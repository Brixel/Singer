import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CareUsersRoutingModule } from './careusers-routing.module';
import { OverviewComponent } from './components/overview/overview.component';
import { MatTableModule } from '@angular/material';

@NgModule({
  declarations: [OverviewComponent],
  imports: [
    CommonModule, CareUsersRoutingModule, MatTableModule
  ]
})
export class CareUsersModule { }
