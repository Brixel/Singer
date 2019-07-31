import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingereventDetailsComponent } from './singerevent-details.component';

describe('SingereventDetailsComponent', () => {
  let component: SingereventDetailsComponent;
  let fixture: ComponentFixture<SingereventDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingereventDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingereventDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
