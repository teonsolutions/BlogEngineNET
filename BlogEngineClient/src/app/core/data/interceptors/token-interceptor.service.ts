import { HttpEvent, HttpHandler, HttpInterceptor, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/assets/environments/environment';
import { DataService } from '../data.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {
  token: string;
  constructor(private dataService: DataService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const { url, method, headers, body } = req;

    switch (true) {
      case url.startsWith(environment.blogEngineApiPrefix):
        return this.authorized(req, next);
      default:
        req = req.clone({
          setHeaders: {
            "Content-Type": "application/json",
          },
        });
        return next.handle(req);
    }
  }

  authorized = (
    request: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> => {
    const { url, method, headers, body } = request;
    const loginToken = this.dataService.session().getSignInData();
    const currentMenu = this.dataService.session().getCurrentMenuData();

    switch (true) {
      case url.endsWith("/login"):
        request = request.clone({
          setHeaders: {
            "Content-Type": "application/json; charset=utf-8"
          },
        });
        return next.handle(request);
      default:
        request = request.clone({
          setHeaders: {
            "Content-Type": "application/json; charset=utf-8",
            Authorization: `Bearer ${loginToken.token}`,
            RolGuid: `${loginToken.rolGuid}`,
            MenuGuid: `${currentMenu?.guid}`,
            ActionCode: 'ACC'
          },
        });
    }

    return next.handle(request);
  }

}
