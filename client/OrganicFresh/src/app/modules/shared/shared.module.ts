import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { AlertRegisterComponent } from './components/alert-register/alert-register.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatIconModule} from '@angular/material/icon';
import { FooterComponent } from './components/footer/footer.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { DetailOrderComponent } from './components/detail-order/detail-order.component';



@NgModule({
  declarations: [
    AlertRegisterComponent,
    FooterComponent,
    ShoppingCartComponent,
    DetailOrderComponent
  ],
  imports: [
    CommonModule,
    MatButtonModule,
  ],
  exports:[
    MatButtonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatIconModule,
    FooterComponent,
    ShoppingCartComponent
  ]
})
export class SharedModule { }
