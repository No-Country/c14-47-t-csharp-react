import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
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
  imagenSeleccionada: string | ArrayBuffer | null = null;

  constructor(fb:FormBuilder, private productService:ProductService, private categoryService:CategoryService, private matDialogRef:MatDialogRef<CreateUpdateProductComponent>){

    this.form = fb.group({
      name:['',[Validators.required]],
      categoryId:['',[Validators.required]],
      price:['',[Validators.required, Validators.min(0)]],
      stock:['', [Validators.required, Validators.min(0)]],
      image:[null,Validators.required]
    });
    this.getCategories();

  }
  ngOnInit(): void {
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
