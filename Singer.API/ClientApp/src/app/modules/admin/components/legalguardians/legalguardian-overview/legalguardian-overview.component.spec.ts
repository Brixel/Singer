import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule, MatSortModule, MatTableModule } from '@angular/material';

import { LegalguardianOverviewComponent } from './legalguardian-overview.component';

describe('LegalguardianOverviewComponent', () => {
   let component: LegalguardianOverviewComponent;
   let fixture: ComponentFixture<LegalguardianOverviewComponent>;

   beforeEach(async(() => {
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
