import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { ILogin } from './ilogin';
import {SlimLoadingBarService} from "ng2-slim-loading-bar";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private _loadingBar: SlimLoadingBarService) {} 

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
        
        const token = localStorage.getItem("token");
        this._loadingBar.start();
        if(token){
            const cloned = request.clone({
                headers : request.headers.set("Authorization","Bearer " + token)
            });
           
            return next.handle(cloned).do((event : HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    this._loadingBar.complete();
                }});
            
        }else{
            this._loadingBar.complete();
            return next.handle(request);
            
        }
    }
}
