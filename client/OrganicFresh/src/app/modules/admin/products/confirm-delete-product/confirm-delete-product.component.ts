import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-confirm-delete-product',
  templateUrl: './confirm-delete-product.component.html',
  styleUrls: ['./confirm-delete-product.component.scss']
})
export class ConfirmDeleteProductComponent {
  constructor(private dialogRef:MatDialogRef<ConfirmDeleteProductComponent>, @Inject(MAT_DIALOG_DATA)private idProduct:number, private productService:ProductService){}


  close():void{
    this.dialogRef.close();
  }

  delete():void{
    this.productService.delete(this.idProduct).subscribe({
      next:()=>{
        this.dialogRef.close(true);
      },
      error:(err)=>{
      }
    });
  }
}
