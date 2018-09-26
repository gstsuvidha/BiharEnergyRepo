import { CanActivate, CanActivateChild, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { LoginService } from '../../login/login.service';


@Injectable()
export class AuthGuardService implements CanActivate, CanActivateChild {
  
      constructor(private loginService: LoginService, private router: Router) {}
  
      canActivate() : boolean {

          if (!this.loginService.isAuthenticated()) {
              this.router.navigate(['/login']);
          }

          return this.loginService.isAuthenticated();
      }
  
      canActivateChild() : boolean {
          return this.canActivate();
      }
  }