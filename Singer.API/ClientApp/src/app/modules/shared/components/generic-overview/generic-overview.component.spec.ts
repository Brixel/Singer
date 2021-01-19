import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { GenericOverviewComponent } from './generic-overview.component';

describe('GenericOverviewComponent', () => {
   let component: GenericOverviewComponent;
   let fixture: ComponentFixture<GenericOverviewComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [GenericOverviewComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(GenericOverviewComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
