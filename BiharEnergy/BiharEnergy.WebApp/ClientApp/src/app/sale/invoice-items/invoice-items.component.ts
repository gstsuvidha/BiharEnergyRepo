
import { FormArray } from '@angular/forms';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../master/products/product.service';
import { ISalesInvoiceItem } from '../isalesinvoice';
import { IProduct } from '../../master/products/iproduct';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-invoice-items',
  templateUrl: './invoice-items.component.html',
  styleUrls: ['./invoice-items.component.css']
})
export class InvoiceItemsComponent implements OnInit {

    constructor(private productService: ProductService) { }
    
    products:IProduct[];
    quantity;
    productSelectList : SelectItem[];
    @Input() invoiceItems:FormArray;

    @Output() addItem: EventEmitter<ISalesInvoiceItem> = new EventEmitter<ISalesInvoiceItem>();  
    @Output() removeItemIndex: EventEmitter<number> = new EventEmitter<number>(); 

  ngOnInit() {
      
    this.productService.getAll().subscribe(products => {
        this.products = products.filter(pro=>pro.isSaleable);
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

  getUnitPrice(pId: HTMLInputElement, unitPrice: HTMLInputElement): void {
    var prod = this.products.find(p => p.id == Number(pId.value));
    var product = this.products.find(p => p.id == Number(pId.value)).id;
    
    if (prod != null) {
        unitPrice.value = prod.rate.toString();      
        }
    }
    
  
  checkQuantity(pId: HTMLInputElement,qty: HTMLInputElement){
    this.quantity = qty.value;
    console.log(this.quantity);
    
  }

addProductLine(pId: HTMLInputElement, qty: HTMLInputElement, unitPrice: HTMLInputElement, dis: HTMLInputElement, changeInDiscountCalcu: HTMLInputElement): void {

    
          let product = this.products.find(p => p.id == Number(pId.value));
          if (product==null) {
              alert("Please Select a Product");
              return;
          }
        
         
          let taxableValue:number;
          let taxAmount:number;
          let totalWithoutCess:number;

        if (product.isTaxIncluded){
            totalWithoutCess = (+qty.value * +unitPrice.value) * (1 - (+dis.value/100));
            taxableValue = totalWithoutCess /(1 + (+product.igst/100) );           
            taxAmount = totalWithoutCess - taxableValue;
              

                                     
        }
        else{
           taxableValue = Number(qty.value) * Number(unitPrice.value) * (1 - Number(dis.value) / 100);

           taxableValue = (+qty.value * +unitPrice.value) * (1 - (+dis.value/100));
           taxAmount = taxableValue * product.igst/100;          
           totalWithoutCess = taxableValue + taxAmount;           


        }
            var salesInvoiceItem: ISalesInvoiceItem = {
                              
                              productId: Number(pId.value),
                              quantity: Number(qty.value),
                              unitPrice: Number(unitPrice.value),
                              discount: Number(dis.value),                 
                              taxRate: product.igst,
                              taxableValue: taxableValue,                
                              taxAmount: taxAmount,
                              cess: (product.rate) * (product.cess/100),                              
                              total: totalWithoutCess + (product.rate) * (product.cess/100)
                          }
  
                          var salesInvoiceItem: ISalesInvoiceItem = {
                            
                            
                                          productId: Number(pId.value),
                                          quantity: Number(qty.value),
                                          unitPrice: Number(unitPrice.value),
                                          discount: Number(dis.value),
                                          taxRate: product.igst,
                                          taxableValue: taxableValue,
                                          taxAmount: taxableValue * product.igst / 100,
                                          cess: (product.rate) * (product.cess/100),
                                          total: (taxableValue) + (taxableValue * product.igst / 100) + (product.rate) * (product.cess/100),

                                        }                                      
         
         
         

  
          
          if (this.isValidSalesInvoiceItem(salesInvoiceItem)) {
  
              this.addItem.emit(salesInvoiceItem)
              qty.value = null;
  
              dis.value = null;
              
              unitPrice.value = null;
              pId.value=null;


              
           
          }
          else
  
              alert("You cannot leave any of the mandatory field empty or null");
  
  
          pId.focus();
  
  
      }
  
      isValidSalesInvoiceItem(salesInvoiceItem: ISalesInvoiceItem): boolean {
  
          let result: boolean = true;
  
          if (isNaN(Number(salesInvoiceItem.quantity)) || isNaN(Number(salesInvoiceItem.unitPrice)) || isNaN(Number(salesInvoiceItem.discount))) {
              result = false;
          }
          return result;
  
      }

    
removeLine(i:number):void{
  this.removeItemIndex.emit(i);
      }


//checkStockOfProduct(pId: number): number {
    
//}






}



