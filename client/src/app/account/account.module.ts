import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {
  _client_signin_,
  _client_signup_,
} from '../shared/_constVars/_client_consts';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

const routes = [
  { path: _client_signin_, component: LoginComponent },
  { path: _client_signup_, component: RegisterComponent },
];
@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [CommonModule, RouterModule.forChild(routes), SharedModule],
})
export class AccountModule {}
