import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingRegistrationsComponent } from './pending-registrations.component';

describe('PendingRegistrationsComponent', () => {
  let component: PendingRegistrationsComponent;
  let fixture: ComponentFixture<PendingRegistrationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PendingRegistrationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PendingRegistrationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
