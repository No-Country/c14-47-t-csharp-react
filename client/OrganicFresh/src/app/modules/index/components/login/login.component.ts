import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { LoginRequest } from 'src/app/interfaces/loginRequest';
import { AlertRegisterComponent } from 'src/app/modules/shared/components/alert-register/alert-register.component';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(fb:FormBuilder, private authService:AuthService, private dialog:MatDialog){

    this.form = fb.group({
      email: ['',[Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });

  }


  form:FormGroup;
  mostrarPassword:boolean = false;
  login():void{
    
    const loginRequest:LoginRequest = {
      email: this.form.value.email,
      password: this.form.value.password
    }
    
    this.authService.login(loginRequest).subscribe({
      next:(res)=>{
        console.log(res);
        this.dialog.open(AlertRegisterComponent, {data:{login:true, title:'Login Successful!', text:'', exito:true}});
      },
      error:()=>{
        this.dialog.open(AlertRegisterComponent, {data:{login:true, title:'Invalid credentials', text:'', exito:false}});
      }
    });

  }

  
}
