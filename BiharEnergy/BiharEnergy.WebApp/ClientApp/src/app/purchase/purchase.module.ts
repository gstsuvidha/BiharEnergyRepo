import { SharedModule } from '../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PurchaseListComponent } from './purchase-list/purchase-list.component';
import { PurchaseFormComponent } from './purchase-form/purchase-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { PurchaseInvoiceItemsComponent } from './purchase-invoice-items/purchase-invoice-items.component';
import { AuthenticatedUserComponent } from '../authenticated-user/authenticated-user.component';
import { AuthGuardService } from '../shared/guards/auth-guard.service';




const routes: Routes = [{
  path : 'authenticated',
  component : AuthenticatedUserComponent,
  canActivate: [AuthGuardService],
  children : [
    { path: 'purchase', component: PurchaseListComponent },
    { path: 'purchases/:id', component: PurchaseFormComponent }
  ]
}

]

@NgModule({
  imports: [SharedModule,
            CommonModule,
            RouterModule.forChild(routes),
            SharedModule,
            FormsModule,
            ReactiveFormsModule
        
  ],
  declarations: [PurchaseListComponent,
                  PurchaseFormComponent,
                PurchaseInvoiceItemsComponent]
})
export class PurchaseModule { }
