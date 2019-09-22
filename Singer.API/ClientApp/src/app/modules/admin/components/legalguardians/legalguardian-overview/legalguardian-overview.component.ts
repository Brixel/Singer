import {
   AfterViewInit,
   Component,
   ViewChild,
   ElementRef,
   OnInit,
} from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { LegalguardianOverviewDataSource } from './legalguardian-overview-datasource';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { LegalguardianDetailsComponent } from '../legalguardian-details/legalguardian-details.component';

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

   constructor(
      public dialog: MatDialog,
      private legalguardiansService: LegalguardiansService
   ) {}

   ngOnInit() {
      this.dataSource = new LegalguardianOverviewDataSource(
         this.legalguardiansService
      );
      this.sort.active = 'firstName';
      this.sort.direction = 'asc';
      this.loadLegalGuardians();
   }

   selectRow(row: LegalGuardian): void {
      const dialogRef = this.dialog.open(LegalguardianDetailsComponent, {
         data: { legalGuardianInstance: row, isAdding: false },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe(
         (result: LegalGuardian) => {
            //Update the legal guardian
            this.legalguardiansService
               .updateLegalGuardian(result)
               .subscribe(res => {
                  // Reload LegalGuardian
                  this.loadLegalGuardians();
               });
         }
      );
   }

   addLegalGuardian(): void {
      const dialogRef = this.dialog.open(LegalguardianDetailsComponent, {
         data: { careUserInstance: null, isAdding: true },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe(
         (result: LegalGuardian) => {
            // Add the legal guardian
            this.legalguardiansService
               .createLegalGuardian(result)
               .subscribe(res => {
                  this.loadLegalGuardians();
               });
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
   }
}
