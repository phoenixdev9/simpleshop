import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderComponent } from './order.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{ path: '', component: OrderComponent }];

@NgModule({
  declarations: [OrderComponent],
  imports: [CommonModule, SharedModule, RouterModule.forChild(routes)],
})
export class OrderModule {}
