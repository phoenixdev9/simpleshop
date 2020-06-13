// import { BasketService } from './_services/basket.service';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { BasketService } from './_services/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(
    private basketService: BasketService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.loadCurrentUser();
    this.loadBasket();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if (token) {
      this.accountService.loadCurrentUser(token).subscribe(
        () => {
          console.log('loaded user');
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  loadBasket() {
    this.basketService.getBasket().subscribe(
      () => {
        console.log('initialised basket');
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
