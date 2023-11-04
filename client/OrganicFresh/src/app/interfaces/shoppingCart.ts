import { CartItem } from "./cartItem";

export interface ShoppingCart{
    items:CartItem[],
    total:number
}