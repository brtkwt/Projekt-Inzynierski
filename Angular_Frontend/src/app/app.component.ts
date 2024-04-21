import { Component, OnInit } from '@angular/core';
import { CartService } from './cart/cart.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Backcountry Gear';

  constructor(private acService: AccountService, private cartService: CartService) { }

  ngOnInit(): void {
    this.loadingCartFromBrowserId();

    this.loadingUserFromToken();

  }


  loadingUserFromToken(){
    const browserToken = localStorage.getItem("token");

    if(browserToken != null){
      this.acService.logUserFromBrowserStorageToken(browserToken).subscribe();
    }
  }

  loadingCartFromBrowserId(){
    const cartId = localStorage.getItem("cartId");

    if(cartId){
      this.cartService.getCartByIdFromApi(cartId);
    }
    
  }

}
