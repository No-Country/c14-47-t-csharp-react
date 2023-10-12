import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRequest } from 'src/app/interfaces/loginRequest';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(fb:FormBuilder, private authService:AuthService){

    this.form = fb.group({
      email: ['',[Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });

  }


  form:FormGroup;

  login():void{
    
    const loginRequest:LoginRequest = {
      email: this.form.value.email,
      password: this.form.value.password
    }
    
    this.authService.login(loginRequest).subscribe({
      next:(res)=>{
        console.log(res.token);
        console.log("Inicio de sesion exitoso");
      },
      error:(err)=>{
        console.log(err);
        console.log("Error al iniciar sesion");
      }
    });

  }

  
}
