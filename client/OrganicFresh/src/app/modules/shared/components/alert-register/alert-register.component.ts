import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-alert-register',
  templateUrl: './alert-register.component.html',
  styleUrls: ['./alert-register.component.scss']
})
export class AlertRegisterComponent {

  constructor(@Inject(MAT_DIALOG_DATA)public exito:boolean,private matDialogRef:MatDialogRef<AlertRegisterComponent>, private router:Router){

  }

  goToLogin():void{
    //this.router.navigate(['index/login']);
    this.matDialogRef.close();
  }
  cerrarDialog():void{
    this.matDialogRef.close();
  }
}
