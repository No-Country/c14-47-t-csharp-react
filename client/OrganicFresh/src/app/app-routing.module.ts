import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path:'index', loadChildren: ()=> import('./modules/index/index.module').then(m => m.IndexModule)
  },
  {
    path:'admin', loadChildren:()=> import('./modules/admin/admin.module').then(m => m.AdminModule)
  },
  {
    path:'**',redirectTo:'index'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
