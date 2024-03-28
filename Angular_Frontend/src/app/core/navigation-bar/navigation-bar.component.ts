import { Component, ElementRef, ViewChild } from '@angular/core';
import { NavigationBarService } from './navigation-bar.service';
import { ShopComponent } from 'src/app/shop/shop.component';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.scss']
})
export class NavigationBarComponent {
  @ViewChild('searching') searchWord?: ElementRef;

  constructor(private navBarService: NavigationBarService) {}

  newInput(){
    this.navBarService.onNewSearch(this.searchWord?.nativeElement.value);
  }
  
  
}
