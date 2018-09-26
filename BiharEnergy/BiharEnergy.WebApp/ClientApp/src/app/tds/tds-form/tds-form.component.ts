import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Itds } from '../Itds';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StateList } from '../../shared/state-list';

import { SelectItem, Message } from 'primeng/api';
import { TdsService } from '../tds.service';
import { Router, ActivatedRoute } from '@angular/router';
import { SupplierService } from '../../master/suppliers/supplier.service';
import { ISupplier, RegistrationType } from '../../master/suppliers/isupplier';
import { UserProfileService } from '../../user-profile/userprofile.service';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-tds-form',
  templateUrl: './tds-form.component.html',
  styleUrls: ['./tds-form.component.css']
})
export class TdsFormComponent implements OnInit {

    suppliers: ISupplier[];
    supplierSelectList: SelectItem[];
    isIntraState: boolean = true;
    accountingUnitPlaceOfSupply: number;


  private _id: number;
  get id(): number{
    return this._id;
  }

 @Input()
  set id(value: number){
      this._id = value;

      if(this._id !=null){
        console.log(this._id)
        this.getTds(this._id);
        
      }
  }

  @Output() closeDialog:EventEmitter<any> = new EventEmitter<any>();
  @Output() refreshList:EventEmitter<boolean> = new EventEmitter<boolean>();
 
  pageTitle;
  tds:Itds;
  tdsForm: FormGroup;
  stateList = StateList;
  msgs:Message[] = [];
  cols : any[];
  displayDialog : boolean = false;
  registrationType : SelectItem[];
  busy : boolean;

  constructor(private fb: FormBuilder,
              private tdsService: TdsService,
              private router: Router,
              private userProfileService: UserProfileService,
              private route: ActivatedRoute,
              private supplierService: SupplierService
              ) { }

  ngOnInit() {


    this.tdsForm = this.fb.group({
      supplierId: [0, ],
       placeOfSupply: ['', ],
       date:[new Date()],
      amountPaid: [0 ],
      //  customerOpeningDate: [new Date(), [Validators.required]],
       cgstAmount: [0] ,
      sgstAmount: [0],
       igstAmount: [0],
       tdsAmount: [0],
       netAmount: [0]
      //  openBalance: [0,Validators.required],
  });

  this.tdsForm.valueChanges.pipe(debounceTime(200)).subscribe(() => this.tdsCalculate());
  
  

    

    this.supplierService.getAll().subscribe(suppliers => {
      this.suppliers = suppliers
      this.supplierSelectList = this.suppliers.map(cl => (
          {
              label: cl.name,
              value: cl.id
          }));
  });

  
  // .filter(acc => acc.nature.toString() == Nature[Nature.Cash]
  // || acc.nature.toString() == Nature[Nature.Bank]));


    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    
   
  

  
 

}


private getTds(this_id):void{
  this.tdsService.getOne(this.id)
  .subscribe((tds:Itds)=> this.onTdsRetrieved(tds)
  );
}


checkSupplyType(event) {

  let pos = this.tdsForm.get('placeOfSupply').value;
  
  let ch = this.tdsForm.get('supplierId').value;


  let supplierPos = this.suppliers.find(p => p.id == ch);
  

  if (supplierPos) {

      if (pos == supplierPos.state)
          this.isIntraState = true
      //  this.isPlaceOfSupplyDifferent = true

      else
          this.isIntraState = false
      console.log(this.isIntraState)
  }
  else {

      if (this.accountingUnitPlaceOfSupply == pos)
          this.isIntraState = true
      else
          this.isIntraState = false
      console.log(this.isIntraState)
  }
  // this.recalculate();
}


 tdsCalculate(){
 let amountPaid = this.tdsForm.get('amountPaid').value;
 console.log(amountPaid);
 let cgstAmount: number;
 let sgstAmount: number;
 let igstAmount: number;
 let tdsAmount: number;
 
 {
 cgstAmount = this.isIntraState ? amountPaid * 1/100 : 0;
 sgstAmount = this.isIntraState ? amountPaid * 1/100 : 0;
 igstAmount = !this.isIntraState ? amountPaid * 2/100 : 0;
 tdsAmount =  this.isIntraState ? amountPaid * 2/100 : amountPaid * 2/100;
 }

 this.tdsForm.patchValue({
  cgstAmount : cgstAmount,
  sgstAmount : sgstAmount,
  igstAmount : igstAmount,
  tdsAmount :  tdsAmount,
  

 })

}






private onTdsRetrieved(tds:Itds): void{
  this.displayDialog = true;
  this.tds = tds;

   if(this.id == 0){
     this.tdsForm.reset();
     this.tdsForm.patchValue({
       date: new Date()
     })
     this.pageTitle = 'Add TDS Details';
     console.log("add")

     this.userProfileService.getProfile().subscribe(up => {

      this.accountingUnitPlaceOfSupply = up.placeOfSupply;
      console.log(this.accountingUnitPlaceOfSupply);

  this.tdsForm.patchValue({
      placeOfSupply: this.accountingUnitPlaceOfSupply
  });
   });

   }
   else
   {
    this.pageTitle = `Edit Tds: ${this.tds.id}`;
     let patchDate = new Date(this.tds.date);
    // Update the data on the form
    this.tdsForm.patchValue({
        supplierId: this.tds.supplierId,
        date: new Date(patchDate.getTime() + Math.abs(patchDate.getTimezoneOffset() * 60000)),
        placeOfSupply: this.tds.placeOfSupply,
        amountPaid: this.tds.amountPaid,
        cgstAmount: this.tds.cgstAmount,
        sgstAmount: this.tds.sgstAmount,
        igstAmount: this.tds.igstAmount,
        tdsAmount: this.tds.tdsAmount,
        netAmount: this.tds.netAmount
        

    
});
this.userProfileService.getAccountingUnits().subscribe(up => {

  this.accountingUnitPlaceOfSupply = up.placeOfSupply;
  this.checkSupplyType(event);

});


  }


  this.supplierService.getAll().subscribe(suppliers => {
    this.suppliers = suppliers;
    this.supplierSelectList = this.suppliers.map(cl => (
        {
            label: cl.name,
            value: cl.id
        }));
});

}



saveTds(): void {

  if (this.tdsForm.dirty && this.tdsForm.valid) {

      let tdsToSave = Object.assign({}, this.tds, this.tdsForm.value);
      this.busy = true;
      this.tdsService.save(tdsToSave, this.id).subscribe(()=> this.onSaveComplete());
      
           }


  else if (!this.tdsForm.dirty) {
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
