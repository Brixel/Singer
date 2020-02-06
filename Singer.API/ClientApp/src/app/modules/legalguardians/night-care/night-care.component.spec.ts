import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NightCareComponent } from './night-care.component';

describe('NightCareComponent', () => {
  let component: NightCareComponent;
  let fixture: ComponentFixture<NightCareComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NightCareComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NightCareComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
