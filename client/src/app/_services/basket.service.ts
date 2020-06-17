import { IAddBasket, IBasket } from './../_models/IBasket';
import { _api_default, _api_basket } from './../_constVars/_api_consts';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, of } from 'rxjs';
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
    if (localStorage.getItem('token')) {
      return this.http.get(this.baseUrl).pipe(
        map((basket: IBasket) => {
          this.basketSource.next(basket);
          return basket;
        })
      );
    } else {
      this.basketSource.next(null);
      return of(null);
    }
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
  clearBasket() {
    return this.http.delete(this.baseUrl).pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        return basket.cart;
      })
    );
  }
  logoutBasket() {
    console.log('logout basket');
    this.basketSource.next(null);
  }
}
