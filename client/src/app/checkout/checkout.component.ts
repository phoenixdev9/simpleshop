import { Observable } from 'rxjs';
import { BasketService } from './../_services/basket.service';
import { Component, OnInit } from '@angular/core';
import { IBasketItem, IBasket } from '../_models/IBasket';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  // basket$: Observable<IBasket>;
  constructor(private bs: BasketService) {}

  ngOnInit(): void {
    // this.basket$ = this.bs.basket$;
  }
}
