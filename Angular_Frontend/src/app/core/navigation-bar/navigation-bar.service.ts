import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ShopComponent } from 'src/app/shop/shop.component';

@Injectable({
  providedIn: 'root'
})
export class NavigationBarService {

  private searchingSource = new BehaviorSubject<string>('');
  newSearch = this.searchingSource.asObservable();

  constructor() { }

  onNewSearch(searching: string){
    this.searchingSource.next(searching);
  }
  
}
