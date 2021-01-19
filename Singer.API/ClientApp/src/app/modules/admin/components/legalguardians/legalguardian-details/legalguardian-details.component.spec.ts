import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { LegalguardianDetailsComponent } from './legalguardian-details.component';

describe('LegalguardianDetailsComponent', () => {
   let component: LegalguardianDetailsComponent;
   let fixture: ComponentFixture<LegalguardianDetailsComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [LegalguardianDetailsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(LegalguardianDetailsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
