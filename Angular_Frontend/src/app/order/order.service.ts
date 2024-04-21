import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  basePath = "https://localhost:5002/api/";

  constructor(private http: HttpClient) {

  }

  getOrderInfo(idNumbr: number) {
    return this.http.get<Order>( this.basePath + "order/" + idNumbr + "/client" );
  }

  getUserOrders() {
    return this.http.get<Order []>( this.basePath + "order/client" );
    }


}
