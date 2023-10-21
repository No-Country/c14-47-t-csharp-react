import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Product } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';
import { CreateUpdateProductComponent } from './create-update-product/create-update-product.component';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent {

  constructor( private productService:ProductService, private matDialog:MatDialog){

    this.getAll();
  }

  getAll():void{
    this.productService.getAll().subscribe( {next:(res) => {this.listProducts =  res.products}  } );
  }

  listProducts:Product[] = [];

  newProduct():void{
    this.matDialog.open(CreateUpdateProductComponent).afterClosed().subscribe({
      next:(res)=>{
        if(res === true){
          this.getAll();
        }
      }
    });
  }

}
