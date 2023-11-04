import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateProductComponent } from './create-update-product.component';

describe('CreateUpdateProductComponent', () => {
  let component: CreateUpdateProductComponent;
  let fixture: ComponentFixture<CreateUpdateProductComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateUpdateProductComponent]
    });
    fixture = TestBed.createComponent(CreateUpdateProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
