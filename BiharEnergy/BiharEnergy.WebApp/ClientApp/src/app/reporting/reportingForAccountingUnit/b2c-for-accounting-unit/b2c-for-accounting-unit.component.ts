import { Component, OnInit } from '@angular/core';
import { StateList } from '../../../shared/state-list';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-b2c-for-accounting-unit',
  templateUrl: './b2c-for-accounting-unit.component.html',
  styleUrls: ['./b2c-for-accounting-unit.component.css']
})
export class B2cForAccountingUnitComponent implements OnInit {

  public gstr1B2CList;
  stateList = StateList;
  monthId: number;
  year : number;
  accountingUnitId : number;

  constructor(private route : ActivatedRoute,
              private gstr1ForAccountingUnitService : Gstr1ForAccountingUnitService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.year = params['yearId'];
      this.accountingUnitId = params['accountingUnitId']

      this.gstr1ForAccountingUnitService.getGstr1ForAccountingUnitB2C(this.monthId,this.year,this.accountingUnitId)
      .subscribe(gstr1B2C => {
          this.gstr1B2CList = gstr1B2C;
          });
  });
  }
  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
