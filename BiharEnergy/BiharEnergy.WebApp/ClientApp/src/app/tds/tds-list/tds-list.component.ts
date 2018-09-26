import { Component, OnInit } from '@angular/core';
import { Itds } from '../Itds';
import { Message, ConfirmationService, LazyLoadEvent } from 'primeng/components/common/api';
import { Router } from '@angular/router';
import { TdsService } from '../tds.service';

@Component({
  selector: 'app-tds-list',
  templateUrl: './tds-list.component.html',
  styleUrls: ['./tds-list.component.css']
})
export class TdsListComponent implements OnInit {

  tdsList: Itds[];
  id:number=null;
  selectedTds: Itds;
  displayDialogDelete : boolean;
  cols : any[];
  msgs: Message[] = [];
  loading : boolean;
  datasource : Itds[];
  totalRecords : number;
  
   constructor(private tdsService: TdsService, 
               private router: Router,
               private confirmationService:ConfirmationService) { }
 
   ngOnInit() {
     this.getTdss();
     
     this.cols = [
      { field: 'id', header: 'Sn.', width: '5%' },
      { field: 'name', header: 'Name', width: '15%' },
      { field: 'placeOfSupply', header: 'Place Of Supply', width: '15%'},
      { field: 'ContactNumber', header: 'Contact Number', width: '15%'}
      ];
     
      this.loading = true;
   }
   getTdss(){
     this.tdsService.getAll().subscribe(tds=>{
       this.tdsList = tds;
       this.datasource = this.tdsList;
       this.totalRecords = this.datasource.length;
     });
   }
    
    tdsToCreate(){
      this.id=0;
      console.log(this.id);
      
    }
 
    tdsToEdit(event){
      this.id = event.data.id;
      console.log(this.id);
      
 
    }
 
    showDialogToDelete(Rowdata){
     this.selectedTds = Rowdata;
     console.log(Rowdata);
     this.displayDialogDelete = true;
     
     this.confirmationService.confirm({
       message : 'Do you want to delete this record?',
       header: 'Delete Confirmation',
           icon: 'fa fa fa-fw fa-trash', 
           accept: () => {
             this.tdsService.delete(this.selectedTds.id).subscribe(() =>{
               this.getTdss();
             this.msgs = [{severity:'info', summary:'Confirmed', detail:'Record deleted'}];
           });         
           },
         reject: () => {
             // this.msgs = [{severity:'info', summary:'Rejected', detail:'You have rejected'}];
         }
      
     });
   } 
 
    findSelectedTdsIndex():number{
      return this.tdsList.indexOf(this.selectedTds)
    }
 
    loadTdssLazy(event: LazyLoadEvent) {
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
             this.tdsList = this.datasource.slice(event.first, (event.first + event.rows));
             this.loading = false;
         }
     }, 1000);
 }
 }
 