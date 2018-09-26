
// import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { LoginService } from '../login.service';
import { ILogin } from '../ilogin';


// import 'rxjs/Rx';


@Component({
    selector: 'loginform',
    templateUrl: './loginform.component.html',
    styleUrls: ['./loginform.component.css']
})
export class LoginFormComponent {
    // isConnected: Observable<boolean>
    
    busy: Subscription;

    constructor(private router: Router,
                private loginService: LoginService) {
                    // this.isConnected = Observable.merge(
                    //     Observable.of(navigator.onLine),
                    //     Observable.fromEvent(window, 'online').map(() => true),
                    //     Observable.fromEvent(window, 'offline').map(() => false));
                    

    }
    ngOnInit() {


    }


    



    login: ILogin;


    doLogin(userName: string, password: string): void {
        var online= navigator.onLine;
        if(!online){
            // alert("No internet Connection");

            // this.toastyService.error({
            //     title:  'No Internet Connection',
            //     theme: 'bootstrap',
            //     showClose: true,
            //     timeout: 5000
            // });
    

            // return;
        }


        this.login = {
            userName: userName,
            password: password
        };


            this.busy = this.loginService.getToken(this.login)
                .subscribe(res => {
                    // if (res. === 200)
                        this.onLoginSuccess();
                });//,
            // error => alert("Failed" + error));


    }

    onLoginSuccess(): void {
        this.router.navigate(['/authenticated/dash-board']);

    }

}
