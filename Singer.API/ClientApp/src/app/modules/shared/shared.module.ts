import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventRegistrationComponent } from './components/event-registration/event-registration.component';
import { UserCardComponent } from './components/user-card/user-card.component';
import { DetailedUserCardComponent } from './components/detailed-user-card/detailed-user-card.component';
import { MaterialModule } from 'src/app/material.module';
import { ApiService } from '../core/services/api.service';
import { SingerEventsService } from '../core/services/singerevents-api/singerevents.service';
import { SingerEventsProxy } from '../core/services/singerevents-api/singerevents.proxy';
import { CoreModule } from '../core/core.module';
import { LoadingComponent } from './components/loading/loading.component';

@NgModule({
   declarations: [
      EventRegistrationComponent,
      UserCardComponent,
      DetailedUserCardComponent,
      LoadingComponent,
   ],
   imports: [CommonModule, MaterialModule, CoreModule],
   exports: [
      EventRegistrationComponent,
      UserCardComponent,
      DetailedUserCardComponent,
      LoadingComponent,
   ],
   entryComponents: [EventRegistrationComponent, LoadingComponent],
   providers: [ApiService, SingerEventsService, SingerEventsProxy],
})
export class SharedModule {}
