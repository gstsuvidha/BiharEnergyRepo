import { IProduct, ProductType } from "./iproduct";
import { ServiceBase } from "../../service-base";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class ProductService extends ServiceBase<IProduct>{
  product:IProduct[];

  constructor(http:HttpClient) { 
    super(http, 'api/Products')
  }

  initializeObject():IProduct{
    return{
        id: 0,
        name: "",
        description: "",
        // item: "",
        productType:ProductType.Goods,
        stockInHand: 0,
        opStockDate: new Date(),
        hsnSacCode: "",
        unit: "",
        unitOthers: "",
        rate: 0,
        isTaxIncluded: false,
        isSaleable: false,
        isPurchaseable: false,
        isReverseChargeApplicable: false,
        perReverseCharge: 0,
        igst: 0,
        cess: 0,
        itcPercentage: 0,
        itcEligibility: ""
       
    }
}
}