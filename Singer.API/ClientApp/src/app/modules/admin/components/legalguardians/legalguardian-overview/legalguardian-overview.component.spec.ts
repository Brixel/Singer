import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';

import { LegalguardianOverviewComponent } from './legalguardian-overview.component';

describe('LegalguardianOverviewComponent', () => {
   let component: LegalguardianOverviewComponent;
   let fixture: ComponentFixture<LegalguardianOverviewComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [LegalguardianOverviewComponent],
         imports: [NoopAnimationsModule, MatPaginatorModule, MatSortModule, MatTableModule],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(LegalguardianOverviewComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should compile', () => {
      expect(component).toBeTruthy();
   });
});
