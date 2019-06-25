import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './services/agegroup.pipe';
import { KeysPipe } from './services/keys.pipe';

@NgModule({
  declarations: [AgegroupPipe, KeysPipe],
  imports: [
    CommonModule
  ],
  exports: [AgegroupPipe, KeysPipe]
})
export class CoreModule { }
