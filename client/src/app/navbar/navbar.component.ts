import {
  _client_signin,
  _client_signup,
  _client_order,
  _client_account,
  _client_checkout_,
} from '../shared/_constVars/_client_consts';
import { AccountService } from '../_services/account.service';
import { IUser } from '../shared/_models/IUser';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from '../_services/basket.service';
import { IBasket } from '../shared/_models/IBasket';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  login: string = _client_account + '/' + _client_signin;
  register: string = _client_account + '/' + _client_signup;
  checkout: string = `/${_client_checkout_}/`;
  order: string = _client_order;
  currentUser$: Observable<IUser>;
  basket$: Observable<IBasket>;

  constructor(private as: AccountService, private bs: BasketService) {}

  ngOnInit() {
    this.currentUser$ = this.as.currentUser$;
    this.currentUser$.subscribe((e) => console.log('eeeee', e));
    this.basket$ = this.bs.basket$;
  }
  logout() {
    this.as.logout();
    this.bs.logoutBasket();
  }
}
