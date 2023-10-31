import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';
import { ResCheckout } from '../interfaces/resCheckout';
import { CartItem } from '../interfaces/cartItem';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private http:HttpClient) { }

  apiUrl:string = environment.API_URL+'/checkout';

  createCheckoutSession(cartItems:CartItem[]):Observable<ResCheckout>{
    
    return this.http.post<ResCheckout>(this.apiUrl, cartItems);
  }
}
