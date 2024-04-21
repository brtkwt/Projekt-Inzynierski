import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { OrderComponent } from './order.component';
import { OrderInfoComponent } from './order-info/order-info.component';

const routing: Routes = [
  
  // zamówienia wszystkie lista
  {path: "", component: OrderComponent},

  // zamówienie jedno po id
  {path: ":id", component: OrderInfoComponent}
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routing)
  ],

  exports:[
    RouterModule,
  ]
})

export class OrderRoutingModule { }
