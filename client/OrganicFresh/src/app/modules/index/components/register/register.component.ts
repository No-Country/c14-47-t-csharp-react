import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  constructor(fb:FormBuilder){

    this.form = fb.group({
      username:['',[Validators.required]],
      email: ['',[Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });

  }


  form:FormGroup;

}
