import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterRequest} from '../interfaces/registerRequest';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Credential } from '../interfaces/credential';
import { LoginRequest } from '../interfaces/loginRequest';
import { Router } from '@angular/router';
import { CartService } from './cart.service';
import { User } from '../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient, private router:Router, private cartService:CartService) { 

    this.credential = new BehaviorSubject<Credential | null>(JSON.parse(localStorage.getItem('credential')!));

    this.user = new BehaviorSubject<User|null>(null);

  }

  private apiUrl:string = environment.API_URL+'/Auth';


  private credential:BehaviorSubject<Credential | null>;

  private user:BehaviorSubject<User|null>;

  get getCredential():Observable<Credential|null>{
    return this.credential.asObservable();
  }

  get getUser():Observable<User|null>{
    return this.user.asObservable();
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

  logout(route:string = 'index'):void{
    this.credential.next(null);
    localStorage.removeItem('credential');
    this.router.navigate([route]);
    this.cartService.hiddenCart();
    this.cartService.deleteCart();

  }

  me():Observable<User>{
    return this.http.get<User>(this.apiUrl+'/me').pipe(map((res)=>{
      this.user.next(res);

      return res;
    }));
  }

  meAdmin():Observable<void>{
    
    return this.http.get<void>(this.apiUrl+'/me/admin');
  }
}
