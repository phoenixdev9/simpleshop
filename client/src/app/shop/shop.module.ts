import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
const routes: Routes = [
  { path: '', component: ShopComponent },
  { path: ':id', component: ProductDetailComponent },
];

@NgModule({
  declarations: [ShopComponent, ProductDetailComponent, ProductItemComponent],
  imports: [CommonModule, SharedModule, RouterModule.forChild(routes)],
})
export class ShopModule {}
