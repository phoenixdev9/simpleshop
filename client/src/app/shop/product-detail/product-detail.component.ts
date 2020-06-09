import { ProductsService } from './../../_services/products.service';
import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/_models/IProduct';
import { ActivatedRoute } from '@angular/router';

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
    private activateRoute: ActivatedRoute // private bcService: BreadcrumbService, // private basketService: BasketService
  ) {
    // this.bcService.set('@productDetails', '');
  }
  addItemToBasket() {
    // this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity() {
    // this.quantity++;
  }

  decrementQuantity() {
    // if (this.quantity > 1) {
    //   this.quantity--;
    // }
  }
  ngOnInit() {
    this.loadProduct();
  }
  loadProduct() {
    this.shopService
      .getProduct(this.activateRoute.snapshot.paramMap.get('id'))
      .subscribe(
        (product) => {
          console.log('product', product);
          this.product = product;
          // this.bcService.set('@productDetails', product.name);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
