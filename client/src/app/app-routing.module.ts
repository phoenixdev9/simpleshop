import { ThankyouComponent } from './thankyou/thankyou.component';
import { OrderComponent } from './order/order.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { AuthGuardGuard } from './_guard/auth-guard.guard';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ProductDetailComponent } from './shop/product-detail/product-detail.component';
import { ShopComponent } from './shop/shop.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', component: ShopComponent },
  { path: 'shop/:id', component: ProductDetailComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: RegisterComponent },
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
