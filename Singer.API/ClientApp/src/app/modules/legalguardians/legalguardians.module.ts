import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LegalguardiansRoutingModule } from './legalguardians-routing.module';
import { MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material';
import { MY_FORMATS } from 'src/app/app.module';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from 'src/app/material.module';
import { RegisterCareWizardComponent } from './shared/register-care-wizard/register-care-wizard.component';
import { SearchCareUserDialogComponent } from './shared/search-care-user-dialog/search-care-user-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
   declarations: [RegisterCareWizardComponent, SearchCareUserDialogComponent],
   imports: [CommonModule, SharedModule, MaterialModule, LegalguardiansRoutingModule, ReactiveFormsModule],
   entryComponents: [SearchCareUserDialogComponent],
   providers: [
      { provide: MAT_DATE_LOCALE, useValue: 'nl-BE' },
      { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
   ],
})
export class LegalguardiansModule {}
