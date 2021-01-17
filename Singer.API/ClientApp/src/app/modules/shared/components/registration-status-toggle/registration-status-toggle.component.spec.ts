import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { RegistrationStatusToggleComponent } from './registration-status-toggle.component';

describe('RegistrationStatusToggleComponent', () => {
  let component: RegistrationStatusToggleComponent;
  let fixture: ComponentFixture<RegistrationStatusToggleComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrationStatusToggleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationStatusToggleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
