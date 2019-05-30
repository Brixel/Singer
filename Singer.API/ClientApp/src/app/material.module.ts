import { NgModule } from '@angular/core';

import {
   MatButtonModule,
   MatMenuModule,
   MatToolbarModule,
   MatIconModule,
   MatCardModule,
   MatListModule,
   MatTableModule,
   MatInputModule,
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
   imports: [
      MatButtonModule,
      MatInputModule,
      MatMenuModule,
      MatToolbarModule,
      MatIconModule,
      MatCardModule,
      MatListModule,
      MatTableModule
   ],
   exports: [
      MatButtonModule,
      MatInputModule,
      MatMenuModule,
      MatToolbarModule,
      MatIconModule,
      MatCardModule,
      MatListModule,
      MatTableModule
   ],
})
export class MaterialModule {}
