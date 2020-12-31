import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThankyouComponent } from './thankyou/thankyou.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    ThankyouComponent,
    ServerErrorComponent,
    NotfoundComponent,
    NavbarComponent,
  ],
  imports: [CommonModule, SharedModule, RouterModule],
  exports: [NavbarComponent],
})
export class CoreModule {}
