import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CareUsersRoutingModule } from './careusers-routing.module';
import { OverviewComponent } from './components/overview/overview.component';

@NgModule({
  declarations: [OverviewComponent],
  imports: [
    CommonModule, CareUsersRoutingModule
  ]
})
export class CareUsersModule { }
