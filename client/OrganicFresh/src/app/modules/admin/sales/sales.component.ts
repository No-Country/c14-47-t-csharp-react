import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Sale } from 'src/app/interfaces/sale';
import { AdminService } from 'src/app/services/admin.service';
import { DetailOrderComponent } from '../../shared/components/detail-order/detail-order.component';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.scss']
})
export class SalesComponent {


  sales:Sale[]= [];
  constructor(adminService:AdminService, private matDialog:MatDialog){

    adminService.getSales().subscribe({
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

  showDetails(sale:Sale):void{
    this.matDialog.open(DetailOrderComponent, {data:sale});
  }
  
}
