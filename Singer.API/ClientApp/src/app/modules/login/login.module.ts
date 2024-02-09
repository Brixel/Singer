import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthComponent } from './components/auth/auth.component';

const ROUTES: Routes = [
   {
      path: '',
      component: AuthComponent,
   },
];

@NgModule({
   declarations: [AuthComponent],
   imports: [CommonModule, RouterModule.forChild(ROUTES), MaterialModule, FormsModule, ReactiveFormsModule],
   providers: [],
})
export class LoginModule {}
