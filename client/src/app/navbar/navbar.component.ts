import {
  _client_login,
  _client_register,
  _client_checkout,
  _client_order,
} from './../_constVars/_client_consts';
import { AccountService } from './../_services/account.service';
import { IUser } from './../_models/IUser';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from '../_services/basket.service';
import { IBasket } from '../_models/IBasket';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  login: string = _client_login;
  register: string = _client_register;
  checkout: string = _client_checkout;
  order: string = _client_order;
  currentUser$: Observable<IUser>;
  basket$: Observable<IBasket>;

  constructor(private as: AccountService, private bs: BasketService) {}

  ngOnInit() {
    this.currentUser$ = this.as.currentUser$;
    this.basket$ = this.bs.basket$;
  }
  logout() {
    this.as.logout();
    this.bs.logoutBasket();
  }
}
