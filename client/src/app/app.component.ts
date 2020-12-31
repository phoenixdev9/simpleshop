// import { BasketService } from './_services/basket.service';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './core/_services/account.service';
import { BasketService } from './core/_services/basket.service';

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
  }

  private loadCurrentUser(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.accountService.loadCurrentUser(token).subscribe(
        () => {
          this.loadBasket();
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  private loadBasket(): void {
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
