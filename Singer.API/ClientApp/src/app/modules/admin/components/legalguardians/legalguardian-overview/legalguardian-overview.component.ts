import { AfterViewInit, Component, ViewChild, ElementRef } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { LegalguardianOverviewDataSource } from './legalguardian-overview-datasource';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';

@Component({
   selector: 'app-legalguardian-overview',
   templateUrl: './legalguardian-overview.component.html',
   styleUrls: ['./legalguardian-overview.component.css'],
})
export class LegalguardianOverviewComponent implements AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;
   dataSource: LegalguardianOverviewDataSource;

   pageSize = 15;
   pageIndex = 0;

   filter: string;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = [
      //'id',
      'firstName',
      'lastName',
      'birthDate',
      'email',
      'userName',
      'address',
      'phoneNumber',
      'gsm',
   ];

   constructor(private legalguardiansService: LegalguardiansService) {}

   ngOnInit(){
      this.dataSource = new LegalguardianOverviewDataSource(this.legalguardiansService);
      this.sort.active = 'lastName';
      this.sort.direction = 'asc';
      this.loadLegalGuardians();
   }

   selectRow(row: LegalGuardian): void {

   }

   addLegalGuardian(): void {

   }

   private loadLegalGuardians(){
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadLegalGuardians(sortDirection, sortColumn, this.pageIndex, this.pageSize, this.filter);
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