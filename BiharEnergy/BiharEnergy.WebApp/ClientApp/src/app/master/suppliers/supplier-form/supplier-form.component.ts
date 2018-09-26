import { ISupplier, RegistrationType } from '../isupplier';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StateList } from '../../../shared/state-list';
import { SupplierService } from '../supplier.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Message } from 'primeng/components/common/api';
import {SelectItem} from 'primeng/api';

@Component({
  selector: 'app-supplier-form',
  templateUrl: './supplier-form.component.html',
  styleUrls: ['./supplier-form.component.css']
})
export class SupplierFormComponent implements OnInit {

  private _id: number;
  get id(): number{
    return this._id;
  }

 @Input()
  set id(value: number){
      this._id = value;

      if(this._id !=null){
        console.log(this._id)
        this.getSupplier(this._id);
        
      }
  }
  @Output() closeDialog:EventEmitter<any> = new EventEmitter<any>();
  @Output() refreshList:EventEmitter<boolean> = new EventEmitter<boolean>();
 
  pageTitle;
  supplier:ISupplier;
  supplierForm: FormGroup;
  stateList = StateList;
  msgs:Message[] = [];
  cols : any[];
  displayDialog : boolean = false;
  registrationType : SelectItem[];
  busy : boolean;

  constructor(private fb: FormBuilder,
              private supplierService: SupplierService,
              private router: Router,
              private route: ActivatedRoute,
              ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
     
    this.registrationType = [
      {label : "Registered", value : 0},
      {label : "Unregistered", value : 1},
      {label : "Composite Dealer", value : 2},
    ]
    this.supplierForm = this.newForm();
   
  }

  
  newForm():FormGroup{
    return this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
       gstin: ['', [Validators.required, Validators.pattern('[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}')]],
      address: ['', [Validators.required, Validators.minLength(2)]],
      //  supplierOpeningDate: [new Date(), [Validators.required]],
       state: ['', [Validators.required]],
      contactNumber: [''],
       registrationType: [0, Validators.required],
       email : ['']
      //  openBalance: [0,Validators.required],
  });
}

private getSupplier(this_id):void{
  this.supplierService.getOne(this.id)
  .subscribe((supplier:ISupplier)=> this.onSupplierRetrieved(supplier)
  );
}

 checkRegistrationType(): void {
  let type: RegistrationType = this.supplierForm.get('registrationType').value;
  if (type == RegistrationType.Unregistered) {
      this.supplierForm.get('gstin').setValidators(null);
      this.supplierForm.get('gstin').setValue("");
  }
  else {
      this.supplierForm.get('gstin')
          .setValidators(Validators.compose(
              [Validators.pattern('[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}'), Validators.required]));
  }
  this.supplierForm.get('gstin').updateValueAndValidity();


}


private onSupplierRetrieved(supplier:ISupplier): void{
  this.displayDialog = true;
  this.supplier = supplier;

  if(this.id == 0){
    this.supplierForm = this.newForm();
    console.log("add");
    this.pageTitle = 'Add Supplier';
  }
  else
  {
    this.pageTitle = `Edit Supplier: ${this.supplier.name}`;
    let opDate = new Date(this.supplier.supplierOpeningDate);
    // Update the data on the form
    this.supplierForm.patchValue({
        name: this.supplier.name,
        supplierOpeningDate: new Date(opDate.getTime() + Math.abs(opDate.getTimezoneOffset() * 60000)),

        gstin: this.supplier.gstin,
        address: this.supplier.address,
        state: this.supplier.state,
        contactNumber: this.supplier.contactNumber,
        registrationType: this.supplier.registrationType,
        openBalance: this.supplier.openBalance,
        email : this.supplier.email
    
});
this.checkRegistrationType();
  }


}

saveSupplier(): void {

  if (this.supplierForm.dirty && this.supplierForm.valid) {

      let supplierToSave = Object.assign({}, this.supplier, this.supplierForm.value);
      this.supplierService.save(supplierToSave, this.id).subscribe(()=> this.onSaveComplete());
           }


  else if (!this.supplierForm.dirty) {
      this.onSaveComplete();
  }
}

private onSaveComplete():void{
  const displayMsg = this.id == 0 ? 'Saved' : 'Updated';
  this.msgs = [];
  this.msgs.push({
    severity : 'success',
    summary : 'Success Message',
    detail : 'Supplier Sucessfully' + displayMsg
  });
  // this.router.navigate(['/supplier']);
  this.refreshList.emit(true);
  this.displayDialog=false;
  this.closeDialog.emit(null);
}

disable(){
  this.busy = false;
}

}
