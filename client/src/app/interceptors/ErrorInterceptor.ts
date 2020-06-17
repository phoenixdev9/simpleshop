import {
  _client_notfound,
  _client_servererror,
} from './../_constVars/_client_consts';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { _client_login } from '../_constVars/_client_consts';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error) {
          if (error.status === 400) {
            if (error.error.errors) {
              const e = error.error.errors;
              for (let index = 0; index < e.length; index++) {
                alert(e[index]);
              }
              throw error.error;
            } else {
              alert('Bad request');
            }
          }
          if (error.status === 404) {
            this.router.navigate([_client_notfound]);
          }
          if (error.status === 401) {
            alert(`You are not authorized, please log in.`);
          }
          if (error.status === 500) {
            this.router.navigate([_client_servererror]);
          }
        }
        return throwError(error);
      })
    );
  }
}
