import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/interfaces/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit{


  listCategories:Category[]=[];

  constructor(private categoryService:CategoryService){}
  ngOnInit(): void {

    this.categoryService.getAll().subscribe({
      next:(res)=>{
        this.listCategories = res;
      }
    });
  }



}
