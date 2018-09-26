/* import { IProduct } from './../master/products/iproduct';
import { ISalesInvoice } from './isalesinvoice';
import { Result } from '../shared/Utilities/result';
export class SalesInvoiceViewModel {



  private _id: number;
  get id() {
    return this._id;
  }
  set id(value: number) {
    this._id = value;
  }

  private _invoiceNumber: number;
  get invoiceNumber() {
    return this._invoiceNumber;
  }
  set invoiceNumber(value: number) {
    this._invoiceNumber = value;
  }

  // private _invoiceDate: number;
  // get invoiceDate() {
  //   return this._invoiceDate;
  // }
  // set invoiceDate(value: number) {
  //   this._invoiceDate = value;
  // }f

  private _isReverseChargeApplicable: boolean;
  get isReverseChargeApplicable() {
    return this._isReverseChargeApplicable;
  }
  set isReverseChargeApplicable(value: boolean) {
    this._isReverseChargeApplicable = value;
  }

  private _isPlaceOfSupplyDifferent: boolean;
  get isPlaceOfSupplyDifferent() {
    return this._isPlaceOfSupplyDifferent;
  }
  set isPlaceOfSupplyDifferent(value: boolean) {
    this._isPlaceOfSupplyDifferent = value;
  }

  private _customerId: number;
  get customerId() {
    return this._customerId;
  }
  set customerId(value: number) {
    this._customerId = value;
  }

  private _customerName: string;
  get customerName() {
    return this._customerName;
  }
  set customerName(value: string) {
    this._customerName = value;
  }

  private _billingAddress: string;
  get billingAddress() {
    return this._billingAddress;
  }
  set billingAddress(value: string) {
    this._billingAddress = value;
  }

 
  private _date: Date;
  get date() {
    return this._date;
  }
  set date(value: Date) {
    this._date = value;
  }

  private _placeOfSupply: string;
  get placeOfSupply() {
    return this._placeOfSupply;
  }
  set placeOfSupply(value: string) {
    this._placeOfSupply = value;
  }

  private _shippingAddress: string;
  get shippingAddress() {
    return this._shippingAddress;
  }
  set shippingAddress(value: string) {
    this._shippingAddress = value;
  }

  private _reference: string;
  get reference() {
    return this._reference;
  }
  set reference(value: string) {
    this._reference = value;
  }

  private _receivedAmount: number;
  get receivedAmount() {
    return this._receivedAmount;
  }
  set receivedAmount(value: number) {
    this._receivedAmount = value;
  }

  get totalInvoiceValue(): number {
    // return this._items
    //               .map(itm => itm.totalValue)
    //               .reduce((sum,current)=> sum + current);

    var total = 0;
    if (this._items != null && this._items.length > 0) {
      this._items.forEach(itm => total += itm.total);
    }
    return total;
  }
  private readonly _items: SalesInvoiceItemViewModel[];
  get items() {
    return this._items;
  }

  public constructor() {
    this._items = [];
  }

  isValid(): boolean {
    let result = true;

    if (!this.invoiceNumber)
      result = false;

    if (!this.reference)
      result = false;


    if (this.items.length == 0)
      result = false;


    return result;
  }



  addItem(product: IProduct, quantity: number, unitPrice: number, discount: number): Result {
    if (unitPrice == 0)
      return Result.Fail("Rate cannot be zero");

    this._items.push(new SalesInvoiceItemViewModel(product.id,product.isTaxIncluded,product.igst,product.cess, quantity, unitPrice, discount));
    return Result.Ok();
  }




  removeItem(index: number): any {
    if (index > -1) {
      this._items.splice(index, 1);
    }
  }

  // update(index: number, item: SalesInvoiceItemViewModel): any {
  //   // var itm = this._items.(item);
  //   // itm.update(item);

  // }


  reset(invoiceNumber:number): void {
    this.id = null;
    this.placeOfSupply = null;
    this.customerId = null;
    this.billingAddress = null;
    this.customerName = null;
    this.date = null;
    this.invoiceNumber = invoiceNumber;
    this.isPlaceOfSupplyDifferent = false;
    this.isReverseChargeApplicable = false;
    this.receivedAmount = 0;
    this.reference = null;
    this.shippingAddress = null;
    this._items.length = 0;
  }

  patch(salesInvoice: ISalesInvoice): void {
    this.id = salesInvoice.id;
    this.placeOfSupply = salesInvoice.placeOfSupply;
    this.customerId = salesInvoice.customerId;
    this.billingAddress = salesInvoice.billingAddress;
    this.customerName = salesInvoice.customerName;
    this.date = salesInvoice.date;
    this.invoiceNumber = salesInvoice.invoiceNumber;
    this.isPlaceOfSupplyDifferent = salesInvoice.isPlaceOfSupplyDifferent;
    this.isReverseChargeApplicable = salesInvoice.isReverseChargeApplicable;
    this.receivedAmount = salesInvoice.receivedAmount;
    this.reference = salesInvoice.reference;
    this.shippingAddress = salesInvoice.shippingAddress;

    salesInvoice.salesInvoiceItems.forEach(el => {
      this._items.push(new SalesInvoiceItemViewModel(el.productId,false,el.taxRate,el.taxRate, el.taxRate, el.quantity, el.discount ))
    });
    // throw new Error("Method not implemented.");
  }

  copy(): ISalesInvoice {
    return {
      id: 0,
      customerId: this.customerId,
      placeOfSupply: this.placeOfSupply,
      billingAddress: this.billingAddress,
      customerName: this.customerName,
      date: this.date,
      invoiceNumber: this.invoiceNumber,
      isPlaceOfSupplyDifferent: this.isPlaceOfSupplyDifferent,
      isReverseChargeApplicable: this.isReverseChargeApplicable,
      receivedAmount: this.receivedAmount,
      reference: this.reference,
      totalTaxableValue: 0,
      totalTaxAmount: 0,
      shippingAddress: this.shippingAddress,
      totalInvoiceValue: this.totalInvoiceValue,
      salesInvoiceItems: this.items.map(element => ({
        productId: element.productId,
        quantity: element.quantity,
        unitPrice: element.unitPrice,
        discount: element.discount,
        taxRate: element.taxRate,
        taxAmount: element.taxAmount,
        taxableValue: element.taxableValue,
        cess: element.cess,
        total: element.total
      }))
    }
  }
}




export class SalesInvoiceItemViewModel {

  // public isTaxIncluded: boolean;//Copy from Parent
  private _productId: number;
  get productId() {
    return this._productId;
  }


  private _quantity: number;
  get quantity() {
    return this._quantity;
  }
  set quantity(value: number) {
    if (!isNaN(value)) {
      this._quantity = value;
    }
  }

  private _unitPrice: number;
  get unitPrice() {
    return this._unitPrice;
  }
  set unitPrice(value: number) {
    if (!isNaN(value)) {
      this._unitPrice = value;
    }
  }

  private _discount: number;
  get discount() {
    return this._discount;
  }
  set discount(value: number) {
    this._discount = value;
  }

  get taxableValue() {
    if (this._isTaxIncluded){
    return ((this._quantity * this.unitPrice) * (1-(this._discount/100)))/ (1+this._taxRate/100);
    }
    else{
      return (this._quantity * this._unitPrice) * (1-this._discount/100);

    }
  }

  private _taxRate: number;
  get taxRate() {
    return this._taxRate;
  }

  private _cess: number;
  get cess() {
    return this._cess;
  }
 
  private _isTaxIncluded: boolean;
  get isTaxIncluded() {
    return this._isTaxIncluded;
  }


  

  get taxAmount() {
    
return this.taxableValue * this._taxRate / 100;

  }

  private _total: number;
  get total() {
    return this.taxableValue + this.taxAmount;
  }

  public constructor(productId: number,isTaxIncluded:boolean,taxRate:number, cess:number, quantity: number, unitPrice: number, discount: number, ) {
    this._productId = productId;
    this._isTaxIncluded = isTaxIncluded;
    this._taxRate = taxRate;
    this._cess = cess;
    this._quantity = quantity;
    this._unitPrice = unitPrice;
    this._discount = discount;
  }



}
 */