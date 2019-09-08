import { NgModule } from '@angular/core';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

import {
   MatButtonModule,
   MatMenuModule,
   MatToolbarModule,
   MatIconModule,
   MatCardModule,
   MatListModule,
   MatTableModule,
   MatInputModule,
   MatPaginatorModule,
   MatSortModule,
   MatFormFieldModule,
   MatOptionModule,
   MatSelectModule,
   MatProgressSpinnerModule,
   MatDatepickerModule,
   MatDialogModule,
   MatDividerModule,
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
   MatMomentDateModule,
   MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from '@angular/material-moment-adapter';

@NgModule({
   imports: [
      MatButtonModule,
      MatInputModule,
      MatMenuModule,
      MatToolbarModule,
      MatIconModule,
      MatCardModule,
      MatListModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatFormFieldModule,
      MatOptionModule,
      MatSelectModule,
      MatProgressSpinnerModule,
      MatDatepickerModule,
      MatDialogModule,
      MatDividerModule,
      MatMomentDateModule,
      NgxMaterialTimepickerModule,
   ],
   exports: [
      MatButtonModule,
      MatInputModule,
      MatMenuModule,
      MatToolbarModule,
      MatIconModule,
      MatCardModule,
      MatListModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatFormFieldModule,
      MatOptionModule,
      MatSelectModule,
      MatProgressSpinnerModule,
      MatDatepickerModule,
      MatDialogModule,
      MatDividerModule,
      MatMomentDateModule,
      NgxMaterialTimepickerModule,
   ],
   providers: [
      { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
   ],
})
export class MaterialModule {}
