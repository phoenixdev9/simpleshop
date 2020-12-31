import { ThankyouComponent } from './thankyou/thankyou.component';
import { OrderComponent } from './order/order.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { AuthGuardGuard } from './_guard/auth-guard.guard';
import { NotfoundComponent } from './notfound/notfound.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ProductDetailComponent } from './shop/product-detail/product-detail.component';
import { ShopComponent } from './shop/shop.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { _client_account } from './_constVars/_client_consts';

const routes: Routes = [
  { path: '', component: ShopComponent },
  { path: 'shop/:id', component: ProductDetailComponent },
  {
    path: _client_account,
    loadChildren: () =>
      import('./account/account.module').then((m) => m.AccountModule),
  },
  {
    path: 'server-error',
    component: ServerErrorComponent,
  },
  {
    path: 'not-found',
    component: NotfoundComponent,
  },
  {
    path: 'thankyou',
    component: ThankyouComponent,
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuardGuard],
    children: [
      {
        path: 'checkout',
        component: CheckoutComponent,
      },
      {
        path: 'order',
        component: OrderComponent,
      },
    ],
  },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
