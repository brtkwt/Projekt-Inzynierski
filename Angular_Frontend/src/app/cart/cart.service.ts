import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Cart, CartSums } from '../shared/models/cart';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/product';
import { CartItem } from '../shared/models/cartItem';

@Injectable({
  providedIn: 'root'
})

export class CartService {
  basePath = 'https://localhost:5002/api/';

  private cartSource = new BehaviorSubject<Cart | null>(null);
  cartSource$ = this.cartSource.asObservable();

  private cartSumCostsSource = new BehaviorSubject<CartSums | null>(null);
  cartSumCostsSource$ = this.cartSumCostsSource.asObservable(); 

  constructor(private http: HttpClient) {}

  private countSums(){
    const cart = this.getCart();

    if(cart === null){
      return;
    }
    else{
      const shippingCost = 0;
      const sumCost = cart.items.reduce((p,c) => c.productNumber * c.cost + p, 0);

      const fullCost = sumCost + shippingCost;

      this.cartSumCostsSource.next({shippingCost, sumCost, fullCost});
    }
  }

  private mappingProductToCartItem(product : Product) : CartItem{
    
    return{
      id: product.id,
      productNumber: 0,
      productName: product.name,
      cost: product.cost,
      pictureUrl: product.imageAdress,
      category: product.categoryName,
      company: product.companyName
    }
  }

  private CreateNewCart(): Cart {
    const newCart = new Cart();
    localStorage.setItem("cartId", newCart.id)

    return newCart;
  }

  private addProductToCartOrChangeNumber(items: CartItem[], newCartItem: CartItem, numberOfproducts: number): CartItem[] {
    
    const item = items.find(x => x.id === newCartItem.id);

    if(item != null){
      item.productNumber = item.productNumber + 1;
    }
    else{
      newCartItem.productNumber = numberOfproducts;
      items.push(newCartItem);
    }

    return items;
  }

  // zwraca koszyk albo null
  getCart(){
    return this.cartSource.value;
  }

  addProductToCart(product: Product | CartItem, numberOfproducts = 1){

    if("categoryName" in product){
      product = this.mappingProductToCartItem(product)
    }

    const cart = this.getCart() ?? this.CreateNewCart();
    cart.items = this.addProductToCartOrChangeNumber(cart.items, product, numberOfproducts);

    this.setCartInApi(cart);
  }

  private addProductToCartOrChangeNumber2(items: CartItem[], newCartItem: CartItem, numberOfproducts: number): CartItem[] {
    
    const item = items.find(x => x.id === newCartItem.id);

    if(item != null){
      item.productNumber = item.productNumber + numberOfproducts;
    }
    else{
      newCartItem.productNumber = numberOfproducts;
      items.push(newCartItem);
    }

    return items;
  }

  addProductToCartFromInfo(product: Product, numberOfproducts: number){
    const mappedProduct = this.mappingProductToCartItem(product)

    const cart = this.getCart() ?? this.CreateNewCart()

    cart.items = this.addProductToCartOrChangeNumber2(cart.items, mappedProduct, numberOfproducts);

    this.setCartInApi(cart);
  }

  updateProductNumberInCart(productId:number, newNumberOfProducts: number){
    const cart = this.getCart();

    if(cart === null){
      return;
    }

    const product = cart.items.find(f => f.id === productId);
    if(product){
      product.productNumber = newNumberOfProducts;

      this.setCartInApi(cart);
    }
  }

  deleteProducts(productId: number, numberOfProducts: number){
    const cart = this.getCart();

    if(cart === null){
      return;
    }

    const product = cart.items.find(f => f.id === productId);

    if(product){
      product.productNumber = product.productNumber - numberOfProducts;
    }

    cart.items = cart.items.filter(f => f.id != productId)

    if(cart.items.length > 0){
      this.setCartInApi(cart);
    }
    else{
      this.deleteCart(cart);
    }

  }

  deleteCart(cart: Cart){
    return this.http.delete(this.basePath + "cart?cartId=" + cart.id).subscribe({
      next: () => {
        localStorage.removeItem("cartId");
        this.cartSource.next(null);
        this.cartSumCostsSource.next(null);
      }
    })
  }
 

  setCartInApi(cart: Cart){
    return this.http.post<Cart>(this.basePath + 'cart', cart).subscribe({
      next: cart => {
        this.cartSource.next(cart)
        this.countSums();
        
      }
    })
  }

  getCartByIdFromApi(cartId: string){
    return this.http.get<Cart>(this.basePath + 'cart?cartId=' + cartId).subscribe({
      next: cart => {
        this.cartSource.next(cart)
        this.countSums();

      }

    });

  }

}
