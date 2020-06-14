import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from 'src/app/_models/IBasket';
import { BasketService } from 'src/app/_services/basket.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
  basket$: Observable<IBasket>;
  constructor(private bs: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.bs.basket$;
  }
  deleteBusket(id: string) {
    this.bs.removeItemFromBasket(id).subscribe(
      () => {},
      (err) => console.log(err)
    );
  }
}
