import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateCategoryComponent } from './create-update-category.component';

describe('CreateUpdateCategoryComponent', () => {
  let component: CreateUpdateCategoryComponent;
  let fixture: ComponentFixture<CreateUpdateCategoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateUpdateCategoryComponent]
    });
    fixture = TestBed.createComponent(CreateUpdateCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
