import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingerEventAdminRegisterComponent } from './singer-eventadmin-register.component';

describe('SingerEventadminRegisterComponent', () => {
  let component: SingerEventAdminRegisterComponent;
  let fixture: ComponentFixture<SingerEventAdminRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingerEventAdminRegisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingerEventAdminRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
