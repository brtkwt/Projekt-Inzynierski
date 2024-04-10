import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './cart.component';
import { CartRoutingModule } from './cart-routing.module';
import { RouterModule } from '@angular/router';
import { CartService } from './cart.service';


@NgModule({
  declarations: [
    CartComponent,
  ],
  imports: [
    CommonModule,
    CartRoutingModule,
    RouterModule
  ],
  exports:[
    CartComponent
  ]
})

export class CartModule { }
