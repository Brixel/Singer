import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgegroupPipe } from './services/agegroup.pipe';

@NgModule({
  declarations: [AgegroupPipe],
  imports: [
    CommonModule
  ],
  exports:[AgegroupPipe]
})
export class CoreModule { }
