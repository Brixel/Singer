import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './Pipes/agegroup.pipe';
import { KeysPipe } from './Pipes/keys.pipe';
import { AgegroupChipsComponent } from './components/agegroup-chips/agegroup-chips.component';
import { MaterialModule } from 'src/app/material.module';
import { AgegroupToColorPipePipe } from './Pipes/agegroup-to-color-pipe.pipe';
import { DailybasisRegistrationsComponent } from './components/dailybasis-registrations/dailybasis-registrations.component';
import { SingleRegistrationComponent } from './components/single-registration/single-registration.component';
import { DeleteConfirmationComponent } from './components/delete-confirmation/delete-confirmation.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SingereventCostPipe } from './Pipes/singerevent-cost.pipe';
import { SingerEventCostFilterParametersPipe } from './Pipes/singerevent-cost-filterparameters.pipe';

@NgModule({
   declarations: [
      AgegroupPipe,
      KeysPipe,
      AgegroupChipsComponent,
      AgegroupToColorPipePipe,
      DailybasisRegistrationsComponent,
      SingleRegistrationComponent,
      DeleteConfirmationComponent,
      SingereventCostPipe,
      SingerEventCostFilterParametersPipe,
   ],
   imports: [CommonModule, MaterialModule, ReactiveFormsModule],
   exports: [
      AgegroupPipe,
      KeysPipe,
      AgegroupChipsComponent,
      AgegroupToColorPipePipe,
      SingleRegistrationComponent,
      DailybasisRegistrationsComponent,
      DeleteConfirmationComponent,
      SingereventCostPipe,
      SingerEventCostFilterParametersPipe,
   ],
})
export class CoreModule {}
