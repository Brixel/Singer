import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './services/agegroup.pipe';
import { KeysPipe } from './services/keys.pipe';
import { AgegroupChipsComponent } from './components/agegroup-chips/agegroup-chips.component';
import { MaterialModule } from 'src/app/material.module';
import { AgegroupToColorPipePipe } from './services/agegroup-to-color-pipe.pipe';
import { MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material';
import { DailybasisRegistrationsComponent } from './components/dailybasis-registrations/dailybasis-registrations.component';
import { SingleRegistrationComponent } from './components/single-registration/single-registration.component';
import { DeleteConfirmationComponent } from './components/delete-confirmation/delete-confirmation.component';
import { ConfirmComponent } from './components/confirm/confirm.component';
import { ReactiveFormsModule } from '@angular/forms';

export const MY_FORMATS = {
   parse: {
     dateInput: 'D-MM-YYYY',
   },
   display: {
     dateInput: 'D-MM-YYYY',
     monthYearLabel: 'MMM YYYY'
   },
 };

@NgModule({
   declarations: [
      AgegroupPipe,
      KeysPipe,
      AgegroupChipsComponent,
      AgegroupToColorPipePipe,
      DailybasisRegistrationsComponent,
      SingleRegistrationComponent,
      DeleteConfirmationComponent,
      ConfirmComponent,
   ],
   imports: [CommonModule, MaterialModule, ReactiveFormsModule],
   providers: [
      {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS}, {provide: MAT_DATE_LOCALE, useValue: 'nl-BE'}],
   exports: [
      AgegroupPipe,
      KeysPipe,
      AgegroupChipsComponent,
      AgegroupToColorPipePipe,
      SingleRegistrationComponent,
      DailybasisRegistrationsComponent,
      DeleteConfirmationComponent,
   ],
})
export class CoreModule {}
