import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FinalizationComponent } from './finalization.component';
import { RouterModule, Routes } from '@angular/router';

const routing: Routes =[
  {path: "", component: FinalizationComponent}

]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routing)
  ],

  exports:[
    RouterModule
  ]

})
export class FinalizationRoutingModule { }
