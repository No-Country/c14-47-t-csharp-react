import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Credential } from '../interfaces/credential';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService:AuthService) {}

  credential:Credential|null = null;

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    this.authService.getCredential.pipe(take(1)).subscribe({
      next:(res)=>{
        this.credential = res;
      }
    });

    
    if(this.credential !== null){
      request = request.clone({
        setHeaders:{
          Authorization : `Bearer ${this.credential.jwt}`
        }
      });

      
    }

    return next.handle(request);
  }
}
