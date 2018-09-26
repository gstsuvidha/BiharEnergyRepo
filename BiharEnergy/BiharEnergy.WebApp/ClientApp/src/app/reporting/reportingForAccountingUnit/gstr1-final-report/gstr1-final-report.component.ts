import { Component, OnInit } from '@angular/core';
import { MonthList } from '../../../shared/month-list';
import { StateList } from '../../../shared/state-list';

import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { IGstr1 } from '../../igstr1';

@Component({
  selector: 'app-gstr1-final-report',
  templateUrl: './gstr1-final-report.component.html',
  styleUrls: ['./gstr1-final-report.component.css']
})
export class Gstr1FinalReportComponent implements OnInit {

  currentYear = new Date();
  year = this.currentYear.getFullYear();
  nextYear=this.year+1;
  public gstr1DocDetail;
  monthList = MonthList;
  monthId: number;
  public gstr1:IGstr1;
  stateList = StateList;
  gstr1HsnList: string;
 gstr1AccountingUnitExtraInfoList:string;

  totalB2bInvoicesNo: number =0;
  totalB2bInvoiceValue: number =0;
  totalB2bTaxableValue: number = 0;
  totalB2bIgst: number =0;
  totalB2bSgst: number=0;
  totalB2bCgst: number=0;
  totalB2bCess: number = 0;

  totalB2clInvoicesNo: number = 0;
  totalB2clInvoiceValue: number = 0;
  totalB2clTaxableValue: number = 0;
  totalB2clIgst: number = 0;
  totalB2clCess: number = 0;

  totalB2cInvoicesNo: number = 0;
  totalB2cInvoiceValue: number = 0;
  totalB2cTaxableValue: number = 0;
  totalB2cIgst: number = 0;
  totalB2cSgst: number = 0;
  totalB2cCgst: number = 0;
  totalB2cCess: number = 0;

  totalCdnrInvoicesNo: number = 0;
  totalCdnrInvoiceValue: number = 0;
  totalCdnrTaxableValue: number = 0;
  totalCdnrIgst: number = 0;
  totalCdnrSgst: number = 0;
  totalCdnrCgst: number = 0;
  totalCdnrCess: number = 0;

  totalCdnurInvoicesNo: number = 0;
  totalCdnurInvoiceValue: number = 0;
  totalCdnurTaxableValue: number = 0;
  totalCdnurIgst: number = 0;
  totalCdnurCess: number = 0;

  totalAdvRevInvoicesNo: number = 0;
  totalAdvRevInvoiceValue: number = 0;
  totalAdvRevTaxableValue: number = 0;
  totalAdvRevIgst: number = 0;
  totalAdvRevSgst: number = 0;
  totalAdvRevCgst: number = 0;
  totalAdvRevCess: number = 0;

  totalHsnInvoicesNo: number = 0;
  totalHsnInvoiceValue: number = 0;
  totalHsnTaxableValue: number = 0;
  totalHsnIgst: number = 0;
  totalHsnSgst: number = 0;
  totalHsnCgst: number = 0;
  totalHsnCess: number = 0;

  totalExportInvoicesNo: number = 0;
  totalExportInvoiceValue: number = 0;
  totalExportTaxableValue: number = 0;
  totalExportIgst: number = 0;

  totalDocdetInvoicesNo: number = 0;
  totalDocdetIssuedDocument: number = 0;
  totalDocdetCancelled: number = 0;
  totalDocdetNetIssuedDoc: number = 0;
  yearId : number;
  company : number;
  accountingUnitId : number;


  constructor(private gstr1ReportingForAccountingUnitService : Gstr1ForAccountingUnitService,
              private route : ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.company = params['companyId'];
      this.accountingUnitId = params['accountingUnitId'];
      this.gstr1ReportingForAccountingUnitService.getGstr1FinalReportForAccountingUnit(this.accountingUnitId,this.monthId,this.yearId)
          .subscribe(gstr1FinalReport => {
              this.gstr1 = gstr1FinalReport;
              console.log(gstr1FinalReport);
              this.calculateB2bReport();
              this.calculateB2clReport();
              this.calculateB2cReport();
              this.calculateCdnrReport();
              this.calculateCdnurReport();
              this.calculateAdvRevReport();
              this.calculateHsnReport();
              this.calculateExportReport();
        });
  });
  this.gstr1ReportingForAccountingUnitService.getGstr1DocumentDetailForAccountingUnit(this.accountingUnitId,this.monthId,this.yearId)
      .subscribe(gstr1DocumentDetail => {
          this.gstr1DocDetail = gstr1DocumentDetail;
          
          
      });
  }

  calculateB2bReport(): void {

    this.gstr1.b2b.forEach(b2bPerCtin => {

        b2bPerCtin.inv.forEach(inv => {
            this.totalB2bInvoicesNo++;
            this.totalB2bInvoiceValue = this.totalB2bInvoiceValue + inv.val;

            inv.itms.forEach(itm => {

                this.totalB2bTaxableValue = this.totalB2bTaxableValue + itm.itmDet.txval;
                this.totalB2bIgst = itm.itmDet.iamt ? this.totalB2bIgst + itm.itmDet.iamt : this.totalB2bIgst;
                this.totalB2bCgst = itm.itmDet.camt ? this.totalB2bCgst + itm.itmDet.camt : this.totalB2bCgst;
                this.totalB2bSgst = itm.itmDet.samt ? this.totalB2bSgst + itm.itmDet.samt : this.totalB2bSgst;
                this.totalB2bCess = itm.itmDet.csamt ? this.totalB2bCess + itm.itmDet.csamt : this.totalB2bCess;
            });
        });
    });

    

}

calculateB2clReport(): void {

    this.gstr1.b2cl.forEach(b2clPerPos => {

        b2clPerPos.inv.forEach(inv => {
            this.totalB2clInvoicesNo++;
            this.totalB2clInvoiceValue = this.totalB2clInvoiceValue + inv.val;   //error in code

            inv.itms.forEach(itm => {

                this.totalB2clTaxableValue = this.totalB2clTaxableValue + itm.itmDet.txval;
                this.totalB2clIgst = itm.itmDet.iamt ? this.totalB2clIgst + itm.itmDet.iamt : this.totalB2clIgst;
                this.totalB2clCess = itm.itmDet.csamt ? this.totalB2clCess + itm.itmDet.csamt : this.totalB2clCess;
            });
        });
    });



}

calculateCdnrReport(): void {

    this.gstr1.cdnr.forEach(cdnrPerCtin => {

        cdnrPerCtin.nt.forEach(inv => {
            this.totalCdnrInvoicesNo++;
            this.totalCdnrInvoiceValue = this.totalCdnrInvoiceValue + inv.val;

            inv.itms.forEach(itm => {

                this.totalCdnrTaxableValue = this.totalB2bTaxableValue + itm.itmDet.txval;
                this.totalCdnrIgst = itm.itmDet.iamt ? this.totalCdnrIgst + itm.itmDet.iamt : this.totalCdnrIgst;
                this.totalCdnrCgst = itm.itmDet.camt ? this.totalCdnrCgst + itm.itmDet.camt : this.totalCdnrCgst;
                this.totalCdnrSgst = itm.itmDet.samt ? this.totalCdnrSgst + itm.itmDet.samt : this.totalCdnrSgst;
                this.totalCdnrCess = itm.itmDet.csamt ? this.totalCdnrCess + itm.itmDet.csamt : this.totalCdnrCess;
            });
        });
    });



}

calculateB2cReport(): void {
    this.gstr1.b2cs.forEach(b2cPerCus => {
        this.totalB2cInvoicesNo++;
        this.totalB2cInvoiceValue = this.totalB2cInvoiceValue + b2cPerCus.txval + b2cPerCus.iamt + b2cPerCus.camt + b2cPerCus.samt;
        this.totalB2cTaxableValue = this.totalB2cTaxableValue + b2cPerCus.txval;
        this.totalB2cIgst = b2cPerCus.iamt ? this.totalB2cIgst + b2cPerCus.iamt : this.totalB2cIgst;
        this.totalB2cCgst = b2cPerCus.camt ? this.totalB2bCgst + b2cPerCus.camt : this.totalB2cCgst;
        this.totalB2cSgst = b2cPerCus.samt ? this.totalB2bSgst + b2cPerCus.samt : this.totalB2cSgst;
        this.totalB2cCess = b2cPerCus.csamt ? this.totalCdnrCess + b2cPerCus.csamt : this.totalB2cCess;
    });
}

calculateCdnurReport(): void {

    this.gstr1.cdnur.forEach(cdnur => {
        this.totalCdnurInvoicesNo++;
        this.totalCdnurInvoiceValue = this.totalCdnurInvoiceValue + cdnur.val;
        cdnur.itms.forEach(itm => {
            this.totalCdnurTaxableValue = this.totalCdnurTaxableValue + itm.itmDet.txval;
            this.totalCdnurIgst = itm.itmDet.iamt ? this.totalCdnurIgst + itm.itmDet.iamt : this.totalCdnurIgst;                                 //igst and  cess is not calculated giving NaN error at front end
            this.totalCdnurCess = itm.itmDet.iamt ? this.totalCdnurCess + itm.itmDet.csamt : this.totalCdnurIgst;
        });
    });

}

calculateAdvRevReport(): void {
    this.gstr1.at.forEach(advRev => {
        this.totalAdvRevInvoicesNo++;
        advRev.itms.forEach(itm => {
            this.totalAdvRevInvoiceValue = this.totalAdvRevInvoiceValue + + itm.itmDet.adAmt + itm.itmDet.iamt + itm.itmDet.camt + itm.itmDet.samt;
            this.totalAdvRevTaxableValue = this.totalAdvRevTaxableValue + itm.itmDet.adAmt;
            this.totalAdvRevIgst = itm.itmDet.iamt ? this.totalAdvRevIgst + itm.itmDet.iamt : this.totalAdvRevIgst;
            this.totalAdvRevCess = itm.itmDet.csamt ? this.totalAdvRevCess + itm.itmDet.csamt : this.totalAdvRevCess;
            this.totalAdvRevCgst = itm.itmDet.camt ? this.totalAdvRevCgst + itm.itmDet.camt : this.totalAdvRevCgst;
            this.totalAdvRevSgst = itm.itmDet.samt ? this.totalAdvRevSgst + itm.itmDet.samt : this.totalAdvRevSgst;
            
        });
    });
}
calculateHsnReport(): void{
    
    this.gstr1.hsn.forEach(hsnsac => {
        
        this.totalHsnInvoicesNo++;
        this.totalHsnInvoiceValue = this.totalHsnInvoiceValue + hsnsac.data.val;
        this.totalHsnTaxableValue = this.totalHsnTaxableValue + hsnsac.data.txval;
        this.totalHsnCgst = (hsnsac.data.iamt / 2) ? this.totalHsnCgst + (hsnsac.data.iamt / 2) : this.totalHsnCgst;//Todo logic check
        this.totalHsnSgst = (hsnsac.data.iamt / 2) ? this.totalHsnSgst + (hsnsac.data.iamt / 2) : this.totalHsnSgst;//Todo logic check
        this.totalHsnIgst = this.totalHsnIgst + hsnsac.data.iamt;
        this.totalHsnCess = this.totalHsnCess + hsnsac.data.csamt;
        });
   


}

calculateExportReport(): void {
    this.gstr1.exp.forEach(expt => {
        this.totalExportInvoicesNo++;
        this.totalExportInvoiceValue = this.totalExportInvoiceValue + expt.inv.val;
        expt.inv.itms.forEach(itm => {
                this.totalExportTaxableValue = this.totalExportTaxableValue + itm.itmDet.txval;
                this.totalExportIgst = itm.itmDet.iamt ? this.totalExportIgst + itm.itmDet.iamt : this.totalExportIgst;
            });
        
    });
}

getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

getMonthName(monthId: number): string {
    return this.monthList.find(m => m.value == monthId).label
}

export() {

    var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(this.gstr1));
    var dlAnchorElem = document.getElementById('downloadAnchorElem');
    dlAnchorElem.setAttribute("href", dataStr);
    dlAnchorElem.setAttribute("download", "gstr1.json");
    dlAnchorElem.click();
}


}
