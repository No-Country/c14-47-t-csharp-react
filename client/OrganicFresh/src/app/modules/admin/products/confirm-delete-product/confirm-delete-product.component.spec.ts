import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmDeleteProductComponent } from './confirm-delete-product.component';

describe('ConfirmDeleteProductComponent', () => {
  let component: ConfirmDeleteProductComponent;
  let fixture: ComponentFixture<ConfirmDeleteProductComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConfirmDeleteProductComponent]
    });
    fixture = TestBed.createComponent(ConfirmDeleteProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
