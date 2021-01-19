import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PendingActionsComponent } from './pending-actions.component';

describe('PendingActionsComponent', () => {
   let component: PendingActionsComponent;
   let fixture: ComponentFixture<PendingActionsComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [PendingActionsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(PendingActionsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
