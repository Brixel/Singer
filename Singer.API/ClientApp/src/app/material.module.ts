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
   MatPaginatorModule,
   MatSortModule,
   MatFormFieldModule,
   MatOptionModule,
   MatSelectModule,
   MatProgressSpinnerModule,
   MatDatepickerModule,
   MatDialogModule,
   MatDividerModule,
   MatGridListModule,
   MatChipsModule,
   MatTooltipModule,
   MatAutocompleteModule,
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
   MatMomentDateModule,
   MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from '@angular/material-moment-adapter';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

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
      MatTooltipModule,
      NgxMaterialTimepickerModule,
      MatAutocompleteModule,
   ],
   exports: [
      MatButtonModule,
      MatInputModule,
      MatMenuModule,
      MatGridListModule,
      MatChipsModule,
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
      MatTooltipModule,
      NgxMaterialTimepickerModule,
      MatAutocompleteModule,
   ],
   providers: [
      { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
   ],
})
export class MaterialModule {}
