import {
   AfterViewInit,
   Component,
   ViewChild,
   ElementRef,
   OnInit,
} from '@angular/core';
import {
   MatPaginator,
   MatSort,
   MatDialog,
   MatSnackBar,
} from '@angular/material';
import { LegalguardianOverviewDataSource } from './legalguardian-overview-datasource';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { LegalguardianDetailsComponent } from '../legalguardian-details/legalguardian-details.component';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
   selector: 'app-legalguardian-overview',
   templateUrl: './legalguardian-overview.component.html',
   styleUrls: ['./legalguardian-overview.component.css'],
})
export class LegalguardianOverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;

   dataSource: LegalguardianOverviewDataSource;

   pageSize = 15;
   pageIndex = 0;

   filter: string;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = ['firstName', 'lastName', 'email', 'address'];

   readonly maxFilterLength = 2048;

   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      filterFieldControl: new FormControl(this.filter, [
         Validators.maxLength(this.maxFilterLength),
      ]),
   });

   constructor(
      public dialog: MatDialog,
      private _legalguardiansService: LegalguardiansService,
      private _snackBar: MatSnackBar,
      private _loadingService: LoadingService
   ) {}

   ngOnInit() {
      this.dataSource = new LegalguardianOverviewDataSource(
         this._legalguardiansService
      );
      this.sort.active = 'firstName';
      this.sort.direction = 'asc';
      this.loadLegalGuardians();
   }

   selectRow(row: LegalGuardian): void {
      //Dereference row to avoid updating row in overview when API might refuse the update
      const deRefRow = { ...row };
      const dialogRef = this.dialog.open(LegalguardianDetailsComponent, {
         data: { legalGuardianInstance: deRefRow, displayLinkedUserFields: true, },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe(
         (result: LegalGuardian) => {
            //Update the legal guardian
            this._legalguardiansService.updateLegalGuardian(result).subscribe(
               res => {
                  // Reload LegalGuardians
                  this.loadLegalGuardians();
                  this._snackBar.open(
                     `${result.firstName} ${result.lastName} werd aangepast.`,
                     'OK',
                     { duration: 2000 }
                  );
               },
               err => {
                  this.handleApiError(err);
               }
            );
         }
      );
   }

   addLegalGuardian(): void {
      const dialogRef = this.dialog.open(LegalguardianDetailsComponent, {
         data: { legalGuardianInstance: null, displayLinkedUserFields: false, },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe(
         (result: LegalGuardian) => {
            // Add the legal guardian
            this._legalguardiansService.createLegalGuardian(result).subscribe(
               res => {
                   // Reload LegalGuardians
                  this.loadLegalGuardians();

                  this._snackBar.open(
                     `${result.firstName} ${result.lastName} werd toegevoegd als voogd.`,
                     'OK',
                     { duration: 2000 }
                  );
               },
               err => {
                  this.handleApiError(err);
               }
            );
         }
      );
   }

   private loadLegalGuardians() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadLegalGuardians(
         sortDirection,
         sortColumn,
         this.pageIndex,
         this.pageSize,
         this.filter
      );
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

   handleApiError(err: any) {
      if (typeof err === 'string') {
         this._snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         let messages = [];
         for (var k in err) {
            messages.push(err[k]);
         }
         this._snackBar.open(
            `⚠ Er zijn fouten opgetreden bij het opslagen:\n${messages.join(
               '\n'
            )}`,
            'OK',
            {
               panelClass: 'multi-line-snackbar',
            }
         );
      }
   }
}
