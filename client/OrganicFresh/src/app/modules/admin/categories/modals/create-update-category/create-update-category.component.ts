import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
import { AlertRegisterComponent } from 'src/app/modules/shared/components/alert-register/alert-register.component';
import { CategoryService } from 'src/app/services/category.service';
import { ConfirmDeleteCategoryComponent } from '../confirm-delete-category/confirm-delete-category.component';

@Component({
  selector: 'app-create-update-category',
  templateUrl: './create-update-category.component.html',
  styleUrls: ['./create-update-category.component.scss']
})
export class CreateUpdateCategoryComponent implements OnInit{

    constructor(fb:FormBuilder,private dialogRef:MatDialogRef<CreateUpdateCategoryComponent>, private categoryService:CategoryService, private dialog:MatDialog,
      @Inject(MAT_DIALOG_DATA)public dataCategory:Category){
      this.name = new FormControl('',[Validators.required, Validators.maxLength(30)]);
      this.image = new FormControl(null, Validators.required);
      this.form = fb.group({
        name:['',Validators.required],
        image:[null, Validators.required]
      });
    }
  ngOnInit(): void {
    if(this.dataCategory){
      this.name.patchValue(this.dataCategory.name);
      this.imagenSeleccionada = this.dataCategory.imageUrl;


    }

  }
    form:FormGroup;
    name:FormControl;
    image:FormControl;
    imagenSeleccionada: string | ArrayBuffer | null = null;
    imagenModificada = false;

  close():void{
    this.dialogRef.close();
  }

  create():void{
    
    const formData = new FormData();
    formData.append('name', this.name.value);
    formData.append('image', this.image.value);

    this.categoryService.create(formData).subscribe({
      next:()=>{
        this.dialogRef.close(true);
      },
      error:()=>{
        this.dialog.open(AlertRegisterComponent,{data:{login:false, title:'Error', text:'An error occurred.', exito:false}})
      }
    });

  }

  update():void{
    const formData = new FormData();
    formData.append('name', this.name.value);
    if(this.imagenModificada === true){

      formData.append('image', this.image.value);
    }

    this.categoryService.update(this.dataCategory.id!, formData).subscribe({
      next:()=>{
        this.dialogRef.close(true);
      },
      error:()=>{

      }
    });
  }

  delete():void{
    this.dialog.open(ConfirmDeleteCategoryComponent,{data:this.dataCategory.id}).afterClosed().subscribe({
      next:(res)=>{
        if(res===true)this.dialogRef.close(true);
      }
    });
  }

  cargarImagen(event: any) {
    const archivo:File = event.target.files[0];
    
    if (archivo) {
      this.image.setValue(archivo);
      
      if(this.dataCategory !== null){
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
    this.image.setValue(null);
  }
}
