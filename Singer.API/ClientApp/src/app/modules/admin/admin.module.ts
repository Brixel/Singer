import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { OverviewComponent } from './components/careusers/overview/overview.component';
import { MaterialModule } from '../../material.module';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUserService } from '../core/services/care-users-api/careusers.service';
import { ApiService } from '../core/services/api.service';
import { AgegroupPipe } from '../core/services/agegroup.pipe';
import { CoreModule } from '../core/core.module';
import { CareUserDetailsComponent } from './components/careusers/care-user-details/care-user-details.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { KeysPipe } from '../core/services/keys.pipe';
import { LegalguardianOverviewComponent } from './components/legalguardians/legalguardian-overview/legalguardian-overview.component';
import { LegalguardianDetailsComponent } from './components/legalguardians/legalguardian-details/legalguardian-details.component';
import { LegalGuardianProxy } from '../core/services/legal-guardians-api/legalguardians.proxy';
import { LegalguardiansService } from '../core/services/legal-guardians-api/legalguardians.service';
import {
   MatMomentDateModule,
   MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from '@angular/material-moment-adapter';
import { AdminListComponent } from './components/admin-users/admin-list/admin-list.component';
import { AdminDetailsComponent } from './components/admin-users/admin-details/admin-details.component';
import { AdminUserProxy } from './services/adminuser.proxy';
import { AdminUserService } from './services/admin-user.service';

@NgModule({
   declarations: [
      OverviewComponent,
      CareUserDetailsComponent,
      LegalguardianOverviewComponent,
        LegalguardianDetailsComponent, AdminListComponent, AdminDetailsComponent
   ],
   imports: [
      CoreModule,
      CommonModule,
      AdminRoutingModule,
      MaterialModule,
      ReactiveFormsModule,
      FormsModule,
      MatMomentDateModule,
   ],
   entryComponents: [
       CareUserDetailsComponent,
       LegalguardianDetailsComponent,
      AdminDetailsComponent
   ],
   providers: [
      CareUserProxy,
      LegalGuardianProxy,
      CareUserService,
      AdminUserProxy,
      AdminUserService,
      LegalguardiansService,
      ApiService,
      AgegroupPipe,
      KeysPipe,
      { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
   ],
})
export class AdminModule {}
