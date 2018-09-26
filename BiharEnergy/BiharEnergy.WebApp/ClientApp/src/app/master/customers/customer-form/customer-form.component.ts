import { ICustomer, RegistrationType } from '../icustomer';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators, } from '@angular/forms';

import { Router, ActivatedRoute } from '@angular/router';

import { Message } from 'primeng/components/common/api';
import { CustomerService } from '../customer.service';
import { StateList } from '../../../shared/state-list';
import {SelectItem} from 'primeng/api';


@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit {

  private _id: number;
  get id(): number{
    return this._id;
  }

 @Input()
  set id(value: number){
      this._id = value;

      if(this._id !=null){
        console.log(this._id)
        this.getCustomer(this._id);
        
      }
  }

  @Output() closeDialog:EventEmitter<any> = new EventEmitter<any>();
  @Output() refreshList:EventEmitter<boolean> = new EventEmitter<boolean>();
 
  pageTitle;
  customer:ICustomer;
  customerForm: FormGroup;
  stateList = StateList;
  msgs:Message[] = [];
  cols : any[];
  displayDialog : boolean = false;
  registrationType : SelectItem[];
  busy : boolean;

  constructor(private fb: FormBuilder,
              private customerService: CustomerService,
              private router: Router,
              private route: ActivatedRoute,
              ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.customerForm = this.newForm();
    this.registrationType = [
      {label : "Registered", value : 0},
      {label : "Unregistered", value : 1},
      {label : "Composite Dealer", value : 2},
    ]
   
  }

  
  newForm():FormGroup{
    return this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
       gstin: ['', [Validators.required, Validators.pattern('[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}')]],
      address: ['', [Validators.required, Validators.minLength(2)]],
      //  customerOpeningDate: [new Date(), [Validators.required]],
       state: ['', [Validators.required]],
      contactNumber: [''],
       registrationType: [0, Validators.required],
       email : ['']
      //  openBalance: [0,Validators.required],
  });
}

private getCustomer(this_id):void{
  this.customerService.getOne(this.id)
  .subscribe((customer:ICustomer)=> this.onCustomerRetrieved(customer)
  );
}

 checkRegistrationType(): void {
  let type: RegistrationType = this.customerForm.get('registrationType').value;
  if (type == RegistrationType.Unregistered) {
      this.customerForm.get('gstin').setValidators(null);
      this.customerForm.get('gstin').setValue("");
  }
  else {
      this.customerForm.get('gstin')
          .setValidators(Validators.compose(
              [Validators.pattern('[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}'), Validators.required]));
  }
  this.customerForm.get('gstin').updateValueAndValidity();


}
private onCustomerRetrieved(customer:ICustomer): void{
  this.displayDialog = true;
  this.customer = customer;

   if(this.id == 0){
     this.customerForm = this.newForm();
     this.pageTitle = 'Add Customer';
     console.log("add")
   }
   else
   {
    this.pageTitle = `Edit Customer: ${this.customer.name}`;
    //  let opDate = new Date(this.customer.customerOpeningDate);
    // Update the data on the form
    this.customerForm.patchValue({
        name: this.customer.name,
        // customerOpeningDate: new Date(opDate.getTime() + Math.abs(opDate.getTimezoneOffset() * 60000)),

        gstin: this.customer.gstin,
        address: this.customer.address,
        state: this.customer.state,
        contactNumber: this.customer.contactNumber,
        registrationType: this.customer.registrationType,
        email : this.customer.email
        // openBalance: this.customer.openBalance,

    
});
this.checkRegistrationType();

  }


}

saveCustomer(): void {

  if (this.customerForm.dirty && this.customerForm.valid) {

      let customerToSave = Object.assign({}, this.customer, this.customerForm.value);
      this.busy = true;
      this.customerService.save(customerToSave, this.id).subscribe(()=> this.onSaveComplete());
           }


  else if (!this.customerForm.dirty) {
      this.onSaveComplete();
  }
}

private onSaveComplete():void{
  const displayMsg = this.id == 0 ? 'Saved' : 'Updated';
  this.msgs = [];
  this.msgs.push({
    severity : 'success',
    summary : 'Success Message',
    detail : 'Customer Sucessfully' + displayMsg
  });
  // this.router.navigate(['/customer']);
  this.refreshList.emit(true);
  this.displayDialog=false;
  this.closeDialog.emit(null);
}

disable(){
  this.busy = false;
}

}





