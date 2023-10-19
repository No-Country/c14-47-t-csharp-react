import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-confirm-delete-category',
  templateUrl: './confirm-delete-category.component.html',
  styleUrls: ['./confirm-delete-category.component.scss']
})
export class ConfirmDeleteCategoryComponent {

  constructor(private dialogRef:MatDialogRef<ConfirmDeleteCategoryComponent>, private categoryService:CategoryService,@Inject(MAT_DIALOG_DATA)private idCategory:number){}


  close():void{
    this.dialogRef.close();
  }

  delete():void{
    this.categoryService.delete(this.idCategory).subscribe({
      next:()=>{
        this.dialogRef.close(true);
      },
      error:()=>{

      }
    });
  }

}
