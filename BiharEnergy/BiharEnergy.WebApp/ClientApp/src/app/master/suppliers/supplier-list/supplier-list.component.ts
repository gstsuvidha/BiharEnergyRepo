import { SupplierService } from '../supplier.service';
import { ISupplier } from '../isupplier';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {ConfirmationService} from 'primeng/api';
import {Message, LazyLoadEvent} from 'primeng/components/common/api';


@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

  supplierList: ISupplier[];
  id: number = null;
  selectedSupplier: ISupplier;
  cols: any[];
  displayDialogDelete : boolean;
  msgs: Message[] = [];
  loading : boolean;
  datasource : ISupplier[];
  totalRecords : number;
 

  constructor(private supplierService: SupplierService,
              private router: Router,
              private confirmationService:ConfirmationService) { }

  ngOnInit() {
    this.getSuppliers();
    
    this.cols = [
      { field: 'id', header: 'Sn.', width: '5%' },
      { field: 'name', header: 'Name', width: '15%' },
      { field: 'gstin', header: 'GSTIN', width: '15%' },
      { field: 'registrationType', header: 'Registration Type', width: '15%' },
      { field: 'address', header: 'Address', width: '15%' },
      { field: 'state', header: 'State', width: '15%' },
      { field: 'ContactNumber', header: 'Contact Number', width: '15%' }
    ];
    this.loading = true;
  }

  getSuppliers(){
    this.supplierService.getAll().subscribe(supplier => {
     this.supplierList = supplier;
     this.datasource = this.supplierList;
     this.totalRecords = this.datasource.length;
    });
  }
  supplierToCreate() {
    this.id = 0;
    console.log(this.id);
    
  }

  supplierToEdit(event) {
    this.id = event.data.id;
    console.log(this.id);
    

  }

  showDialogToDelete(Rowdata){
    this.selectedSupplier = Rowdata;
    console.log(Rowdata);
    this.displayDialogDelete = true;
    
    this.confirmationService.confirm({
      message : 'Do you want to delete this record?',
      header: 'Delete Confirmation',
          icon: 'fa fa fa-fw fa-trash', 
          accept: () => {
            this.supplierService.delete(this.selectedSupplier.id).subscribe(() =>{
              this.getSuppliers();
            this.msgs = [{severity:'info', summary:'Confirmed', detail:'Record deleted'}];
          });         
          },
        reject: () => {
            // this.msgs = [{severity:'info', summary:'Rejected', detail:'You have rejected'}];
        }
     
    });
  } 

  findSelectedSupplierIndex(): number {
    return this.supplierList.indexOf(this.selectedSupplier)
  }
  loadSuppliersLazy(event: LazyLoadEvent) {
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
            this.supplierList = this.datasource.slice(event.first, (event.first + event.rows));
            this.loading = false;
        }
    }, 1000);
}
}


