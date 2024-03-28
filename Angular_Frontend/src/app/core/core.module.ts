import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ShopComponent } from '../shop/shop.component';


@NgModule({
  declarations: [
    NavigationBarComponent,
  ],
  imports: [
    CommonModule,
  ],
  exports: [
    NavigationBarComponent,
  ]
})
export class CoreModule { }
