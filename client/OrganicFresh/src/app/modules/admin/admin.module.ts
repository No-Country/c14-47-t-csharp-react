import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { SharedModule } from '../shared/shared.module';
import { CategoriesComponent } from './categories/categories.component';
import { CreateUpdateCategoryComponent } from './categories/modals/create-update-category/create-update-category.component';
import { ConfirmDeleteCategoryComponent } from './categories/modals/confirm-delete-category/confirm-delete-category.component';
import { ProductsComponent } from './products/products.component';
import { CreateUpdateProductComponent } from './products/create-update-product/create-update-product.component';


@NgModule({
  declarations: [
    AdminComponent,
    CategoriesComponent,
    CreateUpdateCategoryComponent,
    ConfirmDeleteCategoryComponent,
    ProductsComponent,
    CreateUpdateProductComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
