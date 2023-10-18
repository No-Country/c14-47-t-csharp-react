import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {
  constructor(private authService:AuthService){

}

logout():void{
  this.authService.logout();
}

openMenu:boolean = false;

}
