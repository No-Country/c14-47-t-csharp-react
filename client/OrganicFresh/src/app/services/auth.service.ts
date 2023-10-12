import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterRequest} from '../interfaces/registerRequest';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Credential } from '../interfaces/credential';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }

  private apiUrl:string = environment.API_URL+'/Auth';

  registerApi(user:RegisterRequest):Observable<Credential>{
    
    return  this.http.post<Credential>(this.apiUrl+'/register', user);

  }
}
