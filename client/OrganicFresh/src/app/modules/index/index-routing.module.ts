import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index.component';
import { IndexPageComponent } from './components/index-page/index-page.component';

const routes: Routes = [{
  path:'', component:IndexComponent, 
      children:[
        {
          path:'', component:IndexPageComponent
        }
      ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IndexRoutingModule { }
