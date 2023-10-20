import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Credential } from 'src/app/interfaces/credential';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-index-page',
  templateUrl: './index-page.component.html',
  styleUrls: ['./index-page.component.scss']
})
export class IndexPageComponent {


  credential$:Observable<Credential|null>;

  constructor(private authService:AuthService){
    this.credential$ = authService.getCredential;
  }

  logout():void{
    this.authService.logout();
  }

}
