import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {
  constructor(private authService:AuthService, private router:Router){

}

logout():void{
  this.authService.logout();
}

openMenu:boolean = false;

redirectTo(route:string):void{
  this.router.navigate([route]);
  this.openMenu = false;
}

}
