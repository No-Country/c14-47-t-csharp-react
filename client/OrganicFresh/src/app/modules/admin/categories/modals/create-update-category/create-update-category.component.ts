import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
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

    constructor(private dialogRef:MatDialogRef<CreateUpdateCategoryComponent>, private categoryService:CategoryService, private dialog:MatDialog,
      @Inject(MAT_DIALOG_DATA)public dataCategory:Category){
      this.name = new FormControl('',[Validators.required, Validators.maxLength(30)]);
    }
  ngOnInit(): void {

    if(this.dataCategory){
      this.name.patchValue(this.dataCategory.name);
    }

  }

    name:FormControl;

  close():void{
    this.dialogRef.close();
  }

  create():void{
    
    const category:Category = {
      name:this.name.value
    }

    this.categoryService.create(category).subscribe({
      next:()=>{
        this.dialogRef.close(true);
      },
      error:()=>{
        this.dialog.open(AlertRegisterComponent,{data:{login:false, title:'Error', text:'An error occurred.', exito:false}})
      }
    });

  }

  update():void{

    const category:Category = {
      id:this.dataCategory.id,
      name:this.name.value
    }

    this.categoryService.update(category).subscribe({
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
}
