export interface IProduct {
    id: number,
    name: string,
    description: string,
    // item: string,
    productType: ProductType,
    stockInHand: number,
    opStockDate: Date,
    hsnSacCode: string,
    unit: string,
    unitOthers: string,
    rate: number,
    isTaxIncluded: boolean,
    isSaleable: boolean,
    isPurchaseable: boolean,
    isReverseChargeApplicable: boolean,
    perReverseCharge: number,
    igst: number,
    cess: number,
    itcPercentage: number,
    itcEligibility: string,
    
    
}

export enum ProductType {
    Goods,
    Service
}