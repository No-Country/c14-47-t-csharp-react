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

  create(formData:FormData):Observable<Product>{
    
    return this.http.post<Product>(this.apiUrl,formData);
  }

  update(id:number, formData:FormData):Observable<Product>{

    return this.http.put<Product>(this.apiUrl+'/'+id, formData);
  }

}
