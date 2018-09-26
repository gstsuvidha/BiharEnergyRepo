
export interface IPurchaseInvoice {
id: number,
date: Date,
postingDate: Date,
invoiceNumber: string,
supplierId: string,
purchaseInvoiceType: string,

isReverseChargeApplicable:boolean,

placeOfSupply: string,

shippingAddress:string,

totalTaxableValue: number,
totalTaxAmount: number,
totalInvoiceValue: number,
totalCessAmount: number,
totalIgst: number,
totalCgst: number,
totalSgst: number,
reference: string,
billedPassed : boolean,


purchaseInvoiceItems:IPurchaseNoteInvoiceItem[]
}
  
export interface IPurchaseNoteInvoiceItem {

    productId: number;
      quantity: number;
      unitPrice: number;
      discount: number;
      igstAmount:number;
      taxableValue: number;
      cgstAmount:number;
      cessAmount:number;
      sgstAmount:number;
      total:number;
      taxRate:number,
  }