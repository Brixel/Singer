import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './pipes/agegroup.pipe';
import { KeysPipe } from './pipes/keys.pipe';
import { AgegroupChipsComponent } from './components/agegroup-chips/agegroup-chips.component';
import { MaterialModule } from 'src/app/material.module';
import { AgegroupToColorPipePipe } from './pipes/agegroup-to-color-pipe.pipe';
import { DailybasisRegistrationsComponent } from './components/dailybasis-registrations/dailybasis-registrations.component';
import { SingleRegistrationComponent } from './components/single-registration/single-registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationStatusPipe } from './pipes/registration-status.pipe';
import { SingereventCostPipe } from './pipes/singerevent-cost.pipe';
import { AboutComponent } from './components/about/about.component';
import { RegistrationTypePipe } from './pipes/registration-type.pipe';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MAT_DATE_LOCALE } from '@angular/material/core';

@NgModule({
   declarations: [
      AgegroupPipe,
      KeysPipe,
      AgegroupChipsComponent,
      AgegroupToColorPipePipe,
      DailybasisRegistrationsComponent,
      SingleRegistrationComponent,
      RegistrationStatusPipe,
      SingereventCostPipe,
      AboutComponent,
      RegistrationTypePipe,
   ],
   imports: [CommonModule, MaterialModule, ReactiveFormsModule],
   exports: [
      RegistrationStatusPipe,
      AgegroupPipe,
      KeysPipe,
      AgegroupChipsComponent,
      AgegroupToColorPipePipe,
      SingleRegistrationComponent,
      DailybasisRegistrationsComponent,
      SingereventCostPipe,
      AboutComponent,
      RegistrationTypePipe,
   ],
   providers:[,
      {
         provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS,
         useValue: {
            useUtc: true,
         },
      },
      { provide: MAT_DATE_LOCALE, useValue: 'nl-BE' },]
})
export class CoreModule {}
