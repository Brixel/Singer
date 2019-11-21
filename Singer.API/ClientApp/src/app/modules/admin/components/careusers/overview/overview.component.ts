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
   MatSnackBar,
} from '@angular/material';
import { OverviewDataSource } from './overview-datasource';
import { merge, fromEvent } from 'rxjs';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { CareUserDetailsComponent } from '../care-user-details/care-user-details.component';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { CareUser } from 'src/app/modules/core/models/careuser.model';

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
      private careUserService: CareUserService,
      private snackBar: MatSnackBar
   ) {}

   ngOnInit() {
      this.dataSource = new OverviewDataSource(this.careUserService);
      this.sort.active = 'firstName';
      this.sort.direction = 'asc';
      this.loadCareUsers();
   }

   selectRow(row: CareUser): void {
      //Dereference row to avoid updating row in overview when API might refuse the update
      const deRefRow = { ...row };
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: { careUserInstance: deRefRow, isAdding: false, displayContactFields: true, },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {

         // Update the Careuser
         this.careUserService.updateUser(result).subscribe(
            () => {
               // Reload Careusers
               this.loadCareUsers();
               this.snackBar.open(
                  `Gebruiker ${result.firstName} ${result.lastName} werd aangepast.`,
                  'OK',
                  { duration: 2000 }
               );
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   addCareUser(): void {
      const dialogRef = this.dialog.open(CareUserDetailsComponent, {
         data: { careUserInstance: null, isAdding: true, displayContactFields: false, },
         width: '80vw',
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: CareUser) => {
         this.careUserService.createCareUser(result).subscribe(
            _ => {
               this.loadCareUsers();
               this.snackBar.open(
                  `Gebruiker ${result.firstName} ${result.lastName} werd toegevoegd.`,
                  'OK',
                  { duration: 2000 }
               );
            },
            err => {
               this.handleApiError(err);
            }
         );
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

   handleApiError(err: any) {
      if (typeof err === 'string') {
         this.snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         let messages = [];
         for (var k in err) {
            messages.push(err[k]);
         }
         this.snackBar.open(
            `⚠ Er zijn fouten opgetreden bij het opslaan:\n${messages.join(
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
