import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginationDTO } from '../../core/models/careuser.model';
import { AdminUserProxy } from './adminuser.proxy';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminUserService {

  constructor(private adminUserProxy: AdminUserProxy) { }
  get(sortDirection: string, sortColumn: string, pageIndex: number, pageSize: number, filter: string): Observable<PaginationDTO> {
     return this.adminUserProxy.get(sortDirection, sortColumn, pageIndex, pageSize, filter).pipe(map((res) => res));
  }
}
