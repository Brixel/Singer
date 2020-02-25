import { Component, ChangeDetectorRef, ViewChild } from '@angular/core';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { RegistrationOverviewDatasource } from './registration-overview-datasource';
import { RegistrationOverview } from '../../../core/models/registration.model';
import { RegistrationOverviewDTO } from '../../../core/DTOs/registration.dto';
import { RegistrationOverviewService } from '../../../core/services/registration-api/registration-overview-service';
import { RegistrationSearchDTO } from '../../../core/DTOs/registration.dto';
import { SortDirection } from '../../../core/enums/sort-direction';
import { RegistrationStatus } from '../../../core/models/enum';
import * as moment from 'moment';
import { RegistrationType } from '../../../core/enums/registration-type';
import { MatSelect } from '@angular/material';
import { AuthService } from '../../../core/services/auth.service';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { LoadingService } from 'src/app/modules/core/services/loading.service';

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
   @ViewChild('searchType') searchType: MatSelect;
   @ViewChild('searchStatus') searchStatus: MatSelect;
   registrationTypes: any;
   registrationStatus: any;
   authService: AuthService;
   private _loadingService: LoadingService;

   constructor(
      dataService: RegistrationOverviewService,
      cd: ChangeDetectorRef,
      authService: AuthService,
      loadingService: LoadingService
   ) {
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
         careUserIds: [],
      };
      this.RegistrationStatus = RegistrationStatus;
      this.registrationTypes = RegistrationType;
      this.registrationStatus = RegistrationStatus;
      this.authService = authService;
      this._loadingService = loadingService;
   }

   ngOnInit() {
      this.dataSource.loadingSubject$.subscribe(val => {
         if (val) this._loadingService.show();
         if (!val) this._loadingService.hide();
      });
   }

   loadData() {
      let dateFrom = moment.utc(this.searchDateFrom.nativeElement.value, 'D/M/YYYY');
      if (dateFrom.isValid()) {
         this.searchDTO.dateFrom = dateFrom.toDate();
      }
      let dateTo = moment.utc(this.searchDateTo.nativeElement.value, 'D/M/YYYY');
      if (dateTo.isValid()) {
         this.searchDTO.dateTo = dateTo.toDate();
      }
      if (this.searchType.value && this.searchType.value.length > 0) {
         this.searchDTO.registrationType = this.searchType.value.reduce((a, b) => a + b, 0);
      } else {
         this.searchDTO.registrationType = null;
      }
      if (this.searchStatus.value && this.searchStatus.value.length > 0) {
         this.searchDTO.registrationStatus = this.searchStatus.value.reduce((a, b) => a + b, 0);
      } else {
         this.searchDTO.registrationStatus = null;
      }
      super.loadData();
   }

   onCareUserFilterChange(careUsers: CareUser[]) {
      this.searchDTO.careUserIds = careUsers.map(x => x.userId);
      this.loadData();
   }
}
