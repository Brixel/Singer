import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { CareUserOverviewComponent } from './components/careusers/careuser-overview/care-user-overview.component';
import { MaterialModule } from '../../material.module';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUserService } from '../core/services/care-users-api/careusers.service';
import { ApiService } from '../core/services/api.service';
import { AgegroupPipe } from '../core/services/agegroup.pipe';
import { CoreModule } from '../core/core.module';
import { CareUserDetailsComponent } from './components/careusers/careuser-details/care-user-details.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { KeysPipe } from '../core/services/keys.pipe';
import {
   MatTableModule,
   MatPaginatorModule,
   MatSortModule,
   MAT_DATE_LOCALE,
} from '@angular/material';
import { LegalguardianOverviewComponent } from './components/legalguardians/legalguardian-overview/legalguardian-overview.component';
import { LegalguardianDetailsComponent } from './components/legalguardians/legalguardian-details/legalguardian-details.component';
import { LegalGuardianProxy } from '../core/services/legal-guardians-api/legalguardians.proxy';
import { LegalguardiansService } from '../core/services/legal-guardians-api/legalguardians.service';
import {
   MatMomentDateModule,
   MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from '@angular/material-moment-adapter';
import { SingerEventOverviewComponent } from './components/singerevents/singerevent-overview/singerevent-overview.component';
import { SingerEventsProxy } from '../core/services/singerevents-api/singerevents.proxy';
import { SingerEventsService } from '../core/services/singerevents-api/singerevents.service';
import { SingerEventDetailsComponent } from './components/singerevents/singerevent-details/singerevent-details.component';
import { AdminListComponent } from './components/admin-users/admin-list/admin-list.component';
import { AdminDetailsComponent } from './components/admin-users/admin-details/admin-details.component';
import { AdminUserProxy } from './services/adminuser.proxy';
import { AdminUserService } from './services/admin-user.service';
import { SingerEventLocationService } from '../core/services/singerevents-api/singerevent-location.service';
import { SingerEventLocationProxy } from '../core/services/singerevents-api/singerevent-location.proxy';
import { CareUserSearchComponent } from './components/shared/care-user-search/care-user-search.component';
import { EventRegistrationComponent } from '../shared/components/event-registration/event-registration.component';
import { SharedModule } from '../shared/shared.module';
import { AddFamilyWizardComponent } from './components/add-family-wizard/add-family-wizard.component';
import { SingerEventRegistrationsComponent } from './components/singerevents/singer-event-registrations/singer-event-registrations.component';
import { SingerEventAdminRegisterComponent } from './components/singerevents/singer-eventadmin-register/singer-eventadmin-register.component';
import { PendingRegistrationsComponent } from './components/pending-registrations/pending-registrations.component';

@NgModule({
   declarations: [
      CareUserOverviewComponent,
      CareUserDetailsComponent,
      LegalguardianOverviewComponent,
      LegalguardianDetailsComponent,
      SingerEventOverviewComponent,
      SingerEventDetailsComponent,
      AdminListComponent,
      AdminDetailsComponent,
      SingerEventRegistrationsComponent,
      CareUserSearchComponent,
      SingerEventAdminRegisterComponent,
      AddFamilyWizardComponent,
      PendingRegistrationsComponent,
   ],
   imports: [
      CoreModule,
      CommonModule,
      SharedModule,
      AdminRoutingModule,
      MaterialModule,
      ReactiveFormsModule,
      FormsModule,
      MatMomentDateModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
   ],
   entryComponents: [
      CareUserDetailsComponent,
      LegalguardianDetailsComponent,
      SingerEventDetailsComponent,
      SingerEventRegistrationsComponent,
      EventRegistrationComponent,
      AdminDetailsComponent,
      SingerEventAdminRegisterComponent,
   ],
   providers: [
      CareUserProxy,
      LegalGuardianProxy,
      SingerEventsProxy,
      SingerEventLocationProxy,
      CareUserService,
      AdminUserProxy,
      AdminUserService,
      LegalguardiansService,
      SingerEventsService,
      SingerEventLocationService,
      ApiService,
      AgegroupPipe,
      KeysPipe,
      {
         provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS,
         useValue: {
            useUtc: true,
         },
      },
      { provide: MAT_DATE_LOCALE, useValue: 'nl-BE' },
   ],
})
export class AdminModule {}
