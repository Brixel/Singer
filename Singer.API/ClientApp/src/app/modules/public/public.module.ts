import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventListComponent } from '../dashboard/components/event-list/event-list.component';
import { EventCardComponent } from '../dashboard/components/event-card/event-card.component';
import { PublicOverviewComponent } from './public-overview/public-overview.component';

@NgModule({
  declarations: [EventListComponent, EventCardComponent, PublicOverviewComponent],
  imports: [
    CommonModule
  ]
})
export class PublicModule { }
