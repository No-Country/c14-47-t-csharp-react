import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
import { CategoryService } from 'src/app/services/category.service';
import { CreateUpdateCategoryComponent } from './modals/create-update-category/create-update-category.component';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit{


  listCategories:Category[]=[];

  constructor(private categoryService:CategoryService, private dialog:MatDialog){}
  ngOnInit(): void {

   this.getCategories();
  }

  getCategories():void{
    this.categoryService.getAll().subscribe({
      next:(res)=>{
        this.listCategories = res;
      }
    });
  }

  createCategory():void{
    this.dialog.open(CreateUpdateCategoryComponent).afterClosed().subscribe({
      next:(res)=>{
        if(res === true) this.getCategories();
      }
    });
  }

  updateCategory(category:Category):void{
    this.dialog.open(CreateUpdateCategoryComponent,{data:category}).afterClosed().subscribe({
      next:(res)=>{
        if(res===true) this.getCategories();
      }
    });
  }


}
