import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/interfaces/loginRequest';
import { AlertRegisterComponent } from 'src/app/modules/shared/components/alert-register/alert-register.component';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(fb:FormBuilder, private authService:AuthService, private dialog:MatDialog, private router:Router){

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
        if(res.isAdmin){
          this.router.navigate(['admin']);
        }
        else{

          this.router.navigate(['index']);
        }
      },
      error:()=>{
        this.dialog.open(AlertRegisterComponent, {data:{login:true, title:'Invalid credentials', text:'', exito:false}});
      }
    });

  }

  
}
