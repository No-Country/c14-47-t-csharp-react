import { Component, HostListener } from '@angular/core';
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

  isLargeScreen:boolean;

  constructor( private productService:ProductService, private matDialog:MatDialog){
    this.isLargeScreen= true;
    this.getAll();

  }


  @HostListener ('window:resize', ['$event'])
  onResize(event: Event): void {
    this.isLargeScreen = window.innerWidth >= 768; //
  }

  getAll():void{
    this.productService.getAll().subscribe( {next:(res) => {
      this.listProducts =  res.products.filter(p => p.active === true);
    }} );
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

  update(product:Product):void{
    this.matDialog.open(CreateUpdateProductComponent,{data:product}).afterClosed().subscribe({
      next:(res)=>{
        if(res === true){
          this.getAll();
        }
      }
    });
  }

}
