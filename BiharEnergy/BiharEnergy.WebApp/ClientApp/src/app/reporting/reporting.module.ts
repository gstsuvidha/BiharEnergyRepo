import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

import { AccountingUnitDashboardComponent } from './accounting-unit-dashboard/accounting-unit-dashboard.component';
import { B2bComponent } from './b2b/b2b.component';
import { B2cComponent } from './b2c/b2c.component';
import { AdvanceReceiveComponent } from './advance-receive/advance-receive.component';
import { B2clComponent } from './b2cl/b2cl.component';
import { CndrComponent } from './cndr/cndr.component';
import { CndurComponent } from './cndur/cndur.component';
import { ConsolidatedComponent } from './consolidated/consolidated.component';
import { ConsolidatedSidebarComponent } from './consolidated-sidebar/consolidated-sidebar.component';
import { DocumentDetailComponent } from './document-detail/document-detail.component';
import { ExportComponent } from './export/export.component';
import { Gstr3bComponent } from './gstr3b/gstr3b.component';
import { HomeComponent } from './home/home.component';
import { HsnComponent } from './hsn/hsn.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FinalReportComponent } from './final-report/final-report.component';
import { Gstr1FinalReportComponent } from './reportingForAccountingUnit/gstr1-final-report/gstr1-final-report.component';
import { SidebarForAccountingUnitComponent } from './reportingForAccountingUnit/sidebar-for-accounting-unit/sidebar-for-accounting-unit.component';
import { AccountingUnitComponent } from './reportingForAccountingUnit/accounting-unit/accounting-unit.component';
import { Gstr3bForAccountingComponent } from './reportingForAccountingUnit/gstr3b-for-accounting/gstr3b-for-accounting.component';
import { B2bForAccountingUnitComponent } from './reportingForAccountingUnit/b2b-for-accounting-unit/b2b-for-accounting-unit.component';
import { B2cForAccountingUnitComponent } from './reportingForAccountingUnit/b2c-for-accounting-unit/b2c-for-accounting-unit.component';
import { B2clForAccountingUnitComponent } from './reportingForAccountingUnit/b2cl-for-accounting-unit/b2cl-for-accounting-unit.component';
import { CdnrForAccountingUnitComponent } from './reportingForAccountingUnit/cdnr-for-accounting-unit/cdnr-for-accounting-unit.component';
import { CdnurForAccountingUnitComponent } from './reportingForAccountingUnit/cdnur-for-accounting-unit/cdnur-for-accounting-unit.component';
import { AdvanceReceiveForAccountingUnitComponent } from './reportingForAccountingUnit/advance-receive-for-accounting-unit/advance-receive-for-accounting-unit.component';
import { ExportForAccountingUnitComponent } from './reportingForAccountingUnit/export-for-accounting-unit/export-for-accounting-unit.component';
import { HsnForAccountingUnitComponent } from './reportingForAccountingUnit/hsn-for-accounting-unit/hsn-for-accounting-unit.component';
import { DocumentDetailForAccountingUnitComponent } from './reportingForAccountingUnit/document-detail-for-accounting-unit/document-detail-for-accounting-unit.component';
import { Gstr7Component } from './reportingForAccountingUnit/gstr7/gstr7.component';






@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        SharedModule,
        RouterModule.forRoot([
      
            {path :'reporting',component:HomeComponent},
            {path :'reporting/:monthId/:yearId/:companyId/consolidated',component:ConsolidatedComponent},
            {path :'reporting/:monthId/:yearId/:companyId/consolidated/accounting',component:AccountingUnitDashboardComponent},
            {path :'reporting/:monthId/:yearId/:companyId/consolidated/gstr-3b-report',component:Gstr3bComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-b2b-report', component:B2bComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-b2c-report', component:B2cComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-b2cl-report', component:B2clComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-document-detail-report', component:DocumentDetailComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-cndr-report', component:CndrComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-cndur-report', component:CndurComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-advance-receive-report', component:AdvanceReceiveComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-export-report', component:ExportComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-hsn-report', component:HsnComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/consolidated/gstr1-final-report', component:FinalReportComponent},
            
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit',component : AccountingUnitComponent},
            { path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-final-report-for-accountingunit', component : Gstr1FinalReportComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr-3b-report-for-accountingunit',component:Gstr3bForAccountingComponent },
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-b2b-report-for-accountingunit',component:B2bForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-b2c-report-for-accountingunit',component : B2cForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-b2cl-report-for-accountingunit',component : B2clForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-cdnr-report-for-accountingunit',component : CdnrForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-cdnur-report-for-accountingunit',component : CdnurForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-advance-receive-report-for-accountingunit',component : AdvanceReceiveForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-export-report-for-accountingunit',component : ExportForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-hsn-report-for-accountingunit',component : HsnForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr1-document-detail-report-for-accountingunit',component : DocumentDetailForAccountingUnitComponent},
            {path : 'reporting/:monthId/:yearId/:companyId/:accountingUnitId/accounting-unit/gstr7',component : Gstr7Component}

            
          ])
    ],
    exports: [],
    declarations: [
        AccountingUnitDashboardComponent,
        AdvanceReceiveComponent,
        B2bComponent,
        B2cComponent,
        B2clComponent,
        CndrComponent,
        CndurComponent,
        ConsolidatedComponent,
        ConsolidatedSidebarComponent,
        DocumentDetailComponent,
        ExportComponent,
        Gstr3bComponent,
        HomeComponent,
        HsnComponent,
        NavbarComponent,
        FinalReportComponent,
        Gstr1FinalReportComponent,
        SidebarForAccountingUnitComponent,
        AccountingUnitComponent,
        Gstr3bForAccountingComponent,
        B2bForAccountingUnitComponent,
        B2cForAccountingUnitComponent,
        B2clForAccountingUnitComponent,
        CdnrForAccountingUnitComponent,
        CdnurForAccountingUnitComponent,
        AdvanceReceiveForAccountingUnitComponent,
        ExportForAccountingUnitComponent,
        HsnForAccountingUnitComponent,
        DocumentDetailForAccountingUnitComponent,
        Gstr7Component
    ],
    providers: [],
})
export class ReportingModule { }
