import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.scss']
})
export class SuccessComponent implements OnInit {

  idOrder:number | null = null;

  constructor(private activatedRoute:ActivatedRoute, private router:Router, private cartService:CartService ){
    this.activatedRoute.params.subscribe({
      next:(params)=>{
        this.idOrder = params['order'];
      }
    })
  }
  ngOnInit(): void {
    if(!this.idOrder){
      this.router.navigate(['index']);
    }
    this.cartService.deleteCart();
  }

  goTo(route:string):void{
    this.router.navigate([route]);
    
  }


}
