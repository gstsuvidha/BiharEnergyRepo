
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';





import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { SaleModule } from './sale/sale.module'
import { MasterModule } from './master/master.module';
import { PurchaseModule } from './purchase/purchase.module';
import { CustomerService } from './master/customers/customer.service';
import { SupplierService } from './master/suppliers/supplier.service';
import { ProductService } from './master/products/product.service';
import { SharedModule } from './shared/shared.module';
import { PurchaseService } from './purchase/purchase.service';

import { UserProfileComponent } from './user-profile/user-profile/user-profile.component';
import { Gstr1Service } from './reporting/gstr1.service';
import { UserProfileService } from './user-profile/userprofile.service';
import { ReportingModule } from './reporting/reporting.module';
import { Gstr3bService } from './reporting/gstr3b.service';
import { LoginService } from './login/login.service';
import { LoginFormComponent } from './login/loginform/loginform.component';
import { HttpModule } from '@angular/http';
import { AuthenticatedUserComponent } from './authenticated-user/authenticated-user.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { AuthGuardService } from './shared/guards/auth-guard.service';
import { TokenInterceptor } from './login/token.interceptor';
import { SlimLoadingBarModule } from 'ng2-slim-loading-bar';
import { ErrorsHandler } from './shared/errors-handler';
import { HomeComponent } from './home/home.component';
import { Gstr1ForAccountingUnitService } from './reporting/gstr1-for-accounting-unit.service';
import { Gstr3bForAccountingService } from './reporting/gstr3bForAccounting.service';
import { TdsModule } from './tds/tds.module';
import { TdsService } from './tds/tds.service';
import { Gstr7Service } from './reporting/gstr7.service';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    
    
    CounterComponent,
    FetchDataComponent,
    
    
    CounterComponent,
    FetchDataComponent,
    UserProfileComponent,
    LoginFormComponent,
    AuthenticatedUserComponent,
    DashboardComponent,
    TopMenuComponent
    

    
  ],
  imports: [
    // BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    ReportingModule,
    SaleModule,
    SharedModule,
    MasterModule,
    TdsModule,
    ReactiveFormsModule,
    PurchaseModule,
     SlimLoadingBarModule.forRoot(),
    
    RouterModule.forRoot([
      {path: 'authenticated',
      component: AuthenticatedUserComponent,
      canActivate: [AuthGuardService],
      children: [
        {path : 'dash-board', component : DashboardComponent},
        {path : 'user-profile/:action', component : UserProfileComponent}
      
    ]
    },
      {path : 'login', component : LoginFormComponent},
      { path: '', redirectTo: 'authenticated/dash-board', pathMatch: 'full' },
      { path: '**', redirectTo: 'authenticated/dash-board' }

    ])
  ],
  providers: [CustomerService,
    SupplierService,
  ProductService,
    PurchaseService,
    TdsService,
    
    Gstr1Service,
    Gstr1ForAccountingUnitService,
    UserProfileService,
    Gstr3bService,
    LoginService,
    Gstr7Service,
    Gstr3bForAccountingService,
    { provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    // {
    //   provide : ErrorHandler,
    //   useClass : ErrorsHandler
    // }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
