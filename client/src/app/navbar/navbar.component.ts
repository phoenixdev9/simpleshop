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
  currentUser$: Observable<IUser>;
  basket$: Observable<IBasket>;

  constructor(private as: AccountService, private bs: BasketService) {}

  ngOnInit() {
    this.currentUser$ = this.as.currentUser$;
    this.basket$ = this.bs.basket$;
  }
  logout() {
    this.as.logout();
  }
}
