import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss']
})
export class ProductInfoComponent implements OnInit{
  product?: Product;

  constructor( private activeRoute: ActivatedRoute, private shopService: ShopService) {}

  ngOnInit(): void {

    this.loadProductInfo();

  }


  loadProductInfo(){
    const productId = this.activeRoute.snapshot.paramMap.get('id');

    if(productId){
      this.shopService.getProductInfo( + productId ).subscribe({
        next: product => this.product = product,
        error: error => console.log(error),
        
      })
    }
  }
}
