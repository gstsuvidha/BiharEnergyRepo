import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-hsn-for-accounting-unit',
  templateUrl: './hsn-for-accounting-unit.component.html',
  styleUrls: ['./hsn-for-accounting-unit.component.css']
})
export class HsnForAccountingUnitComponent implements OnInit {

  monthId: number;
  yearId : number;
  accountingUnitId : number

  public gstr1HsnList;

  constructor(private route : ActivatedRoute,
              private gstr1ForAccountingUnitService : Gstr1ForAccountingUnitService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.accountingUnitId = params['accountingUnitId'];


      this.gstr1ForAccountingUnitService.getGstr1HsnForAccountingUnit(this.monthId,this.yearId,this.accountingUnitId)
      .subscribe(gstr1Hsn => {
          this.gstr1HsnList = gstr1Hsn;
      });

  });
  }

}
