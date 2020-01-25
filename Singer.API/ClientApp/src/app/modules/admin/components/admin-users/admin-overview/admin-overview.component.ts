import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { AdminDatasource } from '../../../services/admin.datasource';
import { AdminUserService } from '../../../services/admin-user.service';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { AdminUser } from 'src/app/modules/core/models/adminuser.model';
import { AdminDetailsComponent } from '../admin-details/admin-details.component';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ConfirmComponent, ConfirmRequest } from 'src/app/modules/shared/components/confirm/confirm.component';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-admin-overview',
   templateUrl: './admin-overview.component.html',
   styleUrls: ['./admin-overview.component.css'],
})
export class AdminOverviewComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;

   // Filter
   filter: string;

   readonly maxFilterLength = 2048;

   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      filterFieldControl: new FormControl(this.filter, [Validators.maxLength(this.maxFilterLength)]),
   });

   // Datatable
   dataSource: AdminDatasource;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = ['firstName', 'lastName', 'email', 'userName', 'actions'];

   // Paginator
   pageSize = 15;
   pageIndex = 0;

   constructor(
      public dialog: MatDialog,
      private adminUserService: AdminUserService,
      private authService: AuthService,
      private _loadingService: LoadingService,
      private _snackBar: MatSnackBar
   ) {}

   ngOnInit() {
      this.dataSource = new AdminDatasource(this.adminUserService);
      this.sort.active = 'lastName';
      this.sort.direction = 'asc';
      this.loadAdmins();
   }

   ngAfterViewInit() {
      fromEvent(this.filterInput.nativeElement, 'keyup')
         .pipe(
            debounceTime(400),
            distinctUntilChanged(),
            tap(() => {
               this.paginator.pageIndex = 0;
               this.loadAdmins();
            })
         )
         .subscribe();
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
      merge(this.sort.sortChange, this.paginator.page)
         .pipe(
            tap(() => {
               this.pageIndex = this.paginator.pageIndex;
               this.pageSize = this.paginator.pageSize;
               this.loadAdmins();
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

   loadAdmins() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadAdmins(sortDirection, sortColumn, this.pageIndex, this.pageSize, this.filter);
   }

   addAdmin() {
      const dialogRef = this.dialog.open(AdminDetailsComponent);

      dialogRef.componentInstance.submitEvent.subscribe((result: AdminUser) => {
         this.adminUserService.create(result).subscribe(
            () => {
               this._snackBar.open(`Gebruiker ${result.firstName} ${result.lastName} werd toegevoegd.`, 'OK', {
                  duration: 2000,
               });
               this.loadAdmins();
            },
            err => {
               this.handleApiError(err);
            }
         );
      });
   }

   editAdmin(row: AdminUser): void {
      const dialogRef = this.dialog.open(AdminDetailsComponent, {
         data: row,
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: AdminUser) => {
         this.adminUserService.update(result).subscribe(
            () => {
               this._snackBar.open(`Gebruiker ${result.firstName} ${result.lastName} werd aangepast.`, 'OK', {
                  duration: 2000,
               });
               this.loadAdmins();
            },
            err => {
               this.handleApiError(err);
            }
         );
      });

      dialogRef.componentInstance.deleteEvent.subscribe(
         (result: AdminUser) => {
            this.adminUserService.delete(result).subscribe(
               res => {
                  // Reload AdminUsers
                  this.loadAdmins();
                  this._snackBar.open(
                     `Beheerder ${result.firstName} ${result.lastName} werd verwijderd.`,
                     'OK',
                     { duration: 2000 }
                  );
               },
               err => {
                  this.handleApiError(err);
                  this.loadAdmins();
               }
            );
         }
      );
   }

   changePassword(row: AdminUser) {
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

   handleApiError(err: any) {
      if (typeof err === 'string') {
         this._snackBar.open(`⚠ ${err}`, 'OK');
      } else if (typeof err === 'object' && err !== null) {
         let messages = [];
         for (var k in err) {
            messages.push(err[k]);
         }
         this._snackBar.open(`⚠ Er zijn fouten opgetreden bij het opslaan:\n${messages.join('\n')}`, 'OK', {
            panelClass: 'multi-line-snackbar',
         });
      }
   }
}
