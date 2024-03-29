import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationComponent } from './components/event-registration/event-registration.component';
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
import { CareUserSearchComponent } from './components/care-user-search/care-user-search.component';
import { AccountInfoCareusersListComponent } from './components/account-info/account-info-careusers-list/account-info-careusers-list.component';
import { AccountInfoSummaryComponent } from './components/account-info/account-info-summary/account-info-summary.component';
import { AccountInfoPageComponent } from './components/account-info/account-info-base/account-info-page.component';
import { CareUserMultiAutocompleteComponent } from './components/care-user-multi-autocomplete/care-user-multi-autocomplete.component';
import { RegistrationStatusToggleComponent } from './components/registration-status-toggle/registration-status-toggle.component';
import { RegistrationStatusComponent } from './components/registration-status/registration-status.component';

@NgModule({
    declarations: [
        RegistrationComponent,
        UserCardComponent,
        DetailedUserCardComponent,
        LoadingComponent,
        EventTimelineComponent,
        ConfirmComponent,
        DeleteConfirmationComponent,
        DeleteConfirmationDialogComponent,
        CareUserSearchComponent,
        AccountInfoSummaryComponent,
        AccountInfoPageComponent,
        AccountInfoCareusersListComponent,
        CareUserMultiAutocompleteComponent,
        RegistrationStatusToggleComponent,
        RegistrationStatusComponent,
    ],
    imports: [CommonModule, MaterialModule, CoreModule, ReactiveFormsModule],
    exports: [
        RegistrationComponent,
        UserCardComponent,
        DetailedUserCardComponent,
        LoadingComponent,
        EventTimelineComponent,
        ConfirmComponent,
        DeleteConfirmationComponent,
        DeleteConfirmationDialogComponent,
        CareUserSearchComponent,
        AccountInfoSummaryComponent,
        AccountInfoPageComponent,
        AccountInfoCareusersListComponent,
        CareUserMultiAutocompleteComponent,
        RegistrationStatusToggleComponent,
        RegistrationStatusComponent,
    ],
    providers: [ApiService, SingerEventsService, SingerEventsProxy]
})
export class SharedModule {}
