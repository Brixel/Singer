import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DeleteConfirmationDialogComponent } from './delete-confirmation-dialog.component';

describe('DeleteConfirmationDialogComponent', () => {
   let component: DeleteConfirmationDialogComponent;
   let fixture: ComponentFixture<DeleteConfirmationDialogComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [DeleteConfirmationDialogComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(DeleteConfirmationDialogComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
