import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { CartService } from 'src/app/cart/cart.service';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss']
})
export class ProductInfoComponent implements OnInit{
  product?: Product;

  constructor( private activeRoute: ActivatedRoute, private shopService: ShopService, private breadCrumbService: BreadcrumbService,
    private cartService: CartService
  ) {
    this.breadCrumbService.set("@bcProductName", " ")
  }

  ngOnInit(): void {

    this.loadProductInfo();

  }

  addProduct(product: Product, productNumber: string){

    if(parseInt(productNumber) !== 0){
      this.cartService.addProductToCartFromInfo(product, parseInt(productNumber));
    }
    else{
      this.cartService.addProductToCartFromInfo(product, 1);
    }
  }

  loadProductInfo(){
    const productId = this.activeRoute.snapshot.paramMap.get('id');

    if(productId){
      this.shopService.getProductInfo( + productId ).subscribe({
        next: product => {
          this.product = product,
          this.breadCrumbService.set("@bcProductName", product.name)
        },
        error: error => console.log(error),
        
      })
    }
  }
}
