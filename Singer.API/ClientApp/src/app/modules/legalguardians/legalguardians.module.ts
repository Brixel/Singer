import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NightCareComponent } from './night-care/night-care.component';
import { LegalguardiansRoutingModule } from './legalguardians-routing.module';
import {
   CalendarModule,
   CalendarDateFormatter,
   CalendarMomentDateFormatter,
   DateAdapter,
   MOMENT,
} from 'angular-calendar';
import { MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material';
import { adapterFactory } from 'angular-calendar/date-adapters/moment';
import * as moment from 'moment';
import { MY_FORMATS } from 'src/app/app.module';
import { MaterialModule } from 'src/app/material.module';
export function momentAdapterFactory() {
   return adapterFactory(moment);
}
@NgModule({
   declarations: [NightCareComponent],
   imports: [
      CommonModule,
      MaterialModule,
      LegalguardiansRoutingModule,
      CalendarModule.forRoot(
         {
            provide: DateAdapter,
            useFactory: momentAdapterFactory,
         },
         {
            dateFormatter: {
               provide: CalendarDateFormatter,
               useClass: CalendarMomentDateFormatter,
            },
         }
      ),
   ],
   providers: [
      { provide: MAT_DATE_LOCALE, useValue: 'nl-BE' },
      { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
      { provide: MOMENT, useValue: moment },
   ],
})
export class LegalguardiansModule {}
