import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { SalesListComponent } from './sales-list/sales-list.component';
import { SalesInvoiceFormComponent } from './sales-invoice-form/sales-invoice-form.component';
import { SharedModule } from '../shared/shared.module';
import { SalesInvoiceService } from './sales-invoice-service';
import { InvoiceItemsComponent } from './invoice-items/invoice-items.component';
import { SalesDetailComponent } from './sales-detail/sales-detail.component';
import { AuthenticatedUserComponent } from '../authenticated-user/authenticated-user.component';
import { AuthGuardService } from '../shared/guards/auth-guard.service';


const routes: Routes = [{
  path : 'authenticated',
  component : AuthenticatedUserComponent,
  canActivate: [AuthGuardService],
  children : [
    { path: 'sales', component: SalesListComponent },
    { path: 'sales/:id', component: SalesInvoiceFormComponent },
    { path: 'sales/:id/print', component: SalesDetailComponent },  
  ]
}]
  

  // { path: 'purchase', component: PurchaseListComponent },
  // { path: 'purchases/:id', component: PurchaseFormComponent }


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],

  declarations: [SalesListComponent, SalesInvoiceFormComponent,InvoiceItemsComponent, SalesDetailComponent],
  providers:[SalesInvoiceService]
})


export class SaleModule { }
