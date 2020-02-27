import { NgModule } from '@angular/core';
import { OwlDateTimeModule, OwlNativeDateTimeModule, OWL_DATE_TIME_LOCALE, OwlDateTimeIntl } from 'ng-pick-datetime';
import { CommonModule } from '@angular/common';
import { LegalguardiansRoutingModule } from './legalguardians-routing.module';
import { MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material';
import { MY_FORMATS } from 'src/app/app.module';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from 'src/app/material.module';
import { RegisterCareWizardComponent } from './components/register-care-wizard/register-care-wizard.component';
import { SearchCareUserDialogComponent } from './components/search-care-user-dialog/search-care-user-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DutchOwlDateTimeIntl } from './dutchowldatetime';
import { CareRegistrationService } from './services/care-registration.service';
import { CareRegistrationProxy } from './services/care-registration.proxy';
import { ApiService } from '../core/services/api.service';
import { RegistrationTypePipe } from '../core/pipes/registration-type.pipe';

@NgModule({
   declarations: [RegisterCareWizardComponent, SearchCareUserDialogComponent],
   imports: [
      CommonModule,
      SharedModule,
      MaterialModule,
      LegalguardiansRoutingModule,
      ReactiveFormsModule,
      OwlDateTimeModule,
      OwlNativeDateTimeModule,
   ],
   entryComponents: [SearchCareUserDialogComponent],
   providers: [
      ApiService,
      CareRegistrationService,
      CareRegistrationProxy,
      { provide: MAT_DATE_LOCALE, useValue: 'nl-BE' },
      { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
      { provide: OWL_DATE_TIME_LOCALE, useValue: 'nl-BE' },
      { provide: OwlDateTimeIntl, useClass: DutchOwlDateTimeIntl },
   ],
})
export class LegalguardiansModule {}
