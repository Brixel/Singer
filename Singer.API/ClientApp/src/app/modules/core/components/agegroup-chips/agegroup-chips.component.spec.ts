import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AgegroupChipsComponent } from './agegroup-chips.component';

describe('AgegroupChipsComponent', () => {
   let component: AgegroupChipsComponent;
   let fixture: ComponentFixture<AgegroupChipsComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [AgegroupChipsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(AgegroupChipsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
