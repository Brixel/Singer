import { Component, ChangeDetectorRef, ViewChild } from '@angular/core';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { RegistrationOverviewDatasource } from './registration-overview-datasource';
import { RegistrationOverview, Registration } from '../../../core/models/registration.model';
import { RegistrationOverviewDTO } from '../../../core/DTOs/registration.dto';
import { RegistrationService } from '../../../core/services/registration-api/registration-service';
import { RegistrationSearchDTO } from '../../../core/DTOs/registration.dto';
import { SortDirection } from '../../../core/enums/sort-direction';
import { RegistrationStatus } from '../../../core/models/enum';
import * as moment from 'moment';
import { RegistrationType } from '../../../core/enums/registration-type';
import { MatSelect, MatSnackBar } from '@angular/material';
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
   RegistrationService,
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
   private _snackBar: MatSnackBar;
   private _registrationService: RegistrationService;

   constructor(
      dataService: RegistrationService,
      cd: ChangeDetectorRef,
      authService: AuthService,
      loadingService: LoadingService,
      snackBar: MatSnackBar
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
      this._snackBar = snackBar;
      this._registrationService = dataService;
   }

   ngOnInit() {
      this.dataSource.loading$.subscribe(val => {
         if (val) this._loadingService.show();
         if (!val) this._loadingService.hide();
      });
      this.dataSource.error$.subscribe(err => {
         this._loadingService.hide();
         if (err !== null && err !== undefined) {
            this._snackBar.open(`⚠ Er heeft zich een fout voorgedaan: ${err}`, 'OK');
         }
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

   changeRegistration(registrationStatus: RegistrationStatus, registration: RegistrationOverview) {
      console.log(registration);
      switch (registrationStatus) {
         case RegistrationStatus.Accepted:
            this._registrationService.acceptRegistration(registration.id).subscribe(() =>
               this._snackBar.open(
                  `Inschrijving van ${registration.careUserFirstName} ${registration.careUserLastName} is goedgekeurd voor dit evenement`,
                  'OK',
                  {
                     duration: 2000,
                  }
               )
            );
            break;
         case RegistrationStatus.Rejected:
            this._registrationService.rejectRegistration(registration.id).subscribe(() =>
               this._snackBar.open(
                  `Inschrijving van ${registration.careUserFirstName} ${registration.careUserLastName} is afgekeurd voor dit evenement`,
                  'OK',
                  {
                     duration: 2000,
                  }
               )
            );
            break;
         default:
         case RegistrationStatus.Pending:
            this._snackBar.open("Het is niet mogelijk om de inschrijving naar 'In afwachting' te zetten", 'OK', {
               duration: 2000,
            });
            break;
      }
   }
}
