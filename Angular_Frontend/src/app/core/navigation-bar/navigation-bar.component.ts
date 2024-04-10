import { Component, ElementRef, ViewChild } from '@angular/core';
import { NavigationBarService } from './navigation-bar.service';
import { Router } from '@angular/router';
import { CartService } from 'src/app/cart/cart.service';
import { CartItem } from 'src/app/shared/models/cartItem';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.scss']
})
export class NavigationBarComponent {
  @ViewChild('searching') searchWord?: ElementRef;

  constructor(private navBarService: NavigationBarService, private ruter: Router, public cartService: CartService) {}

  newInput(){
    this.navBarService.onNewSearch(this.searchWord?.nativeElement.value);
    this.ruter.navigate(['/sklep']);
  }
  
  resetInput(){
    this.navBarService.onNewSearch("");
  }


  countNumberOfProducts(items: CartItem[]) : number{
    const number: number = items.reduce((counter, product) => counter + product.productNumber, 0);

    return number;
    
  }
    
}
