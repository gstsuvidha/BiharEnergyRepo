import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-document-detail-for-accounting-unit',
  templateUrl: './document-detail-for-accounting-unit.component.html',
  styleUrls: ['./document-detail-for-accounting-unit.component.css']
})
export class DocumentDetailForAccountingUnitComponent implements OnInit {

  public gstr1DocumentDetailList;
  monthId: number;
  yearId: number;
  accountingUnitId: number;

  constructor(private route : ActivatedRoute,
              private gstr1ForAccountingUnitService : Gstr1ForAccountingUnitService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.accountingUnitId = params['accountingUnitId'];

      this.gstr1ForAccountingUnitService.getGstr1DocumentDetailForAccountingUnit(this.accountingUnitId,this.monthId,this.yearId)
        .subscribe(gstr1DocumentDetail => {
          this.gstr1DocumentDetailList = gstr1DocumentDetail;

        });
    });
  }

}
