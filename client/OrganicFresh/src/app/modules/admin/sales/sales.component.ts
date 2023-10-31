import { Component } from '@angular/core';
import { Sale } from 'src/app/interfaces/sale';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.scss']
})
export class SalesComponent {


  sales:Sale[]= [];
  constructor(private authService:AuthService){

    this.authService.salesAdmin().subscribe({
      next:(res)=>{
        this.sales = res;
        this.sales.forEach(sale =>{
          sale.total = 0;
          sale.id = sale.checkoutDetails[0].saleId;
          sale.checkoutDetails.forEach(detail=>{
            sale.total += detail.total
          })
        });
      }
    });

  }
  
}
