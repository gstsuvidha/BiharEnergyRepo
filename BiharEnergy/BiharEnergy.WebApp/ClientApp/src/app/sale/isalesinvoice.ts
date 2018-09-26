export interface ISalesInvoice {
    id: number,
    date: Date,
    invoiceNumber: number,
    isReverseChargeApplicable:boolean,
    customerId?: number,
    customerName: string;
    billingAddress: string;
    isPlaceOfSupplyDifferent:boolean,
    placeOfSupply: string,
    shippingAddress:string,
    totalTaxableValue:number,
    receivedAmount:number,
    totalTaxAmount:number,
    totalCessAmount:number,
    totalInvoiceValue: number,
    totalDiscount: number,
    totalIgst: number,
    totalCgst: number,
    totalSgst: number,
    reference: string,
    modeOfPayment:string,
    invoicePrefix : string,
    salesInvoiceItems : ISalesInvoiceItem[],

}

export interface ISalesInvoiceItem {
    productId: number;
    quantity: number;
    unitPrice: number;
    discount: number;
    taxRate:number,
    taxableValue: number;
    taxAmount: number;
    cess:number;
    total:number;

}
