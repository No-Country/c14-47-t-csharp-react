import { Sale } from "./sale";

export interface User{
    id:string,
    email:string,
    name:string,
    sales:Sale[]
    
}