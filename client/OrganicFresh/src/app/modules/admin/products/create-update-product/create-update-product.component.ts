import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
import { Product } from 'src/app/interfaces/product';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-create-update-product',
  templateUrl: './create-update-product.component.html',
  styleUrls: ['./create-update-product.component.scss']
})
export class CreateUpdateProductComponent implements OnInit {

  form:FormGroup;
  listCategories:Category[]= [];
  weightUnit:string = 'Oz';

  constructor(fb:FormBuilder, private productService:ProductService, private categoryService:CategoryService, private matDialogRef:MatDialogRef<CreateUpdateProductComponent>){

    this.form = fb.group({
      name:['',[Validators.required]],
      categoryId:['',[Validators.required]],
      price:['',[Validators.required, Validators.min(0)]],
      stock:['', [Validators.required, Validators.min(0)]]
    });

  }
  ngOnInit(): void {
   this.getCategories();
  }


  create():void{
   
    const formData = new FormData();
    formData.append('name', this.form.value.name);
    formData.append('categoryId', this.form.value.categoryId);
    formData.append('price', this.form.value.price);
    formData.append('stock', this.form.value.stock);
    formData.append('weightUnit', this.weightUnit);
    this.productService.create(formData).subscribe({
      next:()=>{
        this.matDialogRef.close(true);
      },
      error:(err)=>{
        console.log(err);
      }
    });

  }

  getCategories():void{
    this.categoryService.getAll().subscribe({
      next:(res)=>{
        this.listCategories = res;
      }
    });
  }

  close():void{
    this.matDialogRef.close();
  }


}
