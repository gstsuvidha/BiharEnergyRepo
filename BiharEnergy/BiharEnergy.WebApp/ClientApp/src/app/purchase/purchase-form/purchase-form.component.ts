import { PurchaseService } from '../purchase.service';
import { state } from '@angular/animations';
// import { LoadingService } from './../../../shared/utilities/loading.service';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
// import { ToastyService } from 'ng2-toasty';

// import { CustomValidators } from 'ng2-validation';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { IPurchaseInvoice, IPurchaseNoteInvoiceItem } from '../ipurchase';
import { ISupplier } from '../../master/suppliers/isupplier';
import { Message, SelectItem } from 'primeng/api';
import { IProduct } from '../../master/products/iproduct';
import { StateList } from '../../shared/state-list';
import { SupplierService } from '../../master/suppliers/supplier.service';
import { ProductService } from '../../master/products/product.service';
import { LoadingService } from '../../shared/Utilities/loading.service';
import { UserProfileService } from '../../user-profile/userprofile.service';



@Component({
    selector: 'app-purchase-form',
    templateUrl: './purchase-form.component.html',
    styleUrls: ['./purchase-form.component.css']
})
export class PurchaseFormComponent {
    purchaseForm: FormGroup;
    purchasesInvoice: IPurchaseInvoice;
    id: number;
    pageTitle;
    msgs: Message[] = [];
    suppliers: ISupplier[];
    supplierSelectList: SelectItem[];
    purchase: IPurchaseInvoice;
    products: IProduct[];
    productSelectList: SelectItem[];
    stateList = StateList;

    busy: boolean;
    userprofile;
    isIntraState: boolean = true;
    accountingUnitPlaceOfSupply: number;





    // isPlaceOfSupplyDifferent: Subject<any> = new Subject();

    // notifyChild(): void {
    //     this.isPlaceOfSupplyDifferent.next(this.purchaseForm.get('isPlaceOfSupplyDifferent').value);
    // }



    constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router,
        private purchaseInvoiceService: PurchaseService, private supplierService: SupplierService,
        private loadingService: LoadingService,
        private productService: ProductService,
        private userProfileService: UserProfileService,
        // private toastyService: ToastyService
    ) {
    }

    ngOnInit() {
        //        this.supplierService.getAll().subscribe(supplierList => this.supplierSelectList = supplierList);
        // this.productService.getAll().subscribe(productList => this.productSelectList = productList);


        // this.userProfileService.getAccountingUnits().subscribe(up => {

        //     this.accountingUnitPlaceOfSupply = up.placeOfSupply;
        //     this.checkSupplyType();

        // });

        this.supplierService.getAll().subscribe(suppliers => {
            this.suppliers = suppliers;
            this.supplierSelectList = this.suppliers.map(cl => (
                {
                    label: cl.name,
                    value: cl.id
                }));
        });

        // this.productService.getAll().subscribe(products => {
        //     this.products = products;
        //     this.productSelectList = this.products.map(pl => ({
        //         label: pl.name,
        //         value: pl.id
        //     }));
        // });



        this.route.params.subscribe(params => {
            this.id = params['id'];
            this.getPurchasesInvoice(this.id);
        });









        this.purchaseForm = this.fb.group({
            date: [new Date(), [Validators.required]],
            receivingDate: [new Date()],
            postingDate: [new Date()],
            invoiceNumber: ['', [Validators.required]],
            supplierId: ['', [Validators.required]],
            placeOfSupply: '',
            // isPlaceOfSupplyDifferent: false,
            
            totalInvoiceValue: 0,
            roundOf: 0,
            totalTaxAmount: 0,
            totalCessAmount: 0,
            totalTaxableValue: 0,
            reference: '',
            paidAmount: 0,
            purchaseInvoiceItems: this.fb.array([],Validators.required),
            
            billedPassed : true,


        });

        // this.purchaseForm.valueChanges.debounceTime(1000).subscribe(() => {
        //     this.isPaidAmountValid();
        // this.getPaidAmount();


        //    this.purchaseForm.get('totalInvoiceValue').valueChanges.subscribe(() => this.getPaidAmount());

    }



    getProductName(id: number): string {
        if (this.products) {
            return this.products.find(p => p.id == id).name;
        }
    }




    // getAccountingUnitState(id:string):string{

    //     return this.stateList.find(s=>s.id==id).name
    // }

     buildProductLine(purchaseNoteInvoiceItem: IPurchaseNoteInvoiceItem): FormGroup {
        return this.fb.group({
            productId: [purchaseNoteInvoiceItem.productId],
            quantity: [purchaseNoteInvoiceItem.quantity],
            unitPrice: [purchaseNoteInvoiceItem.unitPrice],
            discount: [purchaseNoteInvoiceItem.discount],
            cessAmount: [purchaseNoteInvoiceItem.cessAmount],
            taxableValue: [purchaseNoteInvoiceItem.taxableValue],
            igstAmount: [purchaseNoteInvoiceItem.igstAmount],
            sgstAmount: [purchaseNoteInvoiceItem.sgstAmount],
            cgstAmount: [purchaseNoteInvoiceItem.cgstAmount],
            total: [purchaseNoteInvoiceItem.total],
            taxRate: [purchaseNoteInvoiceItem.taxRate],


        });
    }

    savePurchaseInvoice(): void {
        if (this.purchaseForm.valid) {

            let p = Object.assign({}, this.purchasesInvoice, this.purchaseForm.value);

            this.loadingService.busy = this.purchaseInvoiceService.save(p, this.id)
                .subscribe(() => this.onSaveComplete());

        }


        else if (!this.purchaseForm.dirty) {
            this.onSaveComplete();
        }
    }


    private onSaveComplete(): void {

        // let displayMsg = this.id == 0 ? "Saved" : "Updated";

        // this.toastyService.success({
        //     title: displayMsg + ' Succesfully',
        //     // msg: 'Customer successfully ' + displayMsg,
        //     theme: 'bootstrap',
        //     showClose: true,
        //     timeout: 5000

        const displayMsg = this.id === 0 ? ' Saved' : ' Updated';
        this.msgs = [];
        this.msgs.push({
            severity: 'success',
            summary: 'Success Message',
            detail: 'Purchase Invoice Successfully' + displayMsg


        });
        // Reset the form to clear the flags
        this.purchaseForm.reset();
        this.router.navigate(['/authenticated/purchase']);
    }



     calculateNetAmount(): void {
        let itemTotal = 0;

        let totalInvoiceValue = 0;
        let totalTaxableValue = 0;
        let totalTaxAmount = 0;
        // let paidAmount = 0;
        let totalCessAmount = 0;
        let a = 0;
        let roundOf = this.purchaseForm.get('roundOf').value;


        for (var i = 0; i < this.purchaseInvoiceItems.length; i++) {
            var item = this.purchaseInvoiceItems.at(i);

            itemTotal = +item.get('total').value
            totalInvoiceValue = totalInvoiceValue + itemTotal;
            totalTaxableValue = totalTaxableValue + +item.get('taxableValue').value
            totalCessAmount = totalCessAmount + +item.get('cessAmount').value
            totalTaxAmount = totalTaxAmount + +item.get('cgstAmount').value + +item.get('sgstAmount').value +
                +item.get('igstAmount').value + +item.get('cessAmount').value
        }

        this.purchaseForm.patchValue({
            totalInvoiceValue: totalInvoiceValue.toFixed(0),
            totalTaxableValue: totalTaxableValue,
            totalTaxAmount: totalTaxAmount,
            totalCessAmount: totalCessAmount,
            // paidAmount: totalInvoiceValue.toFixed(2)

        });

    }



//      private recalculate () {

       

//          for (var i = 0; i < this.purchaseInvoiceItems.length; i++) {
//              var item = this.purchaseInvoiceItems.at(i);

//              let taxableValue = +item.get('taxableValue').value;
//              let taxRate = +item.get('taxRate').value;


                  
//                    if (!this.isIntraState){
//          item.patchValue({
//              igstAmount: (taxableValue * taxRate /100),
//              cgstAmount: 0,
//              sgstAmount: 0
            
              
//          });
// }
//          else{
//              item.patchValue({
//                  igstAmount:0,
//                  cgstAmount: (taxableValue * taxRate /100)/2,
//                  sgstAmount: (taxableValue * taxRate /100)/2
                
                  
//              });

//          }
//          }
//      }

    

     checkSupplyType(event) {

        let pos = this.purchaseForm.get('placeOfSupply').value;
        
        let ch = this.purchaseForm.get('supplierId').value;


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



    get purchaseInvoiceItems(): FormArray {
        return <FormArray>this.purchaseForm.get('purchaseInvoiceItems');
    }


    private getPurchasesInvoice(id: number): void {
        this.purchaseInvoiceService.getOne(id)
            .subscribe((purchasesInvoice: IPurchaseInvoice) => this.onPurchasesInvoiceRetrieved(purchasesInvoice)
            );
    }


   


    


    private onPurchasesInvoiceRetrieved(purchasesInvoice: IPurchaseInvoice): void {

        if (this.purchaseForm) {
            this.purchaseForm.reset();
        }

        this.purchasesInvoice = purchasesInvoice;

        if (this.purchasesInvoice.id == 0) {
            this.pageTitle = 'Purchase Invoice';
            
            this.userProfileService.getProfile().subscribe(up => {

                this.accountingUnitPlaceOfSupply = up.placeOfSupply;
                console.log(this.accountingUnitPlaceOfSupply);

            this.purchaseForm.patchValue({
                placeOfSupply: this.accountingUnitPlaceOfSupply
            });
             });


            // this.purchaseInvoiceService.getInvoiceNumber()
            //    .subscribe(inv => this.purchaseForm.patchValue({ invoiceNumber: inv }));
        }

        else {
            this.pageTitle = `Edit Purchase Invoice: ${this.purchasesInvoice.invoiceNumber}`;
            let invDate = new Date(this.purchasesInvoice.date);
            let invPostingDate = new Date(this.purchasesInvoice.postingDate);


            // Update the data on the form
            this.purchaseForm.patchValue({

                supplierId: this.purchasesInvoice.supplierId == null ? '' : this.purchasesInvoice.supplierId,
                invoiceNumber: this.purchasesInvoice.invoiceNumber,
                date: new Date(invDate.getTime() + Math.abs(invDate.getTimezoneOffset() * 60000)),
                postingDate: new Date(invPostingDate.getTime() + Math.abs(invPostingDate.getTimezoneOffset() * 60000)),
                placeOfSupply: this.purchasesInvoice.placeOfSupply,
                totalInvoiceValue: this.purchasesInvoice.totalInvoiceValue,
                shippingAddress: this.purchasesInvoice.shippingAddress,
                reference: this.purchasesInvoice.reference,
                totalTaxAmount: this.purchasesInvoice.totalTaxAmount,
                totalTaxableValue: this.purchasesInvoice.totalTaxableValue,
                totalCessAmount: this.purchasesInvoice.totalCessAmount,
                billedPassed : this.purchasesInvoice.billedPassed,
                // roundOf:this.purchasesInvoice.roundOf,
            });



            for (let i = 0; i < this.purchasesInvoice.purchaseInvoiceItems.length; i++) {
                this.purchaseInvoiceItems.push(this.buildProductLine(this.purchasesInvoice.purchaseInvoiceItems[i]));

            }

            this.userProfileService.getAccountingUnits().subscribe(up => {

                this.accountingUnitPlaceOfSupply = up.placeOfSupply;
                this.checkSupplyType(event);

            });

        }
    }

    disable(){
        this.busy = false;
    }

}




































