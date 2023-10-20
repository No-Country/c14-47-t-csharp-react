import { Component } from '@angular/core';
import { Product } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent {

  constructor( private productService:ProductService){

    this.getAll();
  }

  getAll():void{
    this.productService.getAll().subscribe( {next:(res) => {this.listProducts =  res.products}  } );
  }

  listProducts:Product[] = [];

}
