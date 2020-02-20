import { Component, ChangeDetectorRef, ViewChild } from '@angular/core';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { RegistrationOverviewDatasource } from './registration-overview-datasource';
import { RegistrationOverview } from '../../models/registration.model';
import { RegistrationOverviewDTO } from '../../DTOs/registration.dto';
import { RegistrationOverviewService } from '../../services/registration-api/registration-overview-service';
import { RegistrationSearchDTO } from '../../DTOs/registration.dto';
import { SortDirection } from '../../enums/sort-direction';
import { RegistrationStatus } from '../../models/enum';
import * as moment from 'moment';

@Component({
   selector: 'app-registration-overview',
   templateUrl: './registration-overview.component.html',
   styleUrls: ['./registration-overview.component.css'],
})
export class RegistrationOverviewComponent extends GenericOverviewComponent<
   RegistrationOverview,
   RegistrationOverviewDTO,
   null,
   null,
   RegistrationOverviewService,
   RegistrationOverviewDatasource,
   RegistrationSearchDTO
> {
   RegistrationStatus: any;
   @ViewChild('searchDateFrom') searchDateFrom: {
      nativeElement: { value: moment.MomentInput };
   };
   @ViewChild('searchDateTo') searchDateTo: {
      nativeElement: { value: moment.MomentInput };
   };
   constructor(dataService: RegistrationOverviewService, cd: ChangeDetectorRef) {
      const ds = new RegistrationOverviewDatasource(dataService);
      super(cd, ds, 'startDateTime', SortDirection.Descending);
      this.displayedColumns.push(
         'eventTitle',
         'startDateTime',
         'registrationType',
         'careUserFirstName',
         'careUserLastName',
         'registrationStatus'
      );
      this.searchDTO = <RegistrationSearchDTO>{
         pageIndex: 0,
         pageSize: 15,
         sortColumn: 'startDateTime',
         sortDirection: SortDirection.Descending,
         text: '',
      };
      this.RegistrationStatus = RegistrationStatus;
   }

   ngOnInit() {}
}
