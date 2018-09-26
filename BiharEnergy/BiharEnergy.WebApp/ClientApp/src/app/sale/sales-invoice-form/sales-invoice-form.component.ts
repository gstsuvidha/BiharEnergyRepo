import { CustomerService } from '../../master/customers/customer.service';
import { ICustomer } from '../../master/customers/icustomer';
import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { Message } from 'primeng/components/common/api';
import { ISalesInvoice, ISalesInvoiceItem } from '../isalesinvoice';
import { IProduct } from '../../master/products/iproduct';
import { ActivatedRoute, Router } from '@angular/router';
import { SalesInvoiceService } from '../sales-invoice-service';
import { ProductService } from '../../master/products/product.service';
import { Dropdown } from 'primeng/dropdown';
import { StateList } from '../../shared/state-list';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';
import { LoadingService } from '../../shared/Utilities/loading.service';
import { debounceTime} from 'rxjs/operators/debounceTime';
import { Iuserprofile } from '../../user-profile/iuserprofile';
import { UserProfileService } from '../../user-profile/userprofile.service';

@Component({
  selector: 'app-sales-invoice-form',
  templateUrl: './sales-invoice-form.component.html',
  styleUrls: ['./sales-invoice-form.component.css']
})
export class SalesInvoiceFormComponent implements OnInit {

  msgs: Message[] = [];
  customers: ICustomer[];
  customerSelectList: SelectItem[];
  salesInvoice: ISalesInvoice;
  products : IProduct[];
  productSelectList : SelectItem[];
  stateList = StateList;
  salesForm : FormGroup;
  pageTitle;
  id:number;
  profiles : Iuserprofile[];
  invoicePrefix;
  busy: boolean;
  // invoicePrefix : string;
  
  // public readonly model: SalesInvoiceViewModel;
      
  //  private _id: number;
  //  get id() {
  //    return this._id;
  //  }
  //  set id(value: number) {
  
  //    this._id = value;
  
  //    if (this._id!= null) {
       
      
  //     //  this.salesService.getInvoiceNumber()
  //     //    .subscribe(inv => this.model.reset(inv));

  //      console.log("edit");
  //      this.getSalesInvoice(this._id);
  //    }
  //  }

 
  
  constructor(private route: ActivatedRoute,
              private salesService: SalesInvoiceService,
              private router: Router,
              private customerService: CustomerService,
              private productService:ProductService,
              private fb : FormBuilder,
              private loadingService : LoadingService,
              private userProfileService : UserProfileService) {
  
    // this.model = new SalesInvoiceViewModel();
  }
  
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getSalesInvoice(this.id);
    });
  
    this.invoicePrefix = this.userProfileService.getInvoicePrefix().subscribe(invoicePrefix => this.invoicePrefix = invoicePrefix);
    console.log(this.invoicePrefix);
    this.customerService.getAll().subscribe(customers => {
      this.customers = customers;
      this.customerSelectList = this.customers.map(cl => (
        {
          label: cl.name,
          value: cl.id
        }));
    });
  
    // this.productService.getAll().subscribe(products => {
    //   this.products = products;
    //   this.productSelectList = this.products.map(pl =>({
    //     label : pl.name,
    //     value : pl.id
    //   }));
    // });
   
    // this.userProfileService.getAll().subscribe(profiles => this.profiles = profiles);
    // this.invoicePrefix = this.getInvoicePrefix(this.id);
    this.salesForm = this.newForm();
    this.salesForm.valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateNetAmount());
}

private getSalesInvoice(id: number): void {
  this.salesService.getOne(id)
      .subscribe((salesInvoice: ISalesInvoice) => this.onSalesInvoiceRetrieved(salesInvoice)
      );
}



  newForm():FormGroup{
    return this.fb.group({
      id: [''],
      date: [new Date(), [Validators.required]],


      invoiceNumber: ['', [Validators.required],
          (control: AbstractControl) => {

              return new Promise((resolve, reject) => {
                  setTimeout(() => {
                      this.salesService.isInvoiceNumberUnique(control.value)
                          .subscribe(result => {

                              if (result)
                                  resolve(null);
                              else {
                                  //control.setErrors({ invoiceNumberExist: false });
                                  resolve({
                                      invoiceNumberExist: true
                                  }); // note invoiceNumber has to be same with Exist => invoiceNumberExist
                              }


                          });
                  },
                      3000);
              });

          }
      ],
      isReverseChargeApplicable: [false],
      customerId: [''],
      billingAddress: [],
      customerName: [],
      isPlaceOfSupplyDifferent: [false],
      placeOfSupply: '35',
      shippingAddress: '',
      totalTaxableValue: [0],
      totalTaxAmount: [0],
      totalCessAmount: [0],
      receivedAmount: [0],
      reference: '',
      totalInvoiceValue: [{ value: 0, disabled: true }],
      modeOfPayment: [''],
      salesInvoiceItems: this.fb.array([],Validators.required),
      

    });
  }
  
    calculateNetAmount(): void {
    let itemTotal = 0;
    let totalInvoice = 0;
    let totalTaxableValue = 0;
    let totalTaxAmount = 0;
    // let receivedAmount=0;
    let totalCessAmount = 0;



    for (var i = 0; i < this.salesInvoiceItems.length; i++) {
        var item = this.salesInvoiceItems.at(i);

        itemTotal = +item.get('total').value
        totalInvoice = totalInvoice + +itemTotal
        totalTaxableValue = totalTaxableValue + +item.get('taxableValue').value
        totalTaxAmount = totalTaxAmount + +item.get('taxAmount').value + +item.get('cess').value
        // receivedAmount=totalInvoice
        totalCessAmount = totalCessAmount + +item.get('cess').value



    }
    let totalInvoiceValue = totalInvoice;

    this.salesForm.patchValue({
        totalInvoiceValue: totalInvoiceValue.toFixed(0),
        totalTaxableValue: totalTaxableValue,
        totalTaxAmount: totalTaxAmount,
        totalCessAmount: totalCessAmount,

    });

}

/* getGstin(id: number) {
  if (this.customerSelectList) {
      let gstin = this.customerSelectList.find(p => p.id == id).gstin;
      return gstin;
  }

} */

getProductName(id : number) : string{
  if(this.products){
    return this.products.find(p => p.id == id).name;
  }
}

// getInvoicePrefix(id : number) : string{
//   if(this.profiles){
//     return this.profiles.find(up => up.id == id).invoicePrefix;
//   }
// }

get salesInvoiceItems(): FormArray {
  return <FormArray>this.salesForm.get('salesInvoiceItems');
}


addProductLine(salesInvoiceItem: ISalesInvoiceItem): void {
  this.salesInvoiceItems.push(this.buildProductLine(salesInvoiceItem));
}

removeLine(i: number): void {

  this.salesInvoiceItems.removeAt(i);

}

private onSalesInvoiceRetrieved(salesInvoice: ISalesInvoice): void {

  // if (this.salesForm) {
  //     this.salesForm.reset();
  // }

  this.salesInvoice = salesInvoice;


  if (this.salesInvoice.id == 0) {
      this.salesForm = this.newForm();
      this.pageTitle = 'Sales Invoice';
      this.salesService.getInvoiceNumber()
          .subscribe(inv => this.salesForm.patchValue({  invoiceNumber: inv }));
  }

  else {
      this.pageTitle = `Edit Sales Invoice: ${this.salesInvoice.invoiceNumber}`;
      // Update the data on the form
      let invDate = new Date(this.salesInvoice.date);
      let shipDate = new Date(this.salesInvoice.date);

      this.salesForm.patchValue({
          id: this.salesInvoice.id,
          date: new Date(invDate.getTime() + Math.abs(invDate.getTimezoneOffset() * 60000)),

          invoiceNumber: this.salesInvoice.invoiceNumber,
          isReverseChargeApplicable: this.salesInvoice.isReverseChargeApplicable,
          customerId: this.salesInvoice.customerId == null ? '' : this.salesInvoice.customerId,


          isPlaceOfSupplyDifferent: this.salesInvoice.isPlaceOfSupplyDifferent,
          placeOfSupply: this.salesInvoice.placeOfSupply,
          customerName: this.salesInvoice.customerName,
          billingAddress: this.salesInvoice.billingAddress,
          shippingAddress: this.salesInvoice.shippingAddress,
          totalTaxableValue: this.salesInvoice.totalTaxableValue,
          totalTaxAmount: this.salesInvoice.totalTaxAmount,
          totalCessAmount: this.salesInvoice.totalCessAmount,
          totalInvoiceValue: this.salesInvoice.totalInvoiceValue,
          receivedAmount: this.salesInvoice.receivedAmount,
          reference: this.salesInvoice.reference,
          modeOfPayment: this.salesInvoice.modeOfPayment,




      });
      //this.salesForm.setControl('salesInvoiceItems', this.fb.array(this.salesInvoice.salesInvoiceItems));
      for (let i = 0; i < this.salesInvoice.salesInvoiceItems.length; i++) {
          this.salesInvoiceItems.push(this.buildProductLine(this.salesInvoice.salesInvoiceItems[i]));

      }

      this.salesForm.get('invoiceNumber').disable();
      this.salesForm.get('invoiceNumber').clearAsyncValidators();

  }


}

saveSalesInvoice(): void {

  if (this.salesForm.valid) {

      let p = Object.assign({}, this.salesInvoice, this.salesForm.value);

      this.loadingService.busy = this.salesService.save(p, this.id)
          .subscribe(si => this.onSaveComplete(si.id));
  }


  else if (!this.salesForm.dirty) {
      this.onSaveComplete(this.id);
  }
}

private onSaveComplete(newId: number):void{   
  let displayMsg = this.id == 0 ? " Saved" : " Updated";
  this.msgs = [];
  this.msgs.push({
    severity:'success',
    summary:'Success Message',
    detail:'Sales Invoice Successfully' + displayMsg
  });
  this.router.navigate(['/authenticated/sales/',newId,'print']);
}



private buildProductLine(salesInvoiceItem: ISalesInvoiceItem): FormGroup {
  return this.fb.group({
      productId: [salesInvoiceItem.productId, [Validators.required]],
      quantity: [salesInvoiceItem.quantity, [Validators.required]],
      unitPrice: [salesInvoiceItem.unitPrice, [Validators.required]],
      discount: [salesInvoiceItem.discount, [Validators.required]],
      taxRate: [salesInvoiceItem.taxRate, [Validators.required]],
      taxableValue: [salesInvoiceItem.taxableValue, [Validators.required]],
      taxAmount: [salesInvoiceItem.taxAmount, [Validators.required]],
      cess: [salesInvoiceItem.cess, [Validators.required]],
      total: [salesInvoiceItem.total, [Validators.required]],

  });
}

disable(){
  this.busy = false;
}

} 
  
