import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AdminDetailsComponent } from './admin-details.component';

describe('AdminDetailsComponent', () => {
   let component: AdminDetailsComponent;
   let fixture: ComponentFixture<AdminDetailsComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [AdminDetailsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(AdminDetailsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
