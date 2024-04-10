import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductCardComponent } from './product-card/product-card.component';
import { SharedModule } from '../shared/shared.module';
import { ProductInfoComponent } from './product-info/product-info.component';
import { RouterModule } from '@angular/router';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { ShopRoutingModule } from './shop-routing.module';


@NgModule({
  declarations: [
    ShopComponent,
    ProductCardComponent,
    ProductInfoComponent,
  ],
  imports: [
    RouterModule,
    CommonModule,
    SharedModule,
    BreadcrumbModule,
  ],
  exports: [
    ShopComponent,
    ShopRoutingModule,
  ]
})
export class ShopModule { }
