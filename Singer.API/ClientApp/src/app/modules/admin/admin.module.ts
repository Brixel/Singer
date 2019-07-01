import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { OverviewComponent } from './components/careusers/overview/overview.component';
import { MaterialModule } from '../../material.module';
import { CareUserProxy } from '../core/services/care-users-api/careuser.proxy';
import { CareUserService as CareUserService } from '../core/services/care-users-api/careusers.service';
import { ApiService } from '../core/services/api.service';
import { AgegroupPipe } from '../core/services/agegroup.pipe';
import { CoreModule } from '../core/core.module';
import { CareUserDetailsComponent } from './components/careusers/care-user-details/care-user-details.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { KeysPipe } from '../core/services/keys.pipe';
import { LegalguardianOverviewComponent } from './components/legalguardians/legalguardian-overview/legalguardian-overview.component';
import { LegalguardianDetailsComponent } from './components/legalguardians/legalguardian-details/legalguardian-details.component';

@NgModule({
   declarations: [OverviewComponent, CareUserDetailsComponent, LegalguardianOverviewComponent, LegalguardianDetailsComponent],
   imports: [
      CoreModule,
      CommonModule,
      AdminRoutingModule,
      MaterialModule,
      ReactiveFormsModule,
      FormsModule,
   ],
   entryComponents: [
      CareUserDetailsComponent, LegalguardianDetailsComponent
   ],
   providers: [
      CareUserProxy,
      CareUserService,
      ApiService,
      AgegroupPipe,
      KeysPipe,
   ]
})
export class AdminModule { }
