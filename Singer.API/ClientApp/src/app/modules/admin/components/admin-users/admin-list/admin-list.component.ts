import {
   Component,
   OnInit,
   ViewChild,
   ElementRef,
   AfterViewInit,
} from '@angular/core';
import {
   MatPaginator,
   MatSort,
   MatDialog,
   MatSnackBar,
} from '@angular/material';
import { AdminDatasource } from '../../../services/admin.datasource';
import { AdminUserService } from '../../../services/admin-user.service';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { AdminUser } from 'src/app/modules/core/models/adminuser.model';
import { AdminDetailsComponent } from '../admin-details/admin-details.component';
import { LoadingService } from 'src/app/modules/core/services/loading.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
   ConfirmComponent,
   ConfirmRequest,
} from 'src/app/modules/core/components/confirm/confirm.component';
import { AuthService } from 'src/app/modules/core/services/auth.service';
import {
   DeleteConfirmationDialogComponent,
   ConfirmationData,
} from 'src/app/modules/shared/components/delete-confirmation-dialog/delete-confirmation-dialog.component';

@Component({
   selector: 'app-admin-list',
   templateUrl: './admin-list.component.html',
   styleUrls: ['./admin-list.component.css'],
})
export class AdminListComponent implements OnInit, AfterViewInit {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild('filterInput') filterInput: ElementRef;

   pageSize = 15;
   pageIndex = 0;

   /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
   displayedColumns = ['firstName', 'lastName', 'email', 'userName', 'actions'];
   filter: string;
   dataSource: AdminDatasource;

   readonly maxFilterLength = 2048;

   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      filterFieldControl: new FormControl(this.filter, [
         Validators.maxLength(this.maxFilterLength),
      ]),
   });

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

   loadAdmins() {
      const sortDirection = this.sort.direction;
      const sortColumn = this.sort.active;
      this.filter = this.filterInput.nativeElement.value;
      this.dataSource.loadAdmins(
         sortDirection,
         sortColumn,
         this.pageIndex,
         this.pageSize,
         this.filter
      );
   }

   editAdmin(row: AdminUser): void {
      const dialogRef = this.dialog.open(AdminDetailsComponent, {
         data: row,
      });

      dialogRef.componentInstance.submitEvent.subscribe((result: AdminUser) => {
         // Update the Careuser
         this.adminUserService.update(result).subscribe(() => {
            // Reload Careusers
            this.loadAdmins();
         });
      });
   }
   addAdmin() {
      const dialogRef = this.dialog.open(AdminDetailsComponent);

      dialogRef.componentInstance.submitEvent.subscribe((result: AdminUser) => {
         this.adminUserService.create(result).subscribe(() => {
            this.loadAdmins();
         });
      });
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
         }
      });
   }

   deleteUser(row: AdminUser) {
      const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, {
         data: <ConfirmationData>{
            name: `${row.firstName} ${row.lastName}`,
            whatToDelete: 'Beheerder',
         },
      });
      dialogRef.afterClosed().subscribe((isConfirmed: boolean) => {
         if (isConfirmed) {
            this.adminUserService.delete(row).subscribe(() => {
               this._snackBar.open(
                  `Beheerder ${row.firstName} ${row.lastName} werd verwijderd.`,
                  'OK',
                  {
                     duration: 2000,
                  }
               );
               this.loadAdmins();
            });
         }
      });
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
}
