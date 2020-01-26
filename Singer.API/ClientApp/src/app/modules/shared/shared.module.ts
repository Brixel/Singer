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
import { EventTimelineComponent } from './components/event-timeline/event-timeline.component';
import { ConfirmComponent } from './components/confirm/confirm.component';
import { DeleteConfirmationComponent } from './components/delete-confirmation/delete-confirmation.component';
import { DeleteConfirmationDialogComponent } from './components/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountInfoBaseComponent } from './components/account-info/account-info-base/account-info-base.component';
import { AccountInfoPageComponent } from './components/account-info/account-info-page/account-info-page.component';
import { AccountInfoCareusersListComponent } from './components/account-info/account-info-careusers-list/account-info-careusers-list.component';

@NgModule({
   declarations: [
      EventRegistrationComponent,
      UserCardComponent,
      DetailedUserCardComponent,
      LoadingComponent,
      EventTimelineComponent,
      ConfirmComponent,
      DeleteConfirmationComponent,
      DeleteConfirmationDialogComponent,
      AccountInfoBaseComponent,
      AccountInfoPageComponent,
      AccountInfoCareusersListComponent,
   ],
   imports: [CommonModule, MaterialModule, CoreModule, ReactiveFormsModule],
   exports: [
      EventRegistrationComponent,
      UserCardComponent,
      DetailedUserCardComponent,
      LoadingComponent,
      EventTimelineComponent,
      ConfirmComponent,
      DeleteConfirmationComponent,
      DeleteConfirmationDialogComponent,
      AccountInfoBaseComponent,
      AccountInfoPageComponent,
      AccountInfoCareusersListComponent,
   ],
   entryComponents: [
      EventRegistrationComponent,
      LoadingComponent,
      ConfirmComponent,
      DeleteConfirmationComponent,
      DeleteConfirmationDialogComponent,
   ],
   providers: [ApiService, SingerEventsService, SingerEventsProxy],
})
export class SharedModule {}
