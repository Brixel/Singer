import { AfterViewInit, Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { LegalguardianOverviewDataSource } from './legalguardian-overview-datasource';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { LegalguardianDetailsComponent } from '../legalguardian-details/legalguardian-details.component';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { ConfirmComponent, ConfirmRequest } from 'src/app/modules/shared/components/confirm/confirm.component';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-legalguardian-overview',
   templateUrl: './legalguardian-overview.component.html',
   styleUrls: ['./legalguardian-overview.component.css'],
})
export class LegalguardianOverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort, { static: true }) sort: MatSort;
   @ViewChild('filterInput', { static: true }) filterInput: ElementRef;

   // Filter
   filter: string = '';

   readonly maxFilterLength = 2048;

   formControlGroup: UntypedFormGroup = new UntypedFormGroup({
      // Form controls
      filterFieldControl: new UntypedFormControl(this.filter, [Validators.maxLength(this.maxFilterLength)]),
   });

   // Datatable
   dataSource: LegalguardianOverviewDataSource;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = ['firstName', 'lastName', 'email', 'address', 'actions'];

   // Paginator
   pageSize = 15;
   pageIndex = 0;

   constructor(
      public dialog: MatDialog,
      private _legalguardiansService: LegalguardiansService,
      private _snackBar: MatSnackBar,
      private _loadingService: LoadingService,
      private authService: AuthService
   ) {}

   ngOnInit() {
      this.dataSource = new LegalguardianOverviewDataSource(this._legalguardiansService);
      this.sort.active = 'firstName';
      this.sort.direction = 'asc';
      this.loadLegalGuardians();
   }

   ngAfterViewInit() {
      fromEvent(this.filterInput.nativeElement, 'keyup')
         .pipe(
            debounceTime(400),
            distinctUntilChanged(),
            tap(() => {
               this.paginator.pageIndex = 0;
               this.loadLegalGuardians();
            })
         )
         .subscribe();
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
      merge(this.sort.sortChange, this.paginator.page)
         .pipe(
            tap(() => {
               this.pageIndex = this.paginator.pageIndex;
               this.pageSize = this.paginator.pageSize;
               this.loadLegalGuardians();
            })
         )
         .subscribe();
      this.dataSource.loading$.subscribe(res => {
         if (res) {
            this._loadingService.show();
         } else {
            this._loadingService.hide();
         }
      });
   }

   private loadLegalGuardians() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadLegalGuardians(sortDirection, sortColumn, this.pageIndex, this.pageSize, this.filter);
   }

   addLegalGuardian(): void {
      const dialogRef = this.dialog.open(LegalguardianDetailsComponent, {
         data: { legalGuardianInstance: null, displayLinkedUserFields: false },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: LegalGuardian) => {
         // Add the legal guardian
         this._legalguardiansService.createLegalGuardian(result).subscribe(
            res => {
               // Reload LegalGuardians
               this.loadLegalGuardians();

               this._snackBar.open(`${result.firstName} ${result.lastName} werd toegevoegd als voogd.`, 'OK', {
                  duration: 2000,
               });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   selectRow(row: LegalGuardian): void {
      //Dereference row to avoid updating row in overview when API might refuse the update
      const deRefRow = { ...row };
      const dialogRef = this.dialog.open(LegalguardianDetailsComponent, {
         data: { legalGuardianInstance: deRefRow, displayLinkedUserFields: true },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: LegalGuardian) => {
         //Update the legal guardian
         this._legalguardiansService.updateLegalGuardian(result).subscribe(
            res => {
               // Reload LegalGuardians
               this.loadLegalGuardians();
               this._snackBar.open(`${result.firstName} ${result.lastName} werd aangepast.`, 'OK', { duration: 2000 });
            },
            err => {
               this.handleApiError(err);
            }
         );
      });

      dialogRef.componentInstance.deleteEvent.subscribe((result: LegalGuardian) => {
         this._legalguardiansService.deleteLegalGuardian(result.id).subscribe(
            res => {
               // Reload LegalGuardians
               this.loadLegalGuardians();
               this._snackBar.open(`${result.firstName} ${result.lastName} werd verwijderd.`, 'OK', { duration: 2000 });
            },
            err => {
               this.handleApiError(err);
               this.loadLegalGuardians();
            }
         );
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
         this._snackBar.open(`⚠ Er zijn fouten opgetreden bij het opslagen:\n${messages.join('\n')}`, 'OK', {
            panelClass: 'multi-line-snackbar',
         });
      }
   }

   changePassword($event: MouseEvent, row: LegalGuardian) {
      $event.stopPropagation();
      const dialogRef = this.dialog.open(ConfirmComponent, {
         data: <ConfirmRequest>{
            confirmMessage: `Wilt u het wachtwoord van ${row.firstName} ${row.lastName} wijzigen?`,
         },
      });
      dialogRef.afterClosed().subscribe((isConfirmed: boolean) => {
         if (isConfirmed) {
            this.authService.requestPasswordReset(row.userId);
            this._snackBar.open(
               `Nieuw wachtwoord voor gebruiker ${row.firstName} ${row.lastName} werd aangevraagd.`,
               'OK',
               {
                  duration: 2000,
               }
            );
         }
      });
   }
}
