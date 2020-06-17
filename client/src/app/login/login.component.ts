import { BasketService } from 'src/app/_services/basket.service';
import { _client_home } from './../_constVars/_client_consts';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router, ActivatedRoute } from '@angular/router';
import {
  _client_notfound,
  _client_register,
} from '../_constVars/_client_consts';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  returnUrl: string;
  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private bs: BasketService
  ) {}

  ngOnInit() {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl;
  }

  readonly emailOnly = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
  isValidateTextTrue(data: any) {
    if (data.touched && data.valid) {
      return true;
    } else {
      return false;
    }
  }
  isValidateTextFalse(data: any) {
    if (data.touched && data.invalid) {
      return true;
    } else {
      return false;
    }
  }
  submitFunc(data: any, event: Event) {
    event.preventDefault();
    this.accountService.login(data.value).subscribe(
      (e) => {
        this.router.navigate([_client_home]);
        this.loadBasket();
      },
      (error) => {
        console.log(error);
      }
    );
  }
  loadBasket() {
    this.bs.getBasket().subscribe(
      () => {
        console.log('initialised basket');
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
