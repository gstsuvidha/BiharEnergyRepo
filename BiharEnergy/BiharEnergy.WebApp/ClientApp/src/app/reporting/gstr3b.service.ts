
import { Injectable, Inject } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable()
export class Gstr3bService {
  baseUrl;

  constructor(private http: HttpClient) {
    this.baseUrl= `api/Gstr3b`;
   }

 
        
 
index(searchMonth: number,yearId:number,companyId : number) {
    return this.http.get(`${this.baseUrl}/Gstr3bDetails?searchMonth=${searchMonth}&year=${yearId}&companyId=${companyId}`);
    }
}