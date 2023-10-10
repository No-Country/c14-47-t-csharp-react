import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IndexRoutingModule } from './index-routing.module';
import { IndexComponent } from './index.component';
import { IndexPageComponent } from './components/index-page/index-page.component';


@NgModule({
  declarations: [
    IndexComponent,
    IndexPageComponent
  ],
  imports: [
    CommonModule,
    IndexRoutingModule
  ]
})
export class IndexModule { }
