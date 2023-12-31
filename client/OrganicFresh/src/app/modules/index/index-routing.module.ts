import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index.component';
import { IndexPageComponent } from './components/index-page/index-page.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { loginRegisterGuard } from 'src/app/guards/login-register.guard';

const routes: Routes = [{
  path:'', component:IndexComponent, 
      children:[
        {
          path:'', component:IndexPageComponent
        },
        {
          path:'register', component:RegisterComponent, canActivate:[loginRegisterGuard]
        },
        {
          path:'login', component:LoginComponent, canActivate:[loginRegisterGuard]
        }
      ],
        }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IndexRoutingModule { }
