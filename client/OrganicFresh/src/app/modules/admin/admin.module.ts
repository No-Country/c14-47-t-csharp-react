import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { SharedModule } from '../shared/shared.module';
import { CategoriesComponent } from './categories/categories.component';
import { CreateUpdateCategoryComponent } from './categories/modals/create-update-category/create-update-category.component';


@NgModule({
  declarations: [
    AdminComponent,
    CategoriesComponent,
    CreateUpdateCategoryComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
