import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, finalize, take, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Credential } from '../interfaces/credential';
import { SpinnerService } from '../services/spinner.service';
import { Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService:AuthService, private spinnerService:SpinnerService) {}

  credential:Credential|null = null;

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    this.spinnerService.mostrarSpinner();

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

    return next.handle(request).pipe(catchError((err:HttpErrorResponse)=>{

      if(err.status === 401){
        this.authService.logout('/index/login');
      }
      
      return throwError(()=>err);
    }),finalize(()=>{
      this.spinnerService.ocultarSpinner();
    }));
  }
}
