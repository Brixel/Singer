import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventRegistrationComponent } from './components/event-registration/event-registration.component';
import { MaterialModule } from 'src/app/material.module';
import { ApiService } from '../core/services/api.service';
import { SingerEventsService } from '../core/services/singerevents-api/singerevents.service';
import { SingerEventsProxy } from '../core/services/singerevents-api/singerevents.proxy';
import { CoreModule } from '../core/core.module';

@NgModule({
   declarations: [EventRegistrationComponent],
   imports: [CommonModule, MaterialModule, CoreModule],
   exports: [EventRegistrationComponent],
   entryComponents: [EventRegistrationComponent],
   providers: [ApiService, SingerEventsService, SingerEventsProxy],
})
export class SharedModule {}
