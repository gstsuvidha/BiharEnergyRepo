import { ProductListComponent } from './products/product-list/product-list.component';


import { SharedModule } from '../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



import { CustomerListComponent } from './customers/customer-list/customer-list.component';
import { Routes, RouterModule } from '@angular/router';

import { SupplierListComponent } from './suppliers/supplier-list/supplier-list.component';
import { SupplierFormComponent } from './suppliers/supplier-form/supplier-form.component';

import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductFormComponent } from './products/product-form/product-form.component';
import { CustomerFormComponent } from './customers/customer-form/customer-form.component';
import { AuthenticatedUserComponent } from '../authenticated-user/authenticated-user.component';

const routes: Routes = [
  { path: 'authenticated',
    component : AuthenticatedUserComponent,
    children : [
      {path: 'customer', component: CustomerListComponent},
  {path:'customers/:id',component:CustomerFormComponent},
  {path: 'supplier/:id', component: SupplierFormComponent},
  {path: 'supplier', component: SupplierListComponent},
  {path: 'products/:id',component:ProductFormComponent},
  {path: 'product', component: ProductListComponent}   
    ]}
  
  
]

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    
    RouterModule.forChild(routes),
  ],
  
  declarations:  [CustomerListComponent, 
                  CustomerFormComponent,
                  SupplierListComponent,
                  SupplierFormComponent,
                  ProductFormComponent,
                  ProductListComponent
                  ]
})
export class MasterModule { }
