import { Product } from "./product";

export interface CartItem{
    product:Product,
    productId:number,
    quantity:number,
    subtotal:number
}