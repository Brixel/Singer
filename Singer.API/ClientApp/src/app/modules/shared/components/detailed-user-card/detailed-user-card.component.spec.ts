import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailedUserCardComponent } from './detailed-user-card.component';

describe('DetailedUserCardComponent', () => {
  let component: DetailedUserCardComponent;
  let fixture: ComponentFixture<DetailedUserCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailedUserCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailedUserCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
