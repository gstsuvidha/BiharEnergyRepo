import { Component, OnInit } from '@angular/core';
import { StateList } from '../../../shared/state-list';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-export-for-accounting-unit',
  templateUrl: './export-for-accounting-unit.component.html',
  styleUrls: ['./export-for-accounting-unit.component.css']
})
export class ExportForAccountingUnitComponent implements OnInit {

  public gstr1ExportReportDetailList;
  stateList = StateList;
  monthId: number;
  yearId : number;
  accountingUnitId : number;

  constructor(private route : ActivatedRoute,
              private gstr1ForAccountingUnitService : Gstr1ForAccountingUnitService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.accountingUnitId = params['accountingUnitId'];

      this.gstr1ForAccountingUnitService.getGstr1ExportInvoiceDetailForAccountingUnit(this.monthId,this.yearId,this.accountingUnitId)
          .subscribe(gstr1ExportReportDetail => {
              this.gstr1ExportReportDetailList = gstr1ExportReportDetail;
          });
  });
  }
  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}
}
