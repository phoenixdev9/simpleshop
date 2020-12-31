import { BasketService } from 'src/app/core/_services/basket.service';
import { Router } from '@angular/router';
import { OrderService } from '../core/_services/order.service';
import { Component, OnInit } from '@angular/core';
import { _client_thankyou_ } from '../shared/_constVars/_client_consts';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  constructor(
    private os: OrderService,
    private bs: BasketService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  submitOrder() {
    this.os.addOrder().subscribe(
      () => {
        this.bs.clearBasket().subscribe(
          () => {
            this.router.navigate([`/${_client_thankyou_}/`]);
          },
          (err) => console.log(err)
        );
      },
      (err) => console.log(err)
    );
  }
}
