import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user.component';
import { OrdersComponent } from './orders/orders.component';
import { SuccessComponent } from './success/success.component';
import { ErrorComponent } from './error/error.component';

const routes: Routes = [
  {
    path:'', component:UserComponent ,children:[
      {path:'orders', component:OrdersComponent},
      {path:'success/:order', component:SuccessComponent},
      {path:'error', component:ErrorComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
