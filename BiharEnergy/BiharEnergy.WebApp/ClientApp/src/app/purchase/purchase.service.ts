import { HttpClient } from '@angular/common/http';
import { ServiceBase } from "../service-base";

import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { IPurchaseInvoice } from './ipurchase';

@Injectable()



export class PurchaseService extends ServiceBase<IPurchaseInvoice> {
    salesInvoice: IPurchaseInvoice[];



    constructor(http: HttpClient) {

        super(http, 'api/PurchaseInvoices');
    }

    initializeObject(): IPurchaseInvoice {
        // Return an initialized object
        return {

            id: 0,
            date: new Date(),
            postingDate: new Date(),
            invoiceNumber: '',
            supplierId: '',
            purchaseInvoiceType: '',

            isReverseChargeApplicable: false,

            placeOfSupply: '',

            shippingAddress: '',

            totalTaxableValue: 0,
            totalTaxAmount: 0,
            totalInvoiceValue: 0,
            totalCessAmount: 0,
            totalIgst: 0,
            totalCgst: 0,
            totalSgst: 0,
            reference: '',
            billedPassed : true,
            purchaseInvoiceItems: []
        }

    }

    getAllByMonth(monthId: number, year: number): Observable<IPurchaseInvoice[]> {

        

        if (monthId == 1 || monthId == 2 || monthId == 3) //fINANCIAL YEAR CONVERSION
            year++;


        return this.http.get<IPurchaseInvoice[]>(`${this.baseUrl}?searchMonth=${monthId}&year=${year}`);

    }
}
        // getAllByMonth(monthId:number, year:number):Observable<IPurchaseInvoice[]>{
        //     monthId=+monthId + +1;
        //     if(monthId == 1 || monthId ==2 || monthId == 3) //fINANCIAL YEAR CONVERSION
        //     year++;  


        // return this._http.get(`${this.baseUrl}?searchMonth=${monthId}&year=${year}`).map(resp => resp.json() as IPurchaseInvoice[]);




        // }



        //  getInfoByInvoiceNumber(originalInvoiceNumber: number):Observable<IPurchaseInvoice[]>{

        //      return this._http.get(`${this.baseUrl}?invoiceNumber=${originalInvoiceNumber}`).map(resp => resp.json() as IPurchaseInvoice[]);
        // }
