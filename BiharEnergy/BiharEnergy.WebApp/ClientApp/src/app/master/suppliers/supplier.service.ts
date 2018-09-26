import { RegistrationType } from './isupplier';

import { ISupplier } from './isupplier';
import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { ServiceBase } from '../../service-base';
import { Observable } from 'rxjs';

@Injectable()
export class SupplierService extends ServiceBase<ISupplier>{
  supplier:ISupplier[];

  constructor(http:HttpClient) { 
    super(http, 'api/Suppliers')
  }


  initializeObject():ISupplier{
    return{
      id: 0,
      supplierOpeningDate: new Date(),
      name: "",
      gstin: "",
      address: "",
      state: "",
      contactNumber: "",
      registrationType: RegistrationType.Registered,
      openBalance: 0,
      email : ""
    }
}

getRegisteredSuppliers():Observable<ISupplier[]>{
  return this.http.get<ISupplier[]>(`${this.baseUrl}/RegisteredSuppliers`)
}
}