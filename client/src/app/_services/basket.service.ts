import { IAddBasket, IBasket } from './../_models/IBasket';
import { _api_default, _api_basket } from './../_constVars/_api_consts';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = _api_default + _api_basket + '/';
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  constructor(private http: HttpClient) {}
  getBasket() {
    return this.http.get(this.baseUrl).pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        return basket;
      })
    );
  }
  editBasket(data: IAddBasket) {
    return this.http.post(this.baseUrl, data).pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        return basket.cart;
      })
    );
  }
  getCurrentBasketValue() {
    return this.basketSource.value;
  }
  removeItemFromBasket(itemId: string) {
    return this.http.delete(this.baseUrl + itemId).pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        return basket.cart;
      })
    );
  }
}
