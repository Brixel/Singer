import { DataSource, CollectionViewer } from '@angular/cdk/collections';
import { MatPaginator, MatSort } from '@angular/material';
import { map } from 'rxjs/operators';
import { Observable, of as observableOf, merge, BehaviorSubject } from 'rxjs';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';

/**
 * Data source for the LegalguardianOverview view. This class should
 */
export class LegalguardianOverviewDataSource extends DataSource<LegalGuardian> {
   private legalGuardiansSubject$ = new BehaviorSubject<LegalGuardian[]>([]);
   private totalSizeSubject$ = new BehaviorSubject<number>(0);
   private queryCountSubject$ = new BehaviorSubject<number>(0);
   private loadingSubject$ = new BehaviorSubject<boolean>(false);

   public legalGuardians$ = this.legalGuardiansSubject$.asObservable();
   public totalSize$ = this.totalSizeSubject$.asObservable();
   public queryCount$ = this.queryCountSubject$.asObservable();
   public loading$ = this.loadingSubject$.asObservable();

   constructor(private legalguardiansService: LegalguardiansService) {
      super();
   }

   public loadLegalGuardians(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?:number,
      pageSize?: number, filter?: string){

      this.loadingSubject$.next(true);
         this.legalguardiansService.fetchLegalGuardiansData(sortDirection, sortColumn, pageIndex, pageSize, filter).subscribe((res) => {
            this.legalGuardiansSubject$.next(res.items as LegalGuardian[]);
            this.totalSizeSubject$.next(res.totalSize);
            this.queryCountSubject$.next(res.size);
            this.loadingSubject$.next(false);
         });
   }

   connect(collectionViewer: CollectionViewer): Observable<LegalGuardian[]> {
      return this.legalGuardiansSubject$.asObservable();
   }
   disconnect() {
      this.legalGuardiansSubject$.complete();
   }
}
