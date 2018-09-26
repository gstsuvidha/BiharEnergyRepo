import { HttpClient } from '@angular/common/http';
import { ICustomer, RegistrationType } from './icustomer';
import { Injectable } from '@angular/core';
import { ServiceBase } from '../../service-base';

@Injectable()
export class CustomerService extends ServiceBase<ICustomer>{
  customer:ICustomer[];

  constructor(http:HttpClient) { 
    super(http, 'api/Customers')
  }

  initializeObject():ICustomer{
    return{
      id: 0,
      // customerOpeningDate: new Date(),  
      name: "",
      gstin: "",
      address: "",
      state: "",
      contactNumber: 0,
      registrationType: RegistrationType.Registered,
      email : ""
      // openBalance: 0,
    }
}
}