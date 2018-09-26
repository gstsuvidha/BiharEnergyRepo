import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Iuserprofile } from './iuserprofile';
import { ServiceBase } from '../service-base';
import { tap } from 'rxjs/operators';
import { Company } from '../reporting/home/company';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class UserProfileService extends ServiceBase<Iuserprofile>{
  userProfile:Iuserprofile;
  company:Company[];
  

  constructor(http:HttpClient) { 
    super(http, 'api/AccountingUnitResolver')
  }

  initializeObject():Iuserprofile{
    return{
        id: 0,
        businessName: "",
        gstin: "",
        address: "",
        email: "",
        contactNumber: "",
        turnOver: "",
        bankAccountNumber: "",
        ifscCode: "",
        registrationType: "",
        termsAndCondition: "",
        imgUrl: "",
        placeOfSupply:0,
        selectedYear :  new Date(),
        bankAccountName : "",
        currentGrossTurnOver : 0,
        companyId: 0,
      invoicePrefix: "",
      pan : ""
    }
}

getProfile(){
  return this.http.get<Iuserprofile>(`${this.baseUrl}/Profile`).pipe(tap(data => this.userProfile=data as Iuserprofile));
}
getCompany(){
  return this.http.get<Company[]>(`${this.baseUrl}/Companies`).pipe(tap(data => this.company = data as Company[]))
}

getAccountingUnits() {
    return this.http.get<Iuserprofile>(`${this.baseUrl}/AccountingUnits`).pipe(tap(data => this.userProfile = data as Iuserprofile));
  }
  
  updateProfile(userProfile : Iuserprofile){


    return this.http.put<Iuserprofile>(`${this.baseUrl}/Profile`,userProfile).pipe(tap(data => this.userProfile = data as Iuserprofile));

} 

  getYearSettings(): number {

    let year = JSON.parse(localStorage.getItem('year'));
    
    return year? +year:new Date().getFullYear();
}
setYearSettings(year: number) {
    
    localStorage.setItem('year', String(year));
}


getYearDisplaySettings(): string {

    let year = JSON.parse(localStorage.getItem('year')); //eg.2017
    let y =year % 100 + 1; // if 2017/100,rem=17+1=18
    let z = year + '-' + y; // '2017' + '-' + '18' (by concanating) = 2017-18(Display)
    return z;   

}
getInvoicePrefix(): Observable<string> {

  return this.http.get<string>(`${this.baseUrl}/invoicePrefix`);

}
getAccountingUnitByCompanyId(companyId : number){
  return this.http.get<Iuserprofile[]>(`${this.baseUrl}/AccountingUnitsForCompany?companyId=${companyId}`);
}



}
