import { HttpInterceptor } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogInterceptorService implements HttpInterceptor {
  //VER SOLICUTDES HECHAS EN HTTP PASO A PASO

  intercept(req: import("@angular/common/http").HttpRequest<any>, next: import("@angular/common/http").HttpHandler):
      Observable<import("@angular/common/http").HttpEvent<any>>
  {
      console.log(req);
      return next.handle(req);
  }

constructor() { }
}
