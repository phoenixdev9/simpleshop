import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { _client_signin, _client_signup } from '../_constVars/_client_consts';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

const routes = [
  { path: _client_signin, component: LoginComponent },
  { path: _client_signup, component: RegisterComponent },
];
@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [CommonModule, RouterModule.forChild(routes), SharedModule],
})
export class AccountModule {}
