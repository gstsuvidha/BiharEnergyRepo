
import { ICustomer } from '../icustomer';
import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../customer.service';
import { Router } from '@angular/router';
import {ConfirmationService} from 'primeng/api';
import {Message, LazyLoadEvent} from 'primeng/components/common/api';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
 customerList: ICustomer[];
 id:number=null;
 selectedCustomer: ICustomer;
 displayDialogDelete : boolean;
 cols : any[];
 msgs: Message[] = [];
 loading : boolean;
 datasource : ICustomer[];
 totalRecords : number;
 
  constructor(private customerService: CustomerService, 
              private router: Router,
              private confirmationService:ConfirmationService) { }

  ngOnInit() {
    this.getCustomers();
    
    this.cols = [
     { field: 'id', header: 'Sn.', width: '5%' },
     { field: 'name', header: 'Name', width: '15%' },
     { field: 'gstin', header: 'GSTIN', width: '15%' },
     { field: 'registrationType', header: 'Registration Type', width: '15%' },
     { field: 'address', header: 'Address', width: '15%'},
     { field: 'state', header: 'State', width: '15%'},
     { field: 'ContactNumber', header: 'Contact Number', width: '15%'}
     ];
    
     this.loading = true;
  }
  getCustomers(){
    this.customerService.getAll().subscribe(customer=>{
      this.customerList = customer;
      this.datasource = this.customerList;
      this.totalRecords = this.datasource.length;
    });
  }
   
   customerToCreate(){
     this.id=0;
     console.log(this.id);
     
   }

   customerToEdit(event){
     this.id = event.data.id;
     console.log(this.id);
     

   }

   showDialogToDelete(Rowdata){
    this.selectedCustomer = Rowdata;
    console.log(Rowdata);
    this.displayDialogDelete = true;
    
    this.confirmationService.confirm({
      message : 'Do you want to delete this record?',
      header: 'Delete Confirmation',
          icon: 'fa fa fa-fw fa-trash', 
          accept: () => {
            this.customerService.delete(this.selectedCustomer.id).subscribe(() =>{
              this.getCustomers();
            this.msgs = [{severity:'info', summary:'Confirmed', detail:'Record deleted'}];
          });         
          },
        reject: () => {
            // this.msgs = [{severity:'info', summary:'Rejected', detail:'You have rejected'}];
        }
     
    });
  } 

   findSelectedCustomerIndex():number{
     return this.customerList.indexOf(this.selectedCustomer)
   }

   loadCustomersLazy(event: LazyLoadEvent) {
    this.loading = true;

    //in a real application, make a remote request to load data using state metadata from event
    //event.first = First row offset
    //event.rows = Number of rows per page
    //event.sortField = Field name to sort with
    //event.sortOrder = Sort order as number, 1 for asc and -1 for dec
    //filters: FilterMetadata object having field as key and filter value, filter matchMode as value

    //imitate db connection over a network
    setTimeout(() => {
        if (this.datasource) {
            this.customerList = this.datasource.slice(event.first, (event.first + event.rows));
            this.loading = false;
        }
    }, 1000);
}
}
