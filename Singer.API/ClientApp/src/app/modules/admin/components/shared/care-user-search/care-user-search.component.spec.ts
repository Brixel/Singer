import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CareUserSearchComponent } from './care-user-search.component';

describe('CareUserSearchComponent', () => {
   let component: CareUserSearchComponent;
   let fixture: ComponentFixture<CareUserSearchComponent>;

   beforeEach(async(() => {
      TestBed.configureTestingModule({
         declarations: [CareUserSearchComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(CareUserSearchComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
