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
import { DeleteConfirmationComponent } from './components/delete-confirmation/delete-confirmation.component';
import { DeleteConfirmationDialogComponent } from './components/delete-confirmation-dialog/delete-confirmation-dialog.component';

@NgModule({
   declarations: [
      EventRegistrationComponent,
      UserCardComponent,
      DetailedUserCardComponent,
      LoadingComponent,
      DeleteConfirmationDialogComponent,
   ],
   imports: [CommonModule, MaterialModule, CoreModule],
   exports: [
      EventRegistrationComponent,
      UserCardComponent,
      DetailedUserCardComponent,
      LoadingComponent,
   ],
   entryComponents: [
      EventRegistrationComponent,
      LoadingComponent,
      DeleteConfirmationComponent,
      DeleteConfirmationDialogComponent,
   ],
   providers: [ApiService, SingerEventsService, SingerEventsProxy],
})
export class SharedModule {}
