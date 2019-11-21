import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFamilyWizardComponent } from './add-family-wizard.component';

describe('AddFamilyWizardComponent', () => {
  let component: AddFamilyWizardComponent;
  let fixture: ComponentFixture<AddFamilyWizardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddFamilyWizardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddFamilyWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
