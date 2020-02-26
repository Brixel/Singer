import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './pipes2/agegroup.pipe';
import { KeysPipe } from './pipes2/keys.pipe';
import { AgegroupChipsComponent } from './components/agegroup-chips/agegroup-chips.component';
import { MaterialModule } from 'src/app/material.module';
import { AgegroupToColorPipePipe } from './pipes2/agegroup-to-color-pipe.pipe';
import { DailybasisRegistrationsComponent } from './components/dailybasis-registrations/dailybasis-registrations.component';
import { SingleRegistrationComponent } from './components/single-registration/single-registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationStatusPipe } from './pipes2/registration-status.pipe';
import { SingereventCostPipe } from './pipes2/singerevent-cost.pipe';
import { AboutComponent } from './components/about/about.component';
import { RegistrationTypePipe } from './pipes2/registration-type.pipe';

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
})
export class CoreModule {}
