import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CareUserMultiAutocompleteComponent } from './care-user-multi-autocomplete.component';

describe('CareUserMultiAutocompleteComponent', () => {
  let component: CareUserMultiAutocompleteComponent;
  let fixture: ComponentFixture<CareUserMultiAutocompleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CareUserMultiAutocompleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CareUserMultiAutocompleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
