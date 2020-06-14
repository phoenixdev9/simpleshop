import { IOrder } from './../_models/IOrder';
import { _api_order } from './../_constVars/_api_consts';
import { Injectable } from '@angular/core';
import { _api_default, _api_account } from '../_constVars/_api_consts';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class OrderService {
  baseUrl = _api_default + _api_order + '/';

  constructor(private http: HttpClient) {}

  addOrder() {
    return this.http.post(this.baseUrl, {});
  }

  getOrder() {
    return this.http.get(this.baseUrl);
  }
  getOrderById(id: string) {
    return this.http.get(this.baseUrl + id);
  }
}
