import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShippingMethod } from '../shared/models/shippingMethod';
import { Order, OrderCreate } from '../shared/models/order';
import { ThisReceiver } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class FinalizationService {
  basePath = 'https://localhost:5002/api/';

  constructor(private http: HttpClient) { }

  getShippingMethods(){
    return this.http.get<ShippingMethod[]>(this.basePath + "order/shipping-methods")
  }

  createNewOrder(order: OrderCreate){
    return this.http.post<Order>(this.basePath + "order", order)
  }

}
