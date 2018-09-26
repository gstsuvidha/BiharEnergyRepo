import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { tokenNotExpired } from 'angular2-jwt';
import decode from 'jwt-decode';
import { HttpClient } from '@angular/common/http';
import { map, filter, tap, catchError } from 'rxjs/operators';
import 'rxjs/add/operator/do';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/map';

import { UserSetting } from '../shared/Utilities/user-setting';
import { ILogin } from './ilogin';



@Injectable()
export class LoginService {

    private baseUrl = '/api/Auth';
    private token: ITokenResponse;
    profile: IUserProfile;


    constructor(private http: HttpClient) {

    }

  isAuthenticated(): boolean {

        return tokenNotExpired('token');
    }

    public getToken(loginModel : ILogin): Observable<ITokenResponse> {

        const url = `${this.baseUrl}/token`;

        return this.http.post<ITokenResponse>(url,loginModel).pipe(
            tap((data) => {
                this.token = data;
                console.log("token", this.token);
                localStorage.setItem("token", this.token.access_token);
                localStorage.setItem('profile', JSON.stringify(this.token.userProfile));
                
                UserSetting.setInventorySettings(this.token.userProfile.inventorySelection);
                UserSetting.setYearSettings(this.token.userProfile.selectedYear);
            },
            
            err => {
                    if (err.status === 400)
                    alert("Invalid Login Credentials");
                   }

            
        ));
    } 

    // public getToken(loginModel: ILogin): Observable<Response> {

    //     const url = `${this.baseUrl}/token`;

    //     return this.http.post(url, loginModel).do((data: Response) => {

    //         this.token = <ITokenResponse>data.json();
    //         console.log("token", this.token);
    //         localStorage.setItem("token", this.token.access_token);
    //         localStorage.setItem('profile', JSON.stringify(this.token.userProfile));


    //         UserSetting.setInventorySettings(this.token.userProfile.inventorySelection);
    //         UserSetting.setYearSettings(this.token.userProfile.selectedYear);

    //     },
    //         err => {
    //             if (err.status === 400)
    //                 alert("Invalid Login Credentials");

    //         });

    // }

    public getUserProfile(): IUserProfile {

        return this.profile = JSON.parse(localStorage.getItem('profile'));

    }

    private readUserFromLocalStorage() {

        this.profile = JSON.parse(localStorage.getItem('profile'));
    }

    public logOut(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('profile');
        localStorage.removeItem('year');
        localStorage.removeItem('inventorySelection');
    }


}

export interface ITokenResponse {
    access_token: string;
    refresh_token: string;
    userProfile: IUserProfile;
}

export interface IUserProfile {
    sub: string;
    name: string;
    email: string;
    selectedYear: number;
    inventorySelection :boolean;
}
