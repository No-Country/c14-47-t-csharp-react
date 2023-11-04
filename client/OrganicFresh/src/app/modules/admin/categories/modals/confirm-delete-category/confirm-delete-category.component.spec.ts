import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmDeleteCategoryComponent } from './confirm-delete-category.component';

describe('ConfirmDeleteCategoryComponent', () => {
  let component: ConfirmDeleteCategoryComponent;
  let fixture: ComponentFixture<ConfirmDeleteCategoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConfirmDeleteCategoryComponent]
    });
    fixture = TestBed.createComponent(ConfirmDeleteCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
