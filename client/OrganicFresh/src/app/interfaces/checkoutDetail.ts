import { Sale } from "./sale";

export interface CheckoutDetail{

    productName:string,
    sale:Sale,
    saleId:number,
    quantity:number,
    total:number
}