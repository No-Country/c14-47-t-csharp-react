import { Component, Output, EventEmitter } from '@angular/core';
import { Observable, take } from 'rxjs';
import { CartItem } from 'src/app/interfaces/cartItem';
import { ShoppingCart } from 'src/app/interfaces/shoppingCart';
import { CartService } from 'src/app/services/cart.service';
import { CheckoutService } from 'src/app/services/checkout.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent {

  constructor(private cartService:CartService, private checkoutService:CheckoutService){
    this.cart$ = cartService.getCart;
  }

  cart$:Observable<ShoppingCart>;

  close():void{
    this.cartService.hiddenCart();
  }

  addQuantity(idProduct:number, quantity:number= 1):void{
    this.cartService.addQuantity(idProduct, quantity);
  }

  subtractQuantity(idProduct:number, quantity:number=1):void{
    this.cartService.subtractQuantity(idProduct, quantity);
  }

  deleteItem(item:CartItem):void{
    this.cartService.removeItem(item);
  }
  proceedToCheckout():void{
    this.cart$.pipe(take(1)).subscribe({
      next:(res)=>{

        this.checkoutService.createCheckoutSession(res.items).subscribe({
          next:(res)=>{
            window.open(res.checkoutUrl,'_self')
          },
          error:(err)=>{
          }
        });
      }
    });
  }

}
