import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { IProduct } from '../iproduct';
import { StateList } from '../../../shared/state-list';
import { Message } from 'primeng/components/common/api';
import { ProductService } from '../product.service';
import { UnitList } from '../../../shared/unit-list';
import {SelectItem} from 'primeng/api';


@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {


  private _id: number;
  busy: boolean;
  get id(): number {
    return this._id;
  }

  @Input()
  set id(value: number) {
    this._id = value;

    if (this._id != null) {
      console.log(this._id)
      this.getProduct(this._id);

    }
  }
  @Output() closeDialog:EventEmitter<any> = new EventEmitter<any>();
  @Output() refreshList:EventEmitter<boolean> = new EventEmitter<boolean>();
 
  pageTitle;
  product: IProduct;
  productForm: FormGroup;
  stateList = StateList;
  unitList = UnitList;
  msgs: Message[] = [];
  cols: any[];
  displayDialog : boolean = false;
  category : SelectItem[];
  itcEligibility: SelectItem[];

  constructor(private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.productForm = this.newForm();
    this.category = [
      
      {label : "Goods", value : 1},
      {label : "Service", value : 2}
    ]

      this.itcEligibility = [
        {label: "Input Goods", value: "Input Goods"},
        {label: "Capital Goods", value: "Capital Goods"},
        {label: "Input Service", value: "Input Service"},
        {label: "Not Eligible", value: "Not Eligible"},
      ]
  }


  newForm(): FormGroup {
    return this.fb.group({
      name: ['', [Validators.required]],
      description: ['',],
      unit: ['',Validators.required,],
      unitOthers: ['',],

      rate: ['', [Validators.required,]],
      // stockInHand: 0,
      // opStockDate: [new Date()],
      hsnSacCode: ['',],
      productType: ['', [Validators.required]],
      igst: ['', [Validators.required,]],
      cess: 0,
      itcPercentage: ['100'],
      itcEligibility: [''],
      
      

      // isReverseChargeApplicable: [false],
      isTaxIncluded: [true],
      isSaleable: [true],
      isPurchaseable: [true],

    });
  }






  private getProduct(this_id): void {
    this.productService.getOne(this.id)
      .subscribe((product: IProduct) => this.onProductRetrieved(product)
      );
  }

  private onProductRetrieved(product: IProduct): void {
    this.displayDialog = true;
    this.product = product;

    if (this.id == 0) {
      this.productForm = this.newForm();
      console.log("add");
      this.pageTitle = 'Add Product';
    }
    else {
      this.pageTitle = `Edit Product: ${this.product.name}`;
       let stockDate = new Date(this.product.opStockDate);
      // Update the data on the form
      this.productForm.patchValue({
        name: this.product.name,
        description: this.product.description,
        unit: this.product.unit,
        unitOthers: this.product.unitOthers,
        rate: this.product.rate,
        opStockDate: new Date(stockDate.getTime() + Math.abs(stockDate.getTimezoneOffset() * 60000)),
        stockInHand: this.product.stockInHand,
        hsnSacCode: this.product.hsnSacCode,
        productType: this.product.productType,
        igst: this.product.igst,
        cess: this.product.cess,
        itcPercentage: this.product.itcPercentage,
        itcEligibility: this.product.itcEligibility,
        isReverseChargeApplicable: this.product.isReverseChargeApplicable,
        isTaxIncluded: this.product.isTaxIncluded,
        isSaleable: this.product.isSaleable,
        isPurchaseable: this.product.isPurchaseable,

      })
    }
  }


  itcNotEligible(event):void{
    let itcEligibility = this.productForm.get('itcEligibility').value;
    let itcPercentage = this.productForm.get('itcPercentage').value;
    if(itcEligibility == "Not Eligible")
    {
      this.productForm.patchValue({
        itcPercentage : 0
        
      });
      
    }

    else{
      this.productForm.patchValue({
        itcPercentage : 100
      })
    }
  }


  saveProduct(): void {

    if (this.productForm.dirty && this.productForm.valid) {

      let productToSave = Object.assign({}, this.product, this.productForm.value);
      this.productService.save(productToSave, this.id).subscribe(() => this.onSaveComplete());
    }


    else if (!this.productForm.dirty) {
      this.onSaveComplete();
    }
  }

  private onSaveComplete(): void {
    const displayMsg = this.id == 0 ? 'Saved' : 'Updated';
    this.msgs = [];
    this.msgs.push({
      severity: 'sucess',
      summary: 'Sucess Message',
      detail: 'Product Sucessfully' + displayMsg
    });
   // this.router.navigate(['/product']);
   this.refreshList.emit(true);
  this.displayDialog=false;
  this.closeDialog.emit(null);
  }

  disable(){
    this.busy = false;
  }

}
