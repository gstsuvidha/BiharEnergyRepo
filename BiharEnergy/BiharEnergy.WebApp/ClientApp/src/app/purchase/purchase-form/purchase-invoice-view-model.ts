import { IProduct } from "../../master/products/iproduct";
import { Result } from "../../shared/Utilities/result";
import { IPurchaseInvoice } from "../ipurchase";


export class PurchaseInvoiceViewModel {
//   private _id: number;
//   get id() {
//     return this._id;
//   }
//   set id(value: number) {
//     this._id = value;
//   }

//   private _date: Date;
//   get date() {
//     return this._date;
//   }
//   set date(value: Date) {

//     this._date = value;
//   }

  
//   private _invoiceNumber: string;
//   get invoiceNumber() {
//     return this._invoiceNumber;
//   }
//   set invoiceNumber(value: string) {
//     this._invoiceNumber = value;
//   }

//   private _isReverseChargeApplicable: boolean;
//   get isReverseChargeApplicable() {
//     return this._isReverseChargeApplicable;
//   }
//   set isReverseChargeApplicable(value: boolean) {
//     this._isReverseChargeApplicable = value;
//   }


//   private _supplierId: string;
//   get supplierId() {
//     return this._supplierId;
//   }
//   set supplierId(value: string) {
//     this._supplierId = value;
//   }


//   // private _billingAddress: string;
//   // get billingAddress() {
//   //   return this.billingAddress;
//   // }
//   // set billingAddress(value: string) {
//   //   this._billingAddress = value;
//   // }



//   private _placeOfSupply: string;
//   get placeOfSupply() {
//     return this._placeOfSupply;
//   }
//   set placeOfSupply(value: string) {
//     this._placeOfSupply = value;
//   }

//   private _shippingAddress: string;
//   get shippingAddress() {
//     return this._shippingAddress;
//   }
//   set shippingAddress(value: string) {
//     this._shippingAddress = value;
//   }

//   private _reference: string;
//   get reference() {
//     return this._reference;
//   }
//   set reference(value: string) {
//     this._reference = value;
//   }

//   // private _receivedAmount: number;
//   // get receivedAmount() {
//   //   return this._receivedAmount;
//   // }
//   // set receivedAmount(value: number) {
//   //   this._receivedAmount = value;
//   // }

//   get totalInvoiceValue(): number {
//     // return this._items
//     //               .map(itm => itm.totalValue)
//     //               .reduce((sum,current)=> sum + current);

//     var total = 0;
//     if (this._items != null && this._items.length > 0) {
//       this._items.forEach(itm => total += itm.total);
//     }
//     return total;
//   }
//   private readonly _items: PurchaseInvoiceItemViewModel[];
//   get items() {
//     return this._items;
//   }

//   public constructor() {
//     this._items = [];
//   }

//   isValid(): boolean {
//     let result = true;

//     if (!this.id)
//       result = false;

//     if (!this.placeOfSupply)
//       result = false;

//     if (!this.invoiceNumber)
//       result = false;

//     if (!this.reference)
//       result = false;

//     if (!this.shippingAddress) {
//       // if (this.di) {
//       result = false;
//       // }
//     }


//     if (this.items.length == 0)
//       result = false;


//     return result;
//   }



//   addItem(product: IProduct, quantity: number, unitPrice: number, discount: number, taxableValue:number,
//     igstAmount: number, cgstAmount: number, sgstAmount: number, cessAmount: number,
//     itcPercentage: number): Result {

//     if (unitPrice == 0)
//       return Result.Fail("Rate cannot be zero");

//     this._items.push(new PurchaseInvoiceItemViewModel(product.id, product.isTaxIncluded, product.igst, product.cess,
//                                                   quantity, unitPrice, discount, taxableValue,
//                                                     igstAmount, cgstAmount, sgstAmount, cessAmount,
//                                                      itcPercentage));
//     return Result.Ok();
//   }




//   removeItem(index: number): any {
//     if (index > -1) {
//       this._items.splice(index, 1);
//     }
//   }

//   // update(index: number, item: SalesInvoiceItemViewModel): any {
//   //   // var itm = this._items.(item);
//   //   // itm.update(item);

//   // }


//   reset(): void {
//     this.id = null;
//     this.placeOfSupply = null;
//     this.supplierId = null;
//     // this.billingAddress = null;
//     this.date = null;
//     this.invoiceNumber = null;
//     this.isReverseChargeApplicable = false;
//     this.reference = null;
//     this.shippingAddress = null;
//     // this._items.length = 0;
//   }


//   patch(purchaseInvoice: IPurchaseInvoice): void {
//     this.id = purchaseInvoice.id;
//     this.date = purchaseInvoice.date;
//         this.invoiceNumber = purchaseInvoice.invoiceNumber;
//     this.isReverseChargeApplicable = purchaseInvoice.isReverseChargeApplicable;
//     this.supplierId = purchaseInvoice.supplierId;
//     this.placeOfSupply = purchaseInvoice.placeOfSupply;
//     this.shippingAddress = purchaseInvoice.shippingAddress;
//     // this.billingAddress = purchaseInvoice.billingAddress;
//     this.reference = purchaseInvoice.reference;


//     purchaseInvoice.purchaseInvoiceItems.forEach(el => {
//       this._items.push(new PurchaseInvoiceItemViewModel(el.productId, false, el.taxRate, el.taxRate, 
//          el.quantity, el.unitPrice, el.discount, el.taxableValue,
//         el.igstAmount, el.cgstAmount, el.sgstAmount, el.cessAmount, el.itcPercentage))
//     });
//     // throw new Error("Method not implemented.");
//   }




//   copy(): IPurchaseInvoice {
//     return {
//       id: 0,
//       date: this.date,
//       invoiceNumber: this.invoiceNumber,
//       isReverseChargeApplicable: this.isReverseChargeApplicable,
//       supplierId: this.supplierId,
//       placeOfSupply: this.placeOfSupply,
//       shippingAddress: this.shippingAddress,
//       totalInvoiceValue: this.totalInvoiceValue,
//       reference: this.reference,





//       // totalTaxableValue: 0,
//       // totalTaxAmount: 0,



//       purchaseInvoiceItems: this.items.map(element => ({
//         productId: element.productId,
//         quantity: element.quantity,
//         unitPrice: element.unitPrice,
//         discount: element.discount,
//         taxRate: element.taxRate,
//         taxAmount: element.taxAmount,
//         taxableValue: element.taxableValue,
//         igstAmount: element.igstAmount,
//         cgstAmount: element.cgstAmount,
//         sgstAmount: element.sgstAmount,
//         cessAmount: element.cessAmount,
//         //   itcEligibility: element.itcEligibility,
//         itcPercentage: element.itcPercentage,
//         total: element.total
//       }))
//     }
//   }
// }







// export class PurchaseInvoiceItemViewModel {

//   // public isTaxIncluded: boolean;//Copy from Parent
//   private _productId: number;
//   get productId() {
//     return this._productId;
//   }


//   private _quantity: number;
//   get quantity() {
//     return this._quantity;
//   }
//   set quantity(value: number) {
//     if (!isNaN(value)) {
//       this._quantity = value;
//     }
//   }

  

//   private _unitPrice: number;
//   get unitPrice() {
//     return this._unitPrice;
//   }
//   set unitPrice(value: number) {
//     if (!isNaN(value)) {
//       this._unitPrice = value;
//     }
//   }
//   private _discount: number;
//   get discount() {
//     return this._discount;
//   }
//   set discount(value: number) {
//     this._discount = value;
//   }


//   private _taxableValue: number;
//   get taxableValue() {
//     return this._taxableValue;
//   }
//   set taxableValue(value: number) {
//     if (!isNaN(value)) {
//       this._taxableValue = value;
//     }
//   }
//   // get taxableValue() {

//   //  if (this._isTaxIncluded) {
//   //      return (this._quantity * this._unitPrice) * (1 - this._discount / 100) / (1 + (this._taxRate / 100) + (this._cess / 100))
//   //     }
//   //     else {
//   //       return this._quantity * this._unitPrice * (1 - this._discount / 100);

//   //     }
 
   

//   private _taxRate: number;
//   get taxRate() {
//     return this._taxRate;
//   }
  
//   private _isTaxIncluded: boolean;
//   get isTaxIncluded() {
//     return this._isTaxIncluded;
//   }
  
//   private _cess: number;
//   get cess() {
//     return this._cess;
//   }

//   private _igstAmount: number;
//   get igstAmount() {
//     return this._igstAmount;
//   }
//   set igstAmount(value: number) {
//     if (!isNaN(value)) {
//       this._igstAmount = value;
//     }
//   }

//   private _cgstAmount: number;
//   get cgstAmount() {
//     return this._cgstAmount;
//   }
//   set cgstAmount(value: number) {
//     if (!isNaN(value)) {
//       this._cgstAmount = value;
//     }
//   }

//   private _sgstAmount: number;
//   get sgstAmount() {
//     return this._sgstAmount;
//   }
//   set sgstAmount(value: number) {
//     if (!isNaN(value)) {
//       this._sgstAmount = value;
//     }
//   }

//   private _cessAmount: number;
//   get cessAmount() {
//     return this._cessAmount;
//   }
//   set cessAmount(value: number) {
//     if (!isNaN(value)) {
//       this._cessAmount = value;
//     }
//   }

//   // private _itcEligibility: string;
//   // get itcEligibility() {
//   //   return this._itcEligibility;
//   // }
//   // set itcEligibility(value: string) {

//   //     this._itcEligibility = value;

//   // }

//   private _itcPercentage: number;
//   get itcPercentage() {
//     return this._itcPercentage;
//   }
//   set itcPercentage(value: number) {
//     if (!isNaN(value)) {
//       this._sgstAmount = value;
//     }
//   }

//   get taxAmount() {

//     return this.taxableValue * this._taxRate / 100;

//   }

//   // private _total: number;
//   // get total() {
//   //   return this._total;
//   // }
//   // set total(value: number) {
//   //   if (!isNaN(value)) {
//   //     this._sgstAmount = value;
//   //   }
//   // }

//   private _total: number;
//   get total() {
//     return this._taxableValue + this._igstAmount + this._cgstAmount + this._cess + this.sgstAmount;
//   }

//   public constructor(productId: number, isTaxIncluded:boolean, taxRate: number, cess:number,
//                       quantity: number, unitPrice: number, discount: number, taxableValue: number,
//                       igstAmount: number, cgstAmount: number, sgstAmount: number, 
//                       cessAmount: number,itcPercentage: number

//   ) {
//     this._productId = productId;
//     this._isTaxIncluded = isTaxIncluded;
//     this._taxRate = taxRate;
//     this._cess = cess;
//     this._quantity = quantity;
//     this._unitPrice = unitPrice;
//     this._discount = discount;
//     this._taxableValue = taxableValue;
//     this._igstAmount = igstAmount;
//     this._cgstAmount = cgstAmount;
//     this._sgstAmount = sgstAmount;
//     this._cessAmount = cessAmount;

//     //   this._itcEligibility= itcEligiibility;
//     this._itcPercentage = itcPercentage;




//   }




}



