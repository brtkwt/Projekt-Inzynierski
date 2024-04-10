import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './cart.component';
import { RouterModule, Routes } from '@angular/router';

const routing: Routes = [
  
  // koszyk
  {path: "", component:CartComponent},
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routing),
  ],

  exports:[
    RouterModule,
  ]
})

export class CartRoutingModule { }
