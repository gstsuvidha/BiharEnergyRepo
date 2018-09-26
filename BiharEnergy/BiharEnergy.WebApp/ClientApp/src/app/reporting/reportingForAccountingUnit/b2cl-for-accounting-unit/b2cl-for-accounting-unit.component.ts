import { Component, OnInit } from '@angular/core';
import { StateList } from '../../../shared/state-list';
import {  ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-b2cl-for-accounting-unit',
  templateUrl: './b2cl-for-accounting-unit.component.html',
  styleUrls: ['./b2cl-for-accounting-unit.component.css']
})
export class B2clForAccountingUnitComponent implements OnInit {

  public gstr1B2CLList;
  stateList = StateList;
  monthId: number;
  year : number;
  accountingUnitId : number;
  
  constructor(private route : ActivatedRoute,
              private gstr1ForAccountingUnit : Gstr1ForAccountingUnitService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.year = params['yearId'];
      this.accountingUnitId = params['accountingUnitId']

      this.gstr1ForAccountingUnit.getGstr1ForAccountingUnitB2CL(this.monthId,this.year,this.accountingUnitId)
      .subscribe(gstr1B2CL => {
          this.gstr1B2CLList = gstr1B2CL;
          });
  });
  }
  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
