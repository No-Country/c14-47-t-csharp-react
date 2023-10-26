import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { adminGuard } from './guards/admin.guard';

const routes: Routes = [
  {
    path:'index', loadChildren: ()=> import('./modules/index/index.module').then(m => m.IndexModule)
  },
  {
    path:'admin', loadChildren:()=> import('./modules/admin/admin.module').then(m => m.AdminModule), canActivate:[adminGuard]
  },
  {
    path:'user', loadChildren:()=> import('./modules/user/user.module').then(m => m.UserModule)
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
