import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Sale } from 'src/app/interfaces/sale';

@Component({
  selector: 'app-detail-order',
  templateUrl: './detail-order.component.html',
  styleUrls: ['./detail-order.component.scss']
})
export class DetailOrderComponent {


  constructor(private matDialogRef:MatDialogRef<DetailOrderComponent>, @Inject(MAT_DIALOG_DATA)public sale:Sale){
  }



  close():void{
    this.matDialogRef.close();
  }

}
