

import { OnInit, Component, Input, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { FormArray, Validators } from '@angular/forms';
import { ProductService } from '../../master/products/product.service';
import { IProduct } from '../../master/products/iproduct';
import { IPurchaseNoteInvoiceItem } from '../ipurchase';
import { SelectItem } from 'primeng/api';
import { debounceTime} from 'rxjs/operators/debounceTime';

@Component({
    selector: 'app-purchase-invoice-items',
    templateUrl: './purchase-invoice-items.component.html',
    styleUrls: ['./purchase-invoice-items.component.css']
})
export class PurchaseInvoiceItemsComponent implements OnInit {

    
    calculateTaxableValue(): void {
        let product = this.products.find(p => p.id == this.itemForm.get('productId').value);
        let unitPrice = this.itemForm.get('unitPrice').value;
        let quantity = this.itemForm.get('quantity').value;
        let dis = this.itemForm.get('discount').value;
        if (product.isTaxIncluded) {
            let taxableValue = (quantity * unitPrice) * (1 - dis / 100)
            let taxableValueForInclusive = (quantity * unitPrice) * (1 - dis / 100) / (1 + (+product.igst / 100) + (+product.cess / 100))
            this.itemForm.patchValue({
                taxableValue: taxableValueForInclusive,
            });

        }

        else {
            let taxableValue = (quantity * unitPrice) * (1 - dis / 100);
            this.itemForm.patchValue({
                taxableValue: taxableValue,
            });
        }
    }

    calculateTaxAmount(): void {
        let taxableValue = this.itemForm.get('taxableValue').value;
        let product = this.products.find(p => p.id == this.itemForm.get('productId').value);
        let igstAmount: number;
        let cgstAmount: number;
        let sgstAmount: number;
        let cessAmount: number;




        {
            igstAmount = !this.isIntraState ? (taxableValue * +product.igst / 100) : 0;
            cgstAmount =  this.isIntraState ? (taxableValue * +product.igst / 100) / 2 : 0;
            sgstAmount = this.isIntraState ? (taxableValue * +product.igst / 100) / 2 : 0;
            cessAmount = taxableValue * +product.cess / 100
        }
        this.itemForm.patchValue({
            igstAmount: igstAmount,
            cgstAmount: cgstAmount,
            sgstAmount: sgstAmount,
            cessAmount: cessAmount,

        });
    }
   

    calculateTotal(): void {
        
        let taxableValue = this.itemForm.get('taxableValue').value
        let igstAmount = this.itemForm.get('igstAmount').value
        let cgstAmount = this.itemForm.get('cgstAmount').value
        let sgstAmount = this.itemForm.get('sgstAmount').value
        let cessAmount = this.itemForm.get('cessAmount').value
        let total = +taxableValue + +igstAmount + +cessAmount + +cgstAmount + +sgstAmount;
        this.itemForm.patchValue({
                 total: total })
    
        
       


    }





    




    constructor(private productService: ProductService, private fb: FormBuilder) { }
    productSelectList: SelectItem[];
    products:IProduct[];
    itemForm: FormGroup;
    purchaseItem: IPurchaseNoteInvoiceItem;
    @Input() invoiceItems: FormArray;
    @Input() isIntraState: boolean;

    @Output() addItem: EventEmitter<IPurchaseNoteInvoiceItem> = new EventEmitter<IPurchaseNoteInvoiceItem>();
    @Output() removeItemIndex: EventEmitter<number> = new EventEmitter<number>();

    ngOnInit() {
        // this.productService.getAll().subscribe(products => this.products = products);
        this.itemForm = this.fb.group({
            productId: ['', [Validators.required]],
            unitPrice: ['', [,]],
            quantity: ['', [,]],
            discount: [0, [,]],
            cessAmount: ['', [,]],
            taxableValue: ['', [,]],
            taxRate: ['', [,]],
            cessRate: '',
            // taxAmount: ['',],
            igstAmount: ['', []],
            sgstAmount: ['', []],
            cgstAmount: ['', []],
            total: ['', [,]],
            // taxAmount: ['',],

        });
        
        this.itemForm.get('unitPrice').valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateTaxableValue());
        this.itemForm.get('discount').valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateTaxableValue());
        this.itemForm.get('quantity').valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateTaxableValue());
        this.itemForm.get('taxableValue').valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateTaxAmount());
        this.itemForm.get('cgstAmount').valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateTotal());
        this.itemForm.get('sgstAmount').valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateTotal());
        this.itemForm.get('igstAmount').valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateTotal());
        this.itemForm.get('cessAmount').valueChanges.pipe(debounceTime(1000)).subscribe(() => this.calculateTotal());

       

          
        this.productService.getAll().subscribe(products => {
            this.products = products.filter(pro => pro.isPurchaseable);
            this.productSelectList = this.products.map(pl =>({
              label : pl.name,
              value : pl.id
            }));
          });
    }



    getProductName(id: number): string {
        if (this.products) {
            return this.products.find(p => p.id == id).name;
        }
    }
    

    addProductLine(): void {
        let item = Object.assign({}, this.itemForm.value);
        this.addItem.emit(item)
        this.itemForm.patchValue({
            //  productId:0,
            taxableValue: null,
            igst: null,
            sgst: null,
            quantity: null,
            unitPrice: null,
            cess: null,
            discount: 0,
            cgst: null,
          

        });


    }


    getTaxRate(): void {
        let product = this.products.find(p => p.id == this.itemForm.get('productId').value);

        if (product) {
            this.itemForm.patchValue({
                taxRate: product.igst,
                cessRate: product.cess,

            });
        }
    }
}


