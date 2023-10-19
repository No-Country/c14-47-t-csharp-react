import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
import { AlertRegisterComponent } from 'src/app/modules/shared/components/alert-register/alert-register.component';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-create-update-category',
  templateUrl: './create-update-category.component.html',
  styleUrls: ['./create-update-category.component.scss']
})
export class CreateUpdateCategoryComponent {

    constructor(private dialogRef:MatDialogRef<CreateUpdateCategoryComponent>, private categoryService:CategoryService, private dialog:MatDialog){
      this.name = new FormControl('',[Validators.required, Validators.maxLength(30)]);
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


}
