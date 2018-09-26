import { Injectable } from '@angular/core';
import { ServiceBase } from '../service-base';
import { Itds } from './Itds';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class TdsService extends ServiceBase<Itds>{
  tds:Itds[];

  constructor(http:HttpClient) {
    super(http, 'api/Tds')
   }

   initializeObject():Itds{
    return{
      id: 0,
      supplierId: 0,
      date:new Date(),
      placeOfSupply: "",
      amountPaid:0,
      cgstAmount: 0,
      sgstAmount: 0,
      igstAmount: 0,
      tdsAmount: 0,
      netAmount: 0,
     }
}
}


