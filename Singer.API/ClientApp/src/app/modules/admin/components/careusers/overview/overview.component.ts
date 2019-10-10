import {
   AfterViewInit,
   Component,
   ViewChild,
   OnInit,
   ElementRef,
} from '@angular/core';
import {
   MatPaginator,
   MatSort,
   MatDialog,
   MAT_DATE_FORMATS,
} from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { DataSource } from '@angular/cdk/table';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { CareUserDetailsComponent } from '../care-user-details/care-user-details.component';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { MY_FORMATS } from 'src/app/app.module';

@Component({
   selector: 'app-overview',
   templateUrl: './overview.component.html',
   styleUrls: ['./overview.component.css'],
})
export class OverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;
   dataSource: OverviewDataSource;

   pageSize = 15;
   pageIndex = 0;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = [
      'firstName',
      'lastName',
      'birthDay',
      'caseNumber',
      'ageGroup',
      'isExtern',
      'hasTrajectory',
      'normalDaycareLocation',
      'vacationDaycareLocation',
      'hasResources',
   ];
   filter: string;

   constructor(
      public dialog: MatDialog,
      private careUserService: CareUserService
   ) {}

   ngOnInit() {
      this.dataSource = new OverviewDataSource(this.careUserService);
      this.sort.active = 'firstName';
      this.sort.direction = 'asc';
      this.loadCareUsers();
   }

   selectRow(row: CareUser): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: { careUserInstance: row, isAdding: false },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         // Update the Careuser
         this.careUserService.updateUser(result).subscribe(res => {
            // Reload Careusers
            this.loadCareUsers();
         });
      });
   }

   addCareUser(): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: { careUserInstance: null, isAdding: true },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         this.careUserService.createCareUser(result).subscribe(res => {
            this.loadCareUsers();
         });
      });
   }

   private loadCareUsers() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadCareUsers(
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
               this.loadCareUsers();
            })
         )
         .subscribe();
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
      merge(this.sort.sortChange, this.paginator.page)
         .pipe(
            tap(() => {
               this.pageIndex = this.paginator.pageIndex;
               this.pageSize = this.paginator.pageSize;
               this.loadCareUsers();
            })
         )
         .subscribe();
   }
}
