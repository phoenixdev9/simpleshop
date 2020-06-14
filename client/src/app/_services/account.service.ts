import { ILogin, ISignup } from './../_models/IAuthForm';
import {
  _api_account,
  _api_login,
  _api_signup,
  _api_address,
} from './../_constVars/_api_consts';
import { Injectable } from '@angular/core';
import { _api_default } from '../_constVars/_api_consts';
import { BehaviorSubject } from 'rxjs';
import { IUser } from '../_models/IUser';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { IAddress } from '../_models/IAddress';
import { _client_home } from '../_constVars/_client_consts';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = _api_default + _api_account + '/';
  private currentUserSource = new BehaviorSubject<IUser>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  loadCurrentUser(token: string) {
    if (token === null) {
      this.currentUserSource.next(null);
      return null;
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(this.baseUrl, { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  login(values: ILogin) {
    return this.http.post(this.baseUrl + _api_login, values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(values: ISignup) {
    return this.http.post(this.baseUrl + _api_signup, values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigate([_client_home]);
  }

  getUserAddress() {
    return this.http.get<IAddress>(this.baseUrl + _api_address);
  }

  updateUserAddress(address: IAddress) {
    return this.http.put<IAddress>(this.baseUrl + _api_address, address);
  }
}
