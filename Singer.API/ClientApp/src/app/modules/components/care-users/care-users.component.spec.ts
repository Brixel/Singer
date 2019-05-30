import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CareUsersComponent } from './care-users.component';

describe('CareUsersComponent', () => {
  let component: CareUsersComponent;
  let fixture: ComponentFixture<CareUsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CareUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CareUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
