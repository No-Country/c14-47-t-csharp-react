import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Sale } from '../interfaces/sale';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  apiUrl:string = environment.API_URL+'/admin';


  getSales():Observable<Sale[]>{
    return this.http.get<Sale[]>(this.apiUrl+'/sales');
  }

}
