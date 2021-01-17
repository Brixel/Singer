import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { RegistrationOverviewComponent } from './registration-overview.component';

describe('RegistrationOverviewComponent', () => {
  let component: RegistrationOverviewComponent;
  let fixture: ComponentFixture<RegistrationOverviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrationOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
