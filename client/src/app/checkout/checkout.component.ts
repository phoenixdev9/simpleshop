import { BasketService } from 'src/app/_services/basket.service';
import { Router } from '@angular/router';
import { OrderService } from './../_services/order.service';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { IBasketItem, IBasket } from '../_models/IBasket';
import { _client_thankyou } from '../_constVars/_client_consts';

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
            this.router.navigate([_client_thankyou]);
          },
          (err) => console.log(err)
        );
      },
      (err) => console.log(err)
    );
  }
}
