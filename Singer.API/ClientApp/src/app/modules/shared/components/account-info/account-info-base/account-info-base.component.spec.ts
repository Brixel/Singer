import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountInfoBaseComponent } from './account-info-base.component';

describe('AccountInfoBaseComponent', () => {
  let component: AccountInfoBaseComponent;
  let fixture: ComponentFixture<AccountInfoBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountInfoBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountInfoBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
