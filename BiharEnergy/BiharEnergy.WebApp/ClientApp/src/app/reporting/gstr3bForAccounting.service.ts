import { Injectable } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';

@Injectable()
export class Gstr3bForAccountingService {

baseUrl;    

constructor(private http : HttpClient) { 
    this.baseUrl = 'api/Gstr3bForAccounting';
}

index(searchMonth: number,yearId:number,accountingUnitId : number) {
    return this.http.get(`${this.baseUrl}/Gstr3bDetails?searchMonth=${searchMonth}&year=${yearId}&accountingUnitId=${accountingUnitId}`);
}


}
