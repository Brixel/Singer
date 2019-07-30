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
import { EventsOverviewComponent } from './components/events/events-overview/events-overview.component';
import {
   MatTableModule,
   MatPaginatorModule,
   MatSortModule,
} from '@angular/material';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { LegalguardianOverviewComponent } from './components/legalguardians/legalguardian-overview/legalguardian-overview.component';
import { LegalguardianDetailsComponent } from './components/legalguardians/legalguardian-details/legalguardian-details.component';
import { LegalGuardianProxy } from '../core/services/legal-guardians-api/legalguardians.proxy';
import { LegalguardiansService } from '../core/services/legal-guardians-api/legalguardians.service';

@NgModule({
   declarations: [
      OverviewComponent,
      CareUserDetailsComponent,
      LegalguardianOverviewComponent,
      LegalguardianDetailsComponent,
      EventsOverviewComponent,
   ],
   imports: [
      CoreModule,
      CommonModule,
      AdminRoutingModule,
      MaterialModule,
      ReactiveFormsModule,
      FormsModule,
      MatMomentDateModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
   ],
   entryComponents: [CareUserDetailsComponent, LegalguardianDetailsComponent],
   providers: [
      CareUserProxy,
      LegalGuardianProxy,
      CareUserService,
      LegalguardiansService,
      ApiService,
      AgegroupPipe,
      KeysPipe,
   ],
})
export class AdminModule {}
