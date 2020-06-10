import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from '../_services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private busyService: BusyService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // if (req.method === 'POST' && req.url.includes('orders')) {
    //   return next.handle(req);
    // }
    // if (req.method === 'DELETE') {
    //   return next.handle(req);
    // }
    // if (req.url.includes('emailexists')) {
    //   return next.handle(req);
    // }
    this.busyService.busy();
    return next.handle(req).pipe(
      delay(800),
      finalize(() => {
        this.busyService.idle();
      })
    );
  }
}
