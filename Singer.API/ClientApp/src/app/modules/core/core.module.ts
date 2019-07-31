import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './services/agegroup.pipe';
import { KeysPipe } from './services/keys.pipe';
import { AgeGroupArrayPipe } from './pipes/age-group-array.pipe';

@NgModule({
  declarations: [AgegroupPipe, KeysPipe, AgeGroupArrayPipe],
  imports: [
    CommonModule
  ],
  exports: [AgegroupPipe, KeysPipe]
})
export class CoreModule { }
