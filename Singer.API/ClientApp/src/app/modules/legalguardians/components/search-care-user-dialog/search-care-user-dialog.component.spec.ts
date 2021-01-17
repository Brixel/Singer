import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SearchCareUserDialogComponent } from './search-care-user-dialog.component';

describe('SearchCareUserDialogComponent', () => {
  let component: SearchCareUserDialogComponent;
  let fixture: ComponentFixture<SearchCareUserDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchCareUserDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchCareUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
