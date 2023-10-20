import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Product } from '../interfaces/product';
import { ResProduct } from '../interfaces/resProduct';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http:HttpClient) { }

  apiUrl : string  = environment.API_URL + '/products';

  getAll():Observable<ResProduct> {
    return this.http.get<ResProduct>(`${this.apiUrl}`);
  }

}
