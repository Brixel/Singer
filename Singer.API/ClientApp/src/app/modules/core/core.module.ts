import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './Pipes/agegroup.pipe';
import { KeysPipe } from './Pipes/keys.pipe';
import { AgegroupChipsComponent } from './components/agegroup-chips/agegroup-chips.component';
import { MaterialModule } from 'src/app/material.module';
import { AgegroupToColorPipePipe } from './Pipes/agegroup-to-color-pipe.pipe';
import { DailybasisRegistrationsComponent } from './components/dailybasis-registrations/dailybasis-registrations.component';
import { SingleRegistrationComponent } from './components/single-registration/single-registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationStatusPipe } from './Pipes/registration-status.pipe';
import { SingereventCostPipe } from './Pipes/singerevent-cost.pipe';
import { AboutComponent } from './components/about/about.component';
import { RegistrationOverviewComponent } from './components/registration-overview/registration-overview.component';
import { RegistrationTypePipe } from './pipes/registration-type.pipe';

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
      RegistrationOverviewComponent,
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
   ],
})
export class CoreModule {}
