import { Component, Input } from '@angular/core';
import { CartService } from 'src/app/cart/cart.service';
import { Product } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent {
  @Input() product?: Product

  constructor(private cartService: CartService) {}


  addProductToCart(){
    if(this.product != null){
      this.cartService.addProductToCart(this.product);
    }

  }
  
}
