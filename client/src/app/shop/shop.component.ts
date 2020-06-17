import { BasketService } from 'src/app/_services/basket.service';
import { ShopParams } from '../_models/ShopParams';
import { ProductsService } from './../_services/products.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { IProduct } from '../_models/IProduct';
import { IBrand } from '../_models/IBrand';
import { IType } from '../_models/IType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  /////////////////
  totalCount: number;
  totalPages: number;
  ///////////////////
  shopParams: ShopParams;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];
  constructor(private ps: ProductsService, private bs: BasketService) {
    this.shopParams = this.ps.getShopParams();
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  ngOnInit(): void {}
  getProducts() {
    this.ps.getProducts().subscribe(
      (response) => {
        const { pagination, result } = response;
        const { totalItems, totalPages } = pagination;
        this.totalCount = totalItems;
        this.totalPages = totalPages;
        this.products = result;
      },
      (err) => console.error(err)
    );
  }
  getBrands() {
    this.ps.getBrands().subscribe(
      (response) => {
        this.brands = [{ id: null, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getTypes() {
    this.ps.getTypes().subscribe(
      (response) => {
        this.types = [{ id: null, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }
  /////
  private filterData(params: ShopParams) {
    this.ps.setShopParams(params);
    this.getProducts();
  }

  onBrandSelected(brandId: string) {
    const params = this.ps.getShopParams();
    params.brandId = brandId;
    params.pageNumber = 1;
    this.filterData(params);
  }

  onTypeSelected(typeId: string) {
    const params = this.ps.getShopParams();
    params.typeId = typeId;
    params.pageNumber = 1;
    this.filterData(params);
  }

  onSortSelected(sort: string) {
    const params = this.ps.getShopParams();
    params.sort = sort;
    this.filterData(params);
  }

  onPageChanged(page: number) {
    const params = this.ps.getShopParams();
    if (params.pageNumber !== page) {
      params.pageNumber = page;
      this.filterData(params);
    }
  }

  onSearch() {
    const params = this.ps.getShopParams();
    params.search = this.searchTerm.nativeElement.value;
    params.pageNumber = 1;
    this.filterData(params);
  }

  onReset() {
    if (this.searchTerm) {
      this.searchTerm.nativeElement.value = '';
    }
    this.shopParams = new ShopParams();
    this.ps.setShopParams(this.shopParams);
    this.getProducts();
  }
}
