import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/components/common/menuitem';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  items: MenuItem[];

  constructor() { }

  ngOnInit() {
    this.items = [
      {
          label: 'Master',expanded : true,
          
          items: [
                  {label: 'Customer', routerLink:['/authenticated/customer']},
                  {label: 'Supplier', routerLink:['/authenticated/supplier']},
                  {label: 'Product', routerLink:['/authenticated/product']}
          ]
      },
     
      {
          label: 'Sales', expanded : true,
          
          items: [
                  {label: 'Sales', routerLink:['/authenticated/sales']}
                  // {label: 'Issue Note', routerLink:['/customer']},
                  // {label: 'Advanced Received'}
          ]
      },
      
      {
         label: 'Purchase',  expanded : true,
          
          items: [
                  {label: 'Purchase', routerLink:['/authenticated/purchase']},
                  // {label: 'Receipt Note'},
                  // {label: 'Advanced Paid'}
          ]
      },


      {
        label: 'TDS',  expanded : true,
         
         items: [
                 {label: 'TDS', routerLink:['/authenticated/tds']},
                 // {label: 'Receipt Note'},
                 // {label: 'Advanced Paid'}
         ]
     },

      
     
    ];
  
}
}