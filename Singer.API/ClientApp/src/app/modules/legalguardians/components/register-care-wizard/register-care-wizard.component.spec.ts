import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterCareWizardComponent } from './register-care-wizard.component';

describe('RegisterCareWizardComponent', () => {
  let component: RegisterCareWizardComponent;
  let fixture: ComponentFixture<RegisterCareWizardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterCareWizardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterCareWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
