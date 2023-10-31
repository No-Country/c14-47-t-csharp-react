import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { User } from 'src/app/interfaces/user';
import { AuthService } from 'src/app/services/auth.service';
import { DetailOrderComponent } from '../../shared/components/detail-order/detail-order.component';
import { Sale } from 'src/app/interfaces/sale';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit{


  constructor(authService:AuthService, private dialog:MatDialog){
    this.user$ = authService.getUser;
  }
  ngOnInit(): void {
    this.user$.subscribe({
      next:(res)=>{
        console.log(res);
        res?.sales.forEach(sale => {
          sale.total = 0;
          sale.id = sale.checkoutDetails[0].saleId;
          sale.checkoutDetails.forEach(detail =>{
              sale.total += detail.total;
          });
        });
      }
    });
  }



  user$:Observable<User|null>;


  showDetails(sale:Sale):void{
    this.dialog.open(DetailOrderComponent,{data:sale});
  }
}
