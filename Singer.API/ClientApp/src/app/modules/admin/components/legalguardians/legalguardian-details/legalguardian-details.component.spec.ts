import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LegalguardianDetailsComponent } from './legalguardian-details.component';

describe('LegalguardianDetailsComponent', () => {
   let component: LegalguardianDetailsComponent;
   let fixture: ComponentFixture<LegalguardianDetailsComponent>;

   beforeEach(async(() => {
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
