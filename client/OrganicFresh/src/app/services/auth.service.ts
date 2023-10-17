import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterRequest} from '../interfaces/registerRequest';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Credential } from '../interfaces/credential';
import { LoginRequest } from '../interfaces/loginRequest';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { 

    this.credential = new BehaviorSubject<Credential | null>(JSON.parse(localStorage.getItem('credential')!));

  }

  private apiUrl:string = environment.API_URL+'/Auth';


  private credential:BehaviorSubject<Credential | null>;

  get getCredential():Observable<Credential|null>{
    return this.credential.asObservable();
  }


  register(user:RegisterRequest):Observable<Credential>{
    
    return  this.http.post<Credential>(this.apiUrl+'/register', user);

  }

  login(loginRequest:LoginRequest):Observable<Credential>{
    
    return this.http.post<Credential>(this.apiUrl+'/login', loginRequest).pipe(map((res)=>{

      this.credential.next(res);
      localStorage.setItem('credential', JSON.stringify(res));

      return res;
    })); 
  }
}
