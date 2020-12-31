import { ThankyouComponent } from './core/thankyou/thankyou.component';
import { AuthGuardGuard } from './core/_guard/auth-guard.guard';
import { NotfoundComponent } from './core/notfound/notfound.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
  _client_account_,
  _client_checkout_,
  _client_notfound_,
  _client_order_,
  _client_servererror_,
  _client_shop_,
  _client_thankyou_,
} from './shared/_constVars/_client_consts';

const routes: Routes = [
  { path: '', redirectTo: _client_shop_, pathMatch: 'full' },
  {
    path: _client_shop_,
    loadChildren: () => import('./shop/shop.module').then((m) => m.ShopModule),
  },
  {
    path: _client_account_,
    loadChildren: () =>
      import('./account/account.module').then((m) => m.AccountModule),
  },
  {
    path: _client_servererror_,
    component: ServerErrorComponent,
  },
  {
    path: _client_notfound_,
    component: NotfoundComponent,
  },
  {
    path: _client_thankyou_,
    component: ThankyouComponent,
  },
  {
    path: _client_checkout_,
    canActivate: [AuthGuardGuard],
    loadChildren: () =>
      import('./checkout/checkout.module').then((m) => m.CheckoutModule),
  },
  {
    path: _client_order_,
    canActivate: [AuthGuardGuard],
    loadChildren: () =>
      import('./order/order.module').then((m) => m.OrderModule),
  },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
