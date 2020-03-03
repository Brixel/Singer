import { Component, ChangeDetectorRef } from '@angular/core';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { SingerLocation } from 'src/app/modules/core/models/singer-location.model';
import {
   SingerLocationDTO,
   UpdateSingerLocationDTO,
   CreateSingerLocationDTO,
   SingerLocationSearchDTO,
} from 'src/app/modules/core/DTOs/singer-event-location.dto';
import { SingerLocationService } from 'src/app/modules/core/services/singer-location-api/singer-location.service';
import { SingerLocationDataSource } from './registration-overview-datasource';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { MatSnackBar, MatDialog } from '@angular/material';
import { SortDirection } from 'src/app/modules/core/enums/sort-direction';
import { LocationDetailsComponent } from '../location-details/location-details.component';
import { ConfirmComponent, ConfirmRequest } from 'src/app/modules/shared/components/confirm/confirm.component';

@Component({
   selector: 'app-location-overview',
   templateUrl: './location-overview.component.html',
   styleUrls: ['./location-overview.component.css'],
})
export class LocationOverviewComponent extends GenericOverviewComponent<
   SingerLocation,
   SingerLocationDTO,
   UpdateSingerLocationDTO,
   CreateSingerLocationDTO,
   SingerLocationService,
   SingerLocationDataSource,
   SingerLocationSearchDTO
> {
   private _loadingService: LoadingService;
   private _snackBar: MatSnackBar;
   private _dialog: MatDialog;
   private _dataService: SingerLocationService;
   constructor(
      dataService: SingerLocationService,
      cd: ChangeDetectorRef,
      loadingService: LoadingService,
      snackBar: MatSnackBar,
      dialog: MatDialog
   ) {
      const ds = new SingerLocationDataSource(dataService);
      super(cd, ds, 'name', SortDirection.Ascending);
      this.displayedColumns.push('name', 'address', 'actions');
      this.searchDTO = <SingerLocationSearchDTO>{
         pageIndex: 0,
         pageSize: 15,
         sortColumn: 'name',
         sortDirection: SortDirection.Descending,
         text: '',
      };
      this._loadingService = loadingService;
      this._snackBar = snackBar;
      this._dialog = dialog;
      this._dataService = dataService;
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

   add(): void {
      const dialogRef = this._dialog.open(LocationDetailsComponent, {
         data: { modelInstance: null, isAdding: true },
      });
      dialogRef.componentInstance.submitEvent.subscribe((result: SingerLocation) => {
         this._dataService.create(result).subscribe(
            (res: SingerLocation) => {
               this.loadData();
               this._snackBar.open(`Locatie ${res.name} succesvol toegevoegd.`, 'OK', { duration: 2500 });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   edit(location: SingerLocation): void {
      const dialogRef = this._dialog.open(LocationDetailsComponent, {
         data: { modelInstance: location, isAdding: false },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: SingerLocation) => {
         this._dataService.update(result).subscribe(
            () => {
               this.loadData();
               this._snackBar.open(`Locatie ${result.name} succesvol opgeslagen.`, 'OK', { duration: 2500 });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   delete(location: SingerLocation): void {
      const dialogRef = this._dialog.open(ConfirmComponent, {
         data: <ConfirmRequest>{
            confirmMessage: 'Ben je zeker dat je deze locatie wil verwijderen uit het systeem?',
         },
      });

      dialogRef.afterClosed().subscribe((result: Boolean) => {
         if (result) {
            this._dataService.delete(location).subscribe(
               () => {
                  this._snackBar.open(`Locatie ${location.name} succesvol verwijderd.`, 'OK', { duration: 2500 });
                  this.loadData();
               },
               err => {
                  this.handleApiError(err);
               }
            );
         }
      });
   }

   handleApiError(err: any) {
      if (typeof err === 'string') {
         this._snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         let messages = [];
         for (var k in err) {
            messages.push(err[k]);
         }
         this._snackBar.open(`⚠ Er zijn fouten opgetreden bij het uitvoeren:\n${messages.join('\n')}`, 'OK', {
            panelClass: 'multi-line-snackbar',
         });
      }
   }
}
