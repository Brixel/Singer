import { Component, ChangeDetectorRef } from '@angular/core';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { RegistrationsDatasource } from './registrations-datasource';
import { Registration } from '../../models/singerevent.model';
import { RegistrationDTO } from '../../DTOs/event-registration.dto';
import { RegistrationService } from '../../services/registration-api/registration-service';
import { MatDialog } from '@angular/material';
import { RegistrationSearchDTO } from '../../DTOs/registration.dto';
import { SortDirection } from '../../enums/sort-direction';

@Component({
   selector: 'app-registration-overview',
   templateUrl: './registration-overview.component.html',
   styleUrls: ['./registration-overview.component.css'],
})
export class RegistrationOverviewComponent extends GenericOverviewComponent<
   Registration,
   RegistrationDTO,
   null,
   null,
   RegistrationService,
   RegistrationsDatasource,
   RegistrationSearchDTO
> {
   constructor(dataService: RegistrationService, cd: ChangeDetectorRef) {
      const ds = new RegistrationsDatasource(dataService);
      super(cd, ds, 'id');
      this.displayedColumns.push('id', 'firstName', 'lastName');
      this.searchDTO = <RegistrationSearchDTO>{
         pageIndex: 0,
         pageSize: 15,
         sortColumn: 'id',
         sortDirection: SortDirection.Ascending,
         text: '',
      };
   }

   myOnInit() {
      throw new Error('Method not implemented.');
   }

   ngOnInit() {}
}
