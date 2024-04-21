import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/shared/models/order';
import { OrderService } from '../order.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-info',
  templateUrl: './order-info.component.html',
  styleUrls: ['./order-info.component.scss']
})
export class OrderInfoComponent implements OnInit{

  order ?: Order;

  constructor (private ordService: OrderService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {

    const orderId = this.route.snapshot.paramMap.get('id');

    orderId && this.ordService.getOrderInfo( parseInt(orderId, 10) ).subscribe({
      next: response => { this.order = response; }})

  }

}
