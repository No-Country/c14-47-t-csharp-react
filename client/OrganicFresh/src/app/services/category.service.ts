import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../interfaces/category';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }

  apiUrl:string = environment.API_URL+'/Categories'

  getAll():Observable<Category[]>{

    return this.http.get<Category[]>(this.apiUrl);
  }


}
