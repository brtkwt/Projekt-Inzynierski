import { Component, OnInit } from '@angular/core';
import { CartService } from './cart/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Backcountry Gear';

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    const cartId = localStorage.getItem("cartId");

    if(cartId){
      this.cartService.getCartByIdFromApi(cartId);
    }

  }

}
