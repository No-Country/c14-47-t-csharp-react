import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-delete-category',
  templateUrl: './confirm-delete-category.component.html',
  styleUrls: ['./confirm-delete-category.component.scss']
})
export class ConfirmDeleteCategoryComponent {

  constructor(private dialogRef:MatDialogRef<ConfirmDeleteCategoryComponent>){}


  close():void{
    this.dialogRef.close();
  }

}
