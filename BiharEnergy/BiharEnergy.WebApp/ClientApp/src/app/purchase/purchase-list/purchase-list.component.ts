import { Component, OnInit } from '@angular/core';
import { Month } from '../../shared/month-list';
import { UserSetting } from '../../shared/Utilities/user-setting';
import { PurchaseService } from '../purchase.service';
import { IPurchaseInvoice } from '../ipurchase';
import { Router } from '@angular/router';
import { ConfirmationService, Message } from 'primeng/api';
import { UserProfileService } from '../../user-profile/userprofile.service';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.css']
})
export class PurchaseListComponent implements OnInit {

  purchaseList: IPurchaseInvoice[];
  id : number = null;
  selectedPurchase : IPurchaseInvoice;
  month = Month;
  selectedMonth: number;
  totalTaxableValueSummary: number = 0;
  totalTaxAmountSummary: number = 0;
  totalIgstSummary: number = 0;
  totalCgstSummary: number = 0;
  totalSgstSummary: number = 0;
  totalCessSummary: number = 0;
  pager: any = [];
  pagedItems: any[];
  yearDisplay: string;
  displayDialogDelete : boolean;
  msgs: Message[] = [];
  
  constructor(private purchaseListService : PurchaseService,
              private router : Router,
              private confirmationService:ConfirmationService,
              private userProfileService : UserProfileService) { }

  ngOnInit() {
    
    this.yearDisplay = this.userProfileService.getYearDisplaySettings();
    this.getPurchasesInvoicesForSelectedMonth(UserSetting.getSalesMonth());
  }

  getPurchasesInvoicesForSelectedMonth(monthId: number) {
    UserSetting.setSalesMonth(monthId);
    
    this.selectedMonth = monthId;
    this.totalTaxableValueSummary = 0;
    this.totalTaxAmountSummary = 0;
    this.totalCgstSummary = 0;
    this.totalSgstSummary = 0;
    this.totalIgstSummary = 0;
    this.totalCessSummary = 0;
    this.pagedItems = [];
    this.pager = {};

    this.purchaseListService.getAllByMonth(this.selectedMonth, this.userProfileService.getYearSettings())
      .subscribe(purchase => {
        this.purchaseList = purchase;
        

        this.purchaseList.forEach(item => {
          this.totalTaxableValueSummary = this.totalTaxableValueSummary + item.totalTaxableValue;
          this.totalTaxAmountSummary = this.totalTaxAmountSummary + item.totalTaxAmount;
          this.totalCgstSummary = this.totalCgstSummary +item.totalCgst;
          this.totalSgstSummary = this.totalSgstSummary +item.totalSgst;
          this.totalIgstSummary = this.totalIgstSummary +item.totalIgst;
          this.totalCessSummary = this.totalCessSummary +item.totalCessAmount;

          
        });
      });
  }

  

  showDialogToAdd(){
    this.id = 0;
    console.log(this.id);
    this.router.navigate(['/authenticated/purchases',this.id]);
  }

  onRowSelect(event){
    this.id = event.data.id;
    console.log(this.id);
    this.router.navigate(['/authenticated/purchases',this.id]);
  }

  showDialogToDelete(Rowdata){
    this.selectedPurchase = Rowdata;
    console.log(Rowdata);
    this.displayDialogDelete = true;
    
    this.confirmationService.confirm({
      message : 'Do you want to delete this record?',
      header: 'Delete Confirmation',
          icon: 'fa fa fa-fw fa-trash', 
          accept: () => {
            this.purchaseListService.delete(this.selectedPurchase.id).subscribe(() =>{
              this.purchaseListService.getAllByMonth(this.selectedMonth, UserSetting.getYearSettings())
              .subscribe(purchase => {
              this.purchaseList = purchase;
              this.totalTaxableValueSummary = this.totalTaxableValueSummary - this.selectedPurchase.totalTaxableValue;
                this.totalSgstSummary = this.totalSgstSummary - this.selectedPurchase.totalSgst;
                this.totalCessSummary= this.totalCessSummary - this.selectedPurchase.totalCessAmount;
                this.totalIgstSummary=this.totalIgstSummary - this.selectedPurchase.totalIgst;
                this.totalCgstSummary=this.totalCgstSummary - this.selectedPurchase.totalCgst;
                this.totalTaxAmountSummary=this.totalTaxAmountSummary - this.selectedPurchase.totalTaxAmount;
              });
            this.msgs = [{severity:'info', summary:'Confirmed', detail:'Record deleted'}];
          });         
          },
        reject: () => {
            // this.msgs = [{severity:'info', summary:'Rejected', detail:'You have rejected'}];
        }
     
    });
  } 

  findSelectedRecurringIndex() : number{
     return this.purchaseList.indexOf(this.selectedPurchase);    
  }
}
