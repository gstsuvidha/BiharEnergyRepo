import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Response} from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs';

@Injectable()
export class Gstr1Service {

  baseUrl;
  constructor(private http : HttpClient) {
    this.baseUrl=`api/Gstr1Reporting`;
   }

   getGstr1B2B(monthId: number,year : number,companyId : number) {
      

    if(monthId == 1 || monthId ==2 || monthId == 3) //fINANCIAL YEAR CONVERSION
     year++;
    return this.http.get(`${this.baseUrl}/B2B?searchMonth=${monthId}&year=${year}&companyId=${companyId}`);

}

getGstr1B2C(searchMonth: number,year : number,companyId: number) {
  return this.http.get(`${this.baseUrl}/B2C?searchMonth=${searchMonth}&year=${year}&companyId=${companyId}`);

}

getGstr1B2CL(searchMonth: number,year: number,companyId : number) {
  return this.http.get(`${this.baseUrl}/B2CL?searchMonth=${searchMonth}&year=${year}&companyId=${companyId}`);

}

getCreditDebitNoteRegister(searchMonth: number,year:number,companyId : number) {
   return this.http.get(`${this.baseUrl}/CDNR?searchMonth=${searchMonth}&year=${year}&companyId=${companyId}`);

}
getCreditDebitNoteUnregister(searchMonth: number,year:number,companyId : number) {
  return this.http.get(`${this.baseUrl}/CDNUR?searchMonth=${searchMonth}&year=${year}&companyId=${companyId}`);

}
getAdvanceReceive(searchMonth: number,year:number,companyId : number) {
  return this.http.get(`${this.baseUrl}/AdvanceReceive?searchMonth=${searchMonth}&year=${year}&companyId=${companyId}`);

}



   getGstr1Hsn (searchMonth: number,year:number,company:number) {
    return this.http.get(`${this.baseUrl}/Hsn?searchMonth=${searchMonth}&year=${year}&companyId=${company}`);
  }

  getGstr1DocumentDetail(searchMonth: number,yearId:number,companyId:number):Observable<any> {
    return this.http.get(`${this.baseUrl}/DocumentDetail?searchMonth=${searchMonth}&year=${yearId}&companyId=${companyId}`);
}

getGstr1ExportInvoiceDetail(searchMonth: number,yearId:number,companyId:number) {
    return this.http.get(`${this.baseUrl}/ExportInvoice?searchMonth=${searchMonth}&year=${yearId}&companyId=${companyId}`);

}
getGstr1FinalReport(searchMonth: number,yearId : number,companyId: number):Observable<any> {
    
    return this.http.get(`${this.baseUrl}/gstr1?searchMonth=${searchMonth}&year=${yearId}&companyId=${companyId}`);

}
// getAccountingUnitExtraInfo():Observable<any> {
    
//     return this.http.get(`${this.baseUrl}/gstr1AccountingUnitInfo`);

// }

 getPlaceOfSupplyCdnur(searchMonth: number,year:number,companyId:number) {
     return this.http.get(`${this.baseUrl}/PlaceOfSupplyCdnur?searchMonth=${searchMonth}&year=${year}&companyId=${companyId}`);

}
 getPlaceOfSupplyCdnr(searchMonth: number,year:number,companyId:number) {
 return this.http.get(`${this.baseUrl }/PlaceOfSupplyCdnr?searchMonth=${searchMonth}&year=${year}&companyId=${companyId}`);
    

}
 getCustomerName(searchMonth: number,yearId:number,companyId:number) {
     return this.http.get(`${this.baseUrl}/CustomerName?searchMonth=${searchMonth}&year=${yearId}&companyId=${companyId}`);
     
}


}
