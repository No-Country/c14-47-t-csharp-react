import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartItem } from '../interfaces/cartItem';
import { ShoppingCart } from '../interfaces/shoppingCart';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  constructor() { 
    
    let shoppingCart:ShoppingCart ={
      items:[],
      total:0
    }
    
    if(localStorage.getItem('cart')){
      shoppingCart = JSON.parse(localStorage.getItem('cart')!);
    }

    this.cart = new BehaviorSubject<ShoppingCart>(shoppingCart);
    this.Show = new BehaviorSubject<boolean>(false);
  }

  private cart:BehaviorSubject<ShoppingCart>;

  private Show:BehaviorSubject<boolean>;

  get getCart():Observable<ShoppingCart>{
    return this.cart.asObservable();
  }

  get show ():Observable<boolean>{

    return this.Show.asObservable();
  }

  showCart():void{
    this.Show.next(true);
  }

  hiddenCart():void{
    this.Show.next(false);
  }

  addItem(item:CartItem):void{
    const cart = this.cart.value;
    const index = cart.items.findIndex(element => element.product.id === item.product.id);

    if(index !== -1) return;

    cart.total += item.subtotal;
    cart.items.push(item);

    this.cart.next(cart);
    this.updateLocalSorage();
  }

  removeItem(item:CartItem):void{
    
    const cart = this.cart.value;

    const index = cart?.items.findIndex(cartItem => cartItem.product.id === item.product.id);

    if(index !== -1){
      cart.total -= cart.items[index].subtotal;
      cart?.items.splice(index!,1);

      this.cart.next(cart);
      this.updateLocalSorage();
    }

  }

  addQuantity(idProduct:number, quantity:number):void{
    const cart = this.cart.value;
    const index = cart.items.findIndex(element => element.product.id === idProduct);

    if(index !== -1){
      cart.items[index].quantity +=  quantity;

      cart.items[index].subtotal += quantity * cart.items[index].product.price; 

      cart.total += quantity * cart.items[index].product.price;

      this.cart.next(cart);
      this.updateLocalSorage();
    }
  }
  
  subtractQuantity(idProduct:number, quantity:number):void{
    const cart = this.cart.value;
    const index = cart.items.findIndex(element => element.product.id === idProduct);

    if(index !== -1){
      if(cart.items[index].quantity>0){

        cart.items[index].quantity -=  quantity;

        cart.items[index].subtotal -= quantity * cart.items[index].product.price ;

        cart.total -= quantity * cart.items[index].product.price;
        this.cart.next(cart);
        this.updateLocalSorage();
      }

    }
  }

  deleteCart():void{
    const shoppingCart: ShoppingCart = {
      items:[],
      total:0
    }
    this.cart.next(shoppingCart);
    localStorage.removeItem('cart');
  }

  private updateLocalSorage():void{
    localStorage.setItem('cart', JSON.stringify(this.cart.value));
  }



}
