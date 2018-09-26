import { Component, OnInit } from '@angular/core';
import { SalesInvoiceService } from '../sales-invoice-service';
import { Router } from '@angular/router';
import { ISalesInvoice } from '../isalesinvoice';
import { Month } from '../../shared/month-list';
import { UserSetting } from '../../shared/Utilities/user-setting';
import { ConfirmationService } from 'primeng/api';
import {Message} from 'primeng/components/common/api';
import { UserProfileService } from '../../user-profile/userprofile.service';

@Component({
  selector: 'app-sales-list',
  templateUrl: './sales-list.component.html',
  styleUrls: ['./sales-list.component.css']
})
export class SalesListComponent implements OnInit {

  salesList : ISalesInvoice[];
  id : number = null;
  selectedSales : ISalesInvoice;
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
  search?: boolean;
  cols : any[];

  constructor(private salesListService : SalesInvoiceService,
              private router : Router,
              private confirmationService:ConfirmationService,
              private userProfileService : UserProfileService) { }

  ngOnInit() {
   
    this.yearDisplay = this.userProfileService.getYearDisplaySettings();
    console.log(this.yearDisplay);
    this.getSalesInvoicesForSelectedMonth(UserSetting.getSalesMonth());
    // this.cols = [
    //   { field : 'date', header : 'Date'},
    //   { field : 'invoiceNumber', header : 'Invoice No.'},
    //   { field : 'customer.name', header : 'Customer'},
    //   { field : 'totalTaxableValue', header : 'Total Taxable Value'},
    //   { field : 'totalTaxAmount', header : 'Total Tax Amount'},
    //   { field : 'totalInvoiceValue', header : 'Total Invoice Value'},
    // ]
  }

  getSalesInvoicesForSelectedMonth(monthId: number) {
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

    this.salesListService.getAllByMonth(this.selectedMonth, this.userProfileService.getYearSettings())
      .subscribe(sales => {
        this.salesList = sales;
        

        this.salesList.forEach(item => {
          this.totalTaxableValueSummary = this.totalTaxableValueSummary + item.totalTaxableValue;
          this.totalTaxAmountSummary = this.totalTaxAmountSummary + item.totalTaxAmount;
          this.totalCgstSummary = this.totalCgstSummary + item.totalCgst;
          this.totalSgstSummary = this.totalSgstSummary + item.totalSgst;
          this.totalIgstSummary = this.totalIgstSummary + item.totalIgst;
          this.totalCessSummary = this.totalCessSummary + item.totalCessAmount;

          
        });
      });
  }


  filterInvoicesRegisteredOrUnregistered(type?: boolean) {

    this.search = type;
  }


  

  showDialogToAdd(){
    this.id = 0;
    console.log(this.id);
    this.router.navigate(['/authenticated/sales',this.id]);
  }

  onRowSelect(event){
    this.id = event.data.id;
    console.log(this.id);
    this.router.navigate(['/authenticated/sales',this.id]);
  }

  showDialogToDelete(Rowdata){
    this.selectedSales = Rowdata;
    console.log(Rowdata);
    this.displayDialogDelete = true;
    
    this.confirmationService.confirm({
      message : 'Do you want to delete this record?',
      header: 'Delete Confirmation',
          icon: 'fa fa fa-fw fa-trash', 
          accept: () => {
            this.salesListService.delete(this.selectedSales.id).subscribe(() =>{
              this.salesListService.getAllByMonth(this.selectedMonth, this.userProfileService.getYearSettings())
              .subscribe(sales => {
                this.salesList = sales;
                this.totalTaxableValueSummary = this.totalTaxableValueSummary - this.selectedSales.totalTaxableValue;
                this.totalSgstSummary = this.totalSgstSummary - this.selectedSales.totalSgst;
                this.totalCessSummary= this.totalCessSummary - this.selectedSales.totalCessAmount;
                this.totalIgstSummary=this.totalIgstSummary - this.selectedSales.totalIgst;
                this.totalCgstSummary=this.totalCgstSummary - this.selectedSales.totalCgst;
                this.totalTaxAmountSummary=this.totalTaxAmountSummary - this.selectedSales.totalTaxAmount;
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
     return this.salesList.indexOf(this.selectedSales);    
  }

}
