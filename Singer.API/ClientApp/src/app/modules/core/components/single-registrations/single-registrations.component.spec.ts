import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleRegistrationsComponent } from './single-registrations.component';

describe('SingleRegistrationsComponent', () => {
  let component: SingleRegistrationsComponent;
  let fixture: ComponentFixture<SingleRegistrationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingleRegistrationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingleRegistrationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
