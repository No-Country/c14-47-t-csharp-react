import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { CategoriesComponent } from './categories/categories.component';
import { ProductsComponent } from './products/products.component';
import { SalesComponent } from './sales/sales.component';

const routes: Routes = [
  {
    path:'', component:AdminComponent, children:[
      {
        path:'categories', component: CategoriesComponent
      },
      {
        path:'products', component: ProductsComponent
      },
      {
        path:'sales', component:SalesComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
