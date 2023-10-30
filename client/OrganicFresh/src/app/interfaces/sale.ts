import { CheckoutDetail } from "./checkoutDetail";

export interface Sale{
 id:number,
 saleId:number,
 createdAt:Date,
 checkoutDetails:CheckoutDetail[],
 total:number,
 userName:string,   
}
