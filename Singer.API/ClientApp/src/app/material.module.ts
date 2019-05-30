import { NgModule } from '@angular/core';

import {
   MatButtonModule,
   MatMenuModule,
   MatToolbarModule,
   MatIconModule,
   MatCardModule,
   MatListModule,
   MatTableModule,
} from '@angular/material';

@NgModule({
   imports: [
      MatButtonModule,
      MatMenuModule,
      MatToolbarModule,
      MatIconModule,
      MatCardModule,
      MatListModule,
      MatTableModule,
   ],
   exports: [
      MatButtonModule,
      MatMenuModule,
      MatToolbarModule,
      MatIconModule,
      MatCardModule,
      MatListModule,
      MatTableModule,
   ],
})
export class MaterialModule {}
