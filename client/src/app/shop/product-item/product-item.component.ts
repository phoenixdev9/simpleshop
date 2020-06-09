import { Component, OnInit, Input } from '@angular/core';
import { IProduct } from 'src/app/_models/IProduct';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct;
  constructor() {}
  addItemToBasket() {}
  ngOnInit(): void {}
}
