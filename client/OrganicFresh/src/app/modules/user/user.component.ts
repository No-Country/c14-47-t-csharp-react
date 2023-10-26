import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent {
  
  showShoppingCart$:Observable<boolean>;
  constructor(private cartService:CartService, private authService:AuthService, private router:Router){
    this.showShoppingCart$ = this.cartService.show;
    this.authService.me().subscribe({
      next:(res)=>{
        this.router.navigate(['user/orders']);
      },
      error:(err:HttpErrorResponse)=> {
        if(err.status !== 401) this.router.navigate(['index']);
      }
    });
  }

  showCart():void{
    this.cartService.showCart();
  }

  logout():void{
    this.authService.logout();
  }

  goToIndex():void{
    this.cartService.hiddenCart();
    this.router.navigate(['index']);
  }
}
