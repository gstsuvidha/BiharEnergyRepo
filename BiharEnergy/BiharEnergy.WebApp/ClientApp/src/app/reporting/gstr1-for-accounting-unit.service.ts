import { Injectable } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { Observable } from '../../../node_modules/rxjs';

@Injectable()
export class Gstr1ForAccountingUnitService {

  baseUrl: string = "api/Gstr1ReportingForAccountingUnit";

  constructor(private http: HttpClient) { }

  getGstr1ForAccountingUnitB2B(accountingUnitId: number,monthId: number,year : number) {
    if(monthId == 1 || monthId ==2 || monthId == 3) //fINANCIAL YEAR CONVERSION
    year++;
    return this.http.get(`${this.baseUrl}/B2B?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);
  }

  getGstr1ForAccountingUnitB2C(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/B2C?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);

  }

  getGstr1ForAccountingUnitB2CL(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/B2CL?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);

  }

  getCreditDebitNoteRegisterForAccountingUnit(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/CDNR?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);

  }

  getCreditDebitNoteUnregisterForAccountingUnit(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/CDNUR?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);

  }
  getAdvanceReceiveForAccountingUnit(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/AdvanceReceive?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);

  }

  getGstr1HsnForAccountingUnit(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/Hsn?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);
  }

  getGstr1DocumentDetailForAccountingUnit( accountingUnitId: number,searchMonth: number,yearId : number) {
    return this.http.get(`${this.baseUrl}/DocumentDetail?searchMonth=${searchMonth}&year=${yearId}&accountingUnitId=${accountingUnitId}`);
  }

  getGstr1ExportInvoiceDetailForAccountingUnit(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/ExportInvoice?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);

  }
  getGstr1FinalReportForAccountingUnit(accountingUnitId: number,searchMonth: number,yearId : number):Observable<any> {

    return this.http.get(`${this.baseUrl}/gstr1?searchMonth=${searchMonth}&year=${yearId}&accountingUnitId=${accountingUnitId}`);

  }
  getAccountingUnitExtraInfo(accountingUnitId : number) {

      return this.http.get(`${this.baseUrl}/gstr1AccountingUnitInfo?accountingUnitId=${accountingUnitId}`);

  }

  getPlaceOfSupplyCdnurForAccountingUnit(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/PlaceOfSupplyCdnur?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);

  }
  getPlaceOfSupplyCdnrForAccountingUnit(monthId: number,year : number,accountingUnitId: number) {
    return this.http.get(`${this.baseUrl}/PlaceOfSupplyCdnr?searchMonth=${monthId}&year=${year}&accountingUnitId=${accountingUnitId}`);


  }
  getCustomerName(accountingUnitId: number,searchMonth: number,yearId : number) {
    return this.http.get(`${this.baseUrl}/CustomerName?searchMonth=${searchMonth}&year=${yearId}&accountingUnitId=${accountingUnitId}`);

  }



}
