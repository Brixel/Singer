import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './services/agegroup.pipe';
import { KeysPipe } from './services/keys.pipe';
import { AgegroupChipsComponent } from './components/agegroup-chips/agegroup-chips.component';
import { MaterialModule } from 'src/app/material.module';
import { AgegroupToColorPipePipe } from './services/agegroup-to-color-pipe.pipe';

@NgModule({
   declarations: [AgegroupPipe, KeysPipe, AgegroupChipsComponent, AgegroupToColorPipePipe],
   imports: [CommonModule, MaterialModule],
   exports: [AgegroupPipe, KeysPipe, AgegroupChipsComponent,AgegroupToColorPipePipe],
})
export class CoreModule {}
