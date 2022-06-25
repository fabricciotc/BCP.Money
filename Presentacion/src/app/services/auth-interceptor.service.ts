import { HttpHandler, HttpHeaderResponse, HttpInterceptor, HttpProgressEvent, HttpRequest, HttpResponse, HttpSentEvent, HttpUserEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from '../account/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private accountService: AccountService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse
      | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
      var token = this.accountService.obtenerToken();
      req = req.clone({
          setHeaders: { Authorization: "Bearer " + token }
      });
      return next.handle(req);
  }
}
