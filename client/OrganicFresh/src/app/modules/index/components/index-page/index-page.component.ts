import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, take } from 'rxjs';
import { CartItem } from 'src/app/interfaces/cartItem';
import { Category } from 'src/app/interfaces/category';
import { Credential } from 'src/app/interfaces/credential';
import { Product } from 'src/app/interfaces/product';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-index-page',
  templateUrl: './index-page.component.html',
  styleUrls: ['./index-page.component.scss']
})
export class IndexPageComponent implements OnInit{


  credential$:Observable<Credential|null>;

  constructor(private authService:AuthService, private productService:ProductService, private categoryService:CategoryService, private router:Router, private cartService:CartService){
    this.credential$ = authService.getCredential;
    this.showShoppingCart$ = cartService.show;
  }
  ngOnInit(): void {
    this.getCategories();
    this.getProducts();
  }

  logout():void{
    this.authService.logout();
  }
  listAllProducts:Product[]=[];
  listProducts:Product[] = [];
  listCategories:Category[] = [];
  idSelectedCategory:number | null = null;
  showShoppingCart$:Observable<boolean>;

  getCategories():void{
    this.categoryService.getAll().subscribe({
      next:(res)=>{
        this.listCategories = res;
      }
    });
  }

  getProducts():void{
    this.productService.getAll().subscribe({
      next:(res)=>{
        this.listAllProducts = res.products.filter(p => p.active === true);

        this.listProducts = this.listAllProducts;
      }
    });
  }

  filter(idCategory:number):void{
    this.idSelectedCategory = idCategory;
    this.listProducts = this.listAllProducts.filter( product => product.categoryId === this.idSelectedCategory);
  }

  addToCart(product:Product):void{
    this.credential$.pipe(take(1)).subscribe({
      next:(res)=>{
        if(res!==null){
          if(res.isAdmin === false){
            const cartItem:CartItem = {
              product:product,
              productId: product.id!,
              quantity:1,
              subtotal:product.price*1
            }
            this.cartService.addItem(cartItem);
            this.cartService.showCart();

          }
        }
        else{
          this.router.navigate(['index/login']);
        }
      }
    })
  }

  showCart():void{
    this.cartService.showCart();
  }

}
