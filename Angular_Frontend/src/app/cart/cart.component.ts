import { Component } from '@angular/core';
import { CartService } from './cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent {

  constructor(public cartService: CartService) {}

  updateNumberOfProducts(productId: number, newNumber: string){

    if(parseInt(newNumber) !== 0){
      this.cartService.updateProductNumberInCart(productId, parseInt(newNumber));
    }
    else{
      this.cartService.updateProductNumberInCart(productId, 1);
    }
  }

  deleteProducts(id: number, producNumber: number){
    this.cartService.deleteProducts(id, producNumber);
  }

}
