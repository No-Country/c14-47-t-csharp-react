import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
import { Product } from 'src/app/interfaces/product';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';
import { ConfirmDeleteProductComponent } from '../confirm-delete-product/confirm-delete-product.component';

@Component({
  selector: 'app-create-update-product',
  templateUrl: './create-update-product.component.html',
  styleUrls: ['./create-update-product.component.scss']
})
export class CreateUpdateProductComponent implements OnInit {

  form:FormGroup;
  listCategories:Category[]= [];
  weightUnit:string = 'Oz';
  imagenSeleccionada: string | ArrayBuffer | null = null;
  editProduct:boolean = false;
  imagenModificada = false;

  constructor(fb:FormBuilder, private productService:ProductService, private categoryService:CategoryService, private matDialogRef:MatDialogRef<CreateUpdateProductComponent>,
    @Inject(MAT_DIALOG_DATA)public dataProduct:Product, private matDialog:MatDialog){

    this.form = fb.group({
      name:['',[Validators.required]],
      categoryId:['',[Validators.required]],
      price:['',[Validators.required, Validators.min(0)]],
      stock:['', [Validators.required, Validators.min(0)]],
      image:[null,Validators.required]
    });

    if(this.dataProduct!==null){
      this.editProduct = true;
    }

    this.getCategories();

  }
  ngOnInit(): void {
    if(this.editProduct){
      this.form.patchValue({
        name:this.dataProduct.name,
        categoryId: this.dataProduct.categoryId,
        price:this.dataProduct.price,
        stock: this.dataProduct.stock,
        image: this.dataProduct.imageUrl
      });
      this.weightUnit = this.dataProduct.weightUnit;
      this.imagenSeleccionada = this.dataProduct.imageUrl;
    }
  }


  create():void{
   
    const formData = new FormData();
    formData.append('name', this.form.value.name);
    formData.append('categoryId', this.form.value.categoryId);
    formData.append('price', this.form.value.price);
    formData.append('stock', this.form.value.stock);
    formData.append('weightUnit', this.weightUnit);
    formData.append('image',this.form.value.image);
    this.productService.create(formData).subscribe({
      next:()=>{
        this.matDialogRef.close(true);
      },
      error:()=>{
      }
    });

  }

  update():void{

    const formData = new FormData();

    formData.append('name', this.form.value.name);
    formData.append('categoryId', this.form.value.categoryId);
    formData.append('price', this.form.value.price);
    formData.append('stock', this.form.value.stock);
    formData.append('weightUnit', this.weightUnit);
    if(this.imagenModificada){
      formData.append('image',this.form.value.image);
    }


   this.productService.update(this.dataProduct.id!, formData).subscribe({
    next:()=>{
      this.matDialogRef.close(true);
    },
    error:()=>{
      
    }
   });
  }

  delete():void{
    this.matDialog.open(ConfirmDeleteProductComponent,{data:this.dataProduct.id}).afterClosed().subscribe({
      next:(res)=>{
        if(res === true){
          this.matDialogRef.close(true);
        }
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


  cargarImagen(event: any) {
    const archivo:File = event.target.files[0];
    
    if (archivo) {
      this.form.patchValue({
        image:archivo
      });

      if(this.editProduct){
        this.imagenModificada = true;
      }
      // Lee el archivo y muestra la imagen
      const lector = new FileReader();
      lector.onload = (e: any) => {
        this.imagenSeleccionada = e.target.result;
      };
      lector.readAsDataURL(archivo);
    }
    event.target.value = '';
  }

  deleteImage():void{
    this.imagenSeleccionada = null;
    this.form.patchValue({
      image:''
    });
  }


}
