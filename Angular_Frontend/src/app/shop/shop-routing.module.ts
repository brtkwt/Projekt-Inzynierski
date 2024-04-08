import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductInfoComponent } from './product-info/product-info.component';
import { RouterModule, Routes } from '@angular/router';

const routing: Routes = [

     // Sklep
  {path: "", component:ShopComponent},
    // produkt
  {path: ":id", component:ProductInfoComponent, data: {breadcrumb: {alias: 'bcProductName'}}}
  
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routing),
  ],

  exports:[
    RouterModule,
  ]
})
export class ShopRoutingModule { }
