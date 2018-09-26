import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class Gstr7Service {
  baseUrl= "api/Gstr7";

  constructor(private http : HttpClient) { }
 getAll(searchMonth: number, year: number, accountingUnitId: number){
   return this.http.get(`${this.baseUrl}?searchMonth=${searchMonth}&year=${year}&accountingUnitId=${accountingUnitId}`)
   
 }
 

 getGstr7Details(accountingUnitId: number)
 {
   return this.http.get(`${this.baseUrl}/details?accountingUnitId=${accountingUnitId}`);
 }
}
