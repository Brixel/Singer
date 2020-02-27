import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationStatusToggleComponent } from './registration-status-toggle.component';

describe('RegistrationStatusToggleComponent', () => {
  let component: RegistrationStatusToggleComponent;
  let fixture: ComponentFixture<RegistrationStatusToggleComponent>;

  beforeEach(async(() => {
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
