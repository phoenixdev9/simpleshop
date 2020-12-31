import { IOrder } from '../shared/_models/IOrder';
import { OrderService } from '../core/_services/order.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
})
export class OrderComponent implements OnInit {
  orders: IOrder[];
  constructor(private os: OrderService) {}

  ngOnInit(): void {
    this.getOrders();
  }
  getOrders() {
    this.os.getOrder().subscribe(
      (e: IOrder[]) => {
        this.orders = e;
      },
      (err) => console.log(err)
    );
  }
}
