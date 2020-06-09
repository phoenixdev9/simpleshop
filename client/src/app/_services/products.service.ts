import { IPagination, PaginatedResult } from './../_models/IPagination';
import { ShopParams } from '../_models/ShopParams';
import {
  _api_default,
  _api_products,
  _api_brands,
  _api_types,
} from './../_constVars/_api_consts';
import { _getPagination } from './../_helper/_getPagination';
import { IProduct } from './../_models/IProduct';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { _paramsAppend } from '../_helper/_paramsAppend';
import { map } from 'rxjs/operators';
import { IBrand } from '../_models/IBrand';
import { IType } from '../_models/IType';
import { of } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private base = _api_default + _api_products + '/';
  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = [];
  shopParams = new ShopParams();
  /////////////////////Pag
  currentPage: number = 1;

  constructor(private http: HttpClient) {}

  getProducts(): Observable<PaginatedResult<IProduct[]>> {
    const paginatedResult: PaginatedResult<IProduct[]> = new PaginatedResult<
      IProduct[]
    >();
    let params = _paramsAppend(this.shopParams);
    return this.http
      .get<IProduct[]>(this.base, { observe: 'response', params })
      .pipe(
        map((response) => {
          const data = _getPagination(response, paginatedResult); //combine the header and body data into PaginatedResult
          this.products = data.result;
          return data;
        })
      );
  }

  getProduct(id: string): Observable<IProduct> {
    const product = this.products.find((p) => p.id === id);
    if (product) {
      return of(product);
    }
    return this.http.get<IProduct>(this.base + id);
  }

  getBrands(): Observable<IBrand[]> {
    if (this.brands.length > 0) {
      return of(this.brands);
    }
    return this.http.get<IBrand[]>(this.base + _api_brands).pipe(
      map((response) => {
        this.brands = response;
        return response;
      })
    );
  }

  getTypes(): Observable<IType[]> {
    if (this.types.length > 0) {
      return of(this.types);
    }
    return this.http.get<IType[]>(this.base + _api_types).pipe(
      map((response) => {
        this.types = response;
        return response;
      })
    );
  }
  getShopParams() {
    return this.shopParams;
  }

  setShopParams(params: ShopParams) {
    this.shopParams = params;
  }
}
