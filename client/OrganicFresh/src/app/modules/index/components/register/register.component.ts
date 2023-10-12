import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterRequest } from 'src/app/interfaces/registerRequest';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  constructor(fb:FormBuilder, private authService:AuthService){

    this.form = fb.group({
      name:['',[Validators.required]],
      email: ['',[Validators.required, Validators.email]],
      password: ['', [this.customPasswordValidator]],
      confirmPassword:['',[this.customPasswordValidator]]
    });

  }


  form:FormGroup;

  register():void{
    const userRegister:RegisterRequest={
      name: this.form.value.name,
      email: this.form.value.email,
      password: this.form.value.password

    }

    this.authService.registerApi(userRegister).subscribe({
      next:(res)=>{
        //Agregar redirección a login o dashboard
        console.log(res.token);
        console.log("Registro exitoso");
      },
      error:(err)=>{
        //Mostrar Alerta
        console.log("Error");
        console.log(err);
      }
    });

  }

  customPasswordValidator(control: AbstractControl): { [key: string]: any } | null {
    const value = control.value;
    if(!value) {
      return {required:true} //Error si el campo esta vacio
    }
  
    if(!/^\S*$/.test(value)){
      return {espacioBlanco:true} //Error si tiene espacios en blanco
    }
  
    if (!/[a-z]/.test(value)) {
      return { lowercase: true }; // Error si no hay minúsculas
    }
  
    if (!/[A-Z]/.test(value)) {
      return { uppercase: true }; // Error si no hay mayúsculas
    }
  
    if (!/\d/.test(value)) {
      return { digit: true }; // Error si no hay números
    }
  
    if (value.length < 6) {
      return { minlength: true }; // Error si la longitud es menor a 8 caracteres
    }
  
    if (value.length > 20) {
      return { maxlength: true }; // Error si la longitud es mayor a 15 caracteres
    }
    
    return null; // La contraseña cumple con todas las validaciones
  }

  passwordValidator():void{

    const password = this.form.get('password');
    const confirmPassword = this.form.get('confirmPassword'); 

      
      if(password?.errors && confirmPassword?.errors && (!password.errors.hasOwnProperty('passwordMismatch') ||  !confirmPassword.errors.hasOwnProperty('passwordMismatch'))){
        return;
      }
  
      if(password && confirmPassword && !password.pristine && !confirmPassword.pristine){
        if(password.value != confirmPassword.value){
         
          password?.setErrors({passwordMismatch: true});
          confirmPassword?.setErrors({passwordMismatch: true});
        }
        else{
          password.setErrors(null);
          confirmPassword.setErrors(null);
        }
      }
    



  }
 
}


