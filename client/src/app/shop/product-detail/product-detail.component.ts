import { _client_checkout } from './../../_constVars/_client_consts';
import {
  _checkItemExistsInCart,
  _findItemExistsInCart,
} from './../../_utils/_checkItemExistsInCart';
import { ProductsService } from './../../_services/products.service';
import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/_models/IProduct';
import { ActivatedRoute, Router } from '@angular/router';
import { BasketService } from 'src/app/_services/basket.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent implements OnInit {
  product: IProduct;
  quantity = 1;
  constructor(
    private shopService: ProductsService,
    private activateRoute: ActivatedRoute,
    private basketService: BasketService,
    private router: Router
  ) {}
  addItemToBasket(id: string) {
    this.basketService
      .editBasket({ ProductId: id, Quantity: this.quantity })
      .subscribe(
        () => {
          this.router.navigate([_client_checkout]);
        },
        (err) => console.log(err)
      );
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
  ngOnInit() {
    this.loadProduct();
  }
  checkProduct() {
    const cart = this.basketService.getCurrentBasketValue()?.cart;
    const item = _findItemExistsInCart(cart, this.product?.id);
    if (item) {
      this.quantity = item.quantity;
    }
  }
  loadProduct() {
    this.shopService
      .getProduct(this.activateRoute.snapshot.paramMap.get('id'))
      .subscribe(
        (product) => {
          this.product = product;
          this.checkProduct();
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
