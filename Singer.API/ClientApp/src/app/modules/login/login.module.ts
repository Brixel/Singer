import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const ROUTES: Routes = [{ path: '', component: AuthComponent }];

@NgModule({
   declarations: [AuthComponent],
   imports: [
      CommonModule,
      RouterModule.forChild(ROUTES),
      MaterialModule,
      FormsModule,
      ReactiveFormsModule,
   ],
   providers: [],
})
export class LoginModule {}
