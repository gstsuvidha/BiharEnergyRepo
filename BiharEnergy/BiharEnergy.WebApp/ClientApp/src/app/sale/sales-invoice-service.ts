import { HttpClient } from '@angular/common/http';
import { ServiceBase } from "../service-base";
import { ISalesInvoice } from "./isalesinvoice";
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';

@Injectable()

    

export class SalesInvoiceService extends ServiceBase<ISalesInvoice> {
    salesInvoice: ISalesInvoice[];



    constructor(http:HttpClient) {

        super(http, 'api/SalesInvoices');
    }

    
    getInvoiceForPrint(id:number):Observable<any>{
        return this.http.get(`${this.baseUrl}/${id}/detail`);
                            
    }
    initializeObject(): ISalesInvoice {
        // Return an initialized object
        return {
            id: 0,
            date: new Date(),
            invoiceNumber: 0,
            isReverseChargeApplicable: false,  
            isPlaceOfSupplyDifferent:false,
            placeOfSupply: '',
            shippingAddress: '',
            customerName: '',
            billingAddress: '',
            totalCessAmount: 0,
            totalTaxableValue: 0,
            totalTaxAmount: 0,
            receivedAmount: 0,
            reference: '',
            totalInvoiceValue: 0,
            modeOfPayment : '',
            totalDiscount: 0,
            totalCgst:0,
            totalIgst:0,
            totalSgst:0,
            invoicePrefix:'',
            salesInvoiceItems: []



        }
    }

  
  
     getAllByMonth(monthId:number,year:number):Observable<ISalesInvoice[]>{
       
          

         if(monthId == 1 || monthId ==2 || monthId == 3) //fINANCIAL YEAR CONVERSION
          year++;  
         
         
         return this.http.get<ISalesInvoice[]>(`${this.baseUrl}?searchMonth=${monthId}&year=${year}`);

     }

     getInvoiceNumber(): Observable<number> {

         return this.http.get<number>(`${this.baseUrl}/invoiceNumber`);

     }
    
     isInvoiceNumberUnique(invoiceNumber: number): Observable<boolean> {

       return this.http.get<boolean>(`${this.baseUrl}/isInvoiceNumberUnique/${invoiceNumber}`);

     }



}
