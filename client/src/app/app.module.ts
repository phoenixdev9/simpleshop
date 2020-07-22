import { LoadingInterceptor } from './interceptors/loading.interceptors';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ShopComponent } from './shop/shop.component';
import { ProductItemComponent } from './shop/product-item/product-item.component';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { ProductDetailComponent } from './shop/product-detail/product-detail.component';
import { ErrorInterceptor } from './interceptors/ErrorInterceptor';
import { NotfoundComponent } from './notfound/notfound.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { JwtInterceptor } from './interceptors/JWTInterceptor';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CheckoutComponent } from './checkout/checkout.component';
import { OrderComponent } from './order/order.component';
import { CartComponent } from './checkout/cart/cart.component';
import { AddressComponent } from './checkout/address/address.component';
import { ThankyouComponent } from './thankyou/thankyou.component';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ShopComponent,
    ProductItemComponent,
    PagingHeaderComponent,
    PagerComponent,
    ProductDetailComponent,
    NotfoundComponent,
    ServerErrorComponent,
    LoginComponent,
    RegisterComponent,
    CheckoutComponent,
    OrderComponent,
    CartComponent,
    AddressComponent,
    ThankyouComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
