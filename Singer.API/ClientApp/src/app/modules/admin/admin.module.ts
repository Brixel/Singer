import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { CareUserOverviewComponent } from './components/careusers/careuser-overview/care-user-overview.component';
import { MaterialModule } from '../../material.module';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUserService } from '../core/services/care-users-api/careusers.service';
import { ApiService } from '../core/services/api.service';
import { AgegroupPipe } from '../core/pipes/agegroup.pipe';
import { CoreModule } from '../core/core.module';
import { CareUserDetailsComponent } from './components/careusers/careuser-details/care-user-details.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { KeysPipe } from '../core/pipes/keys.pipe';
import { MatTableModule, MatPaginatorModule, MatSortModule, MAT_DATE_LOCALE } from '@angular/material';
import { LegalguardianOverviewComponent } from './components/legalguardians/legalguardian-overview/legalguardian-overview.component';
import { LegalguardianDetailsComponent } from './components/legalguardians/legalguardian-details/legalguardian-details.component';
import { LegalGuardianProxy } from '../core/services/legal-guardians-api/legalguardians.proxy';
import { LegalguardiansService } from '../core/services/legal-guardians-api/legalguardians.service';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { SingerEventOverviewComponent } from './components/singerevents/singerevent-overview/singerevent-overview.component';
import { SingerEventsProxy } from '../core/services/singerevents-api/singerevents.proxy';
import { SingerEventsService } from '../core/services/singerevents-api/singerevents.service';
import { SingerEventDetailsComponent } from './components/singerevents/singerevent-details/singerevent-details.component';
import { AdminOverviewComponent } from './components/admin-users/admin-overview/admin-overview.component';
import { AdminDetailsComponent } from './components/admin-users/admin-details/admin-details.component';
import { AdminUserProxy } from './services/adminuser.proxy';
import { AdminUserService } from './services/admin-user.service';
import { SingerLocationService } from '../core/services/singer-location-api/singer-location.service';
import { RegistrationComponent } from '../shared/components/event-registration/event-registration.component';
import { SharedModule } from '../shared/shared.module';
import { AddFamilyWizardComponent } from './components/add-family-wizard/add-family-wizard.component';
import { SingerRegistrationsComponent } from './components/singerevents/event-registrations/event-registrations.component';
import { SingerEventAdminRegisterComponent } from './components/singerevents/singer-eventadmin-register/singer-eventadmin-register.component';
import { PendingRegistrationsComponent } from './components/pending-registrations/pending-registrations.component';
import { PendingActionsComponent } from './components/pending-actions/pending-actions.component';
import { LocationOverviewComponent } from './components/location-overview/location-overview.component';
import { LocationDetailsComponent } from './components/location-details/location-details.component';
import { SingerLocationProxy } from '../core/services/singer-location-api/singer-location.proxy';

@NgModule({
   declarations: [
      CareUserOverviewComponent,
      CareUserDetailsComponent,
      LegalguardianOverviewComponent,
      LegalguardianDetailsComponent,
      SingerEventOverviewComponent,
      SingerEventDetailsComponent,
      AdminOverviewComponent,
      AdminDetailsComponent,
      SingerRegistrationsComponent,
      SingerEventAdminRegisterComponent,
      AddFamilyWizardComponent,
      PendingRegistrationsComponent,
      PendingActionsComponent,
      LocationOverviewComponent,
      LocationDetailsComponent,
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
      SingerRegistrationsComponent,
      RegistrationComponent,
      AdminDetailsComponent,
      SingerEventAdminRegisterComponent,
      LocationDetailsComponent,
   ],
   providers: [
      CareUserProxy,
      LegalGuardianProxy,
      SingerEventsProxy,
      SingerLocationProxy,
      CareUserService,
      AdminUserProxy,
      AdminUserService,
      LegalguardiansService,
      SingerEventsService,
      SingerLocationService,
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
