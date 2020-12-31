import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { CartComponent } from './cart/cart.component';
import { AddressComponent } from './address/address.component';
import { CheckoutComponent } from './checkout.component';
import { RouterModule, Routes } from '@angular/router';
const routes: Routes = [{ path: '', component: CheckoutComponent }];
@NgModule({
  declarations: [CartComponent, AddressComponent, CheckoutComponent],
  imports: [CommonModule, SharedModule, RouterModule.forChild(routes)],
})
export class CheckoutModule {}
