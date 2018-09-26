import { Component, OnInit } from '@angular/core';
import { StateList } from '../../shared/state-list';
import { Gstr1Service } from '../gstr1.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.css']
})
export class ExportComponent implements OnInit {

  public gstr1ExportReportDetailList;
  stateList = StateList;
  monthId: number;
  yearId : number;
  company : number


  constructor(private gstr1ReportingService: Gstr1Service,private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.company = params['companyId'];

      this.gstr1ReportingService.getGstr1ExportInvoiceDetail(this.monthId,this.yearId,this.company)
          .subscribe(gstr1ExportReportDetail => {
              this.gstr1ExportReportDetailList = gstr1ExportReportDetail;
          });
  });
  }

  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
