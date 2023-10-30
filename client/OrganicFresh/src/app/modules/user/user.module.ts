import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { OrdersComponent } from './orders/orders.component';
import { SharedModule } from '../shared/shared.module';
import { SuccessComponent } from './success/success.component';
import { ErrorComponent } from './error/error.component';


@NgModule({
  declarations: [
    UserComponent,
    OrdersComponent,
    SuccessComponent,
    ErrorComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
