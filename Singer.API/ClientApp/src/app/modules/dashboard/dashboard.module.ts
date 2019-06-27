import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { MaterialModule } from 'src/app/material.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from './components/about/about.component';
import {RouterModule} from '@angular/router';

@NgModule({
  declarations: [DashboardComponent, HomeComponent, AboutComponent],
  imports: [
    CommonModule, MaterialModule, DashboardRoutingModule, RouterModule
  ]
})
export class DashboardModule { }
