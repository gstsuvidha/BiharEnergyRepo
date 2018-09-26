import { Component, OnInit } from '@angular/core';
import { StateList } from '../../../shared/state-list';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-advance-receive-for-accounting-unit',
  templateUrl: './advance-receive-for-accounting-unit.component.html',
  styleUrls: ['./advance-receive-for-accounting-unit.component.css']
})
export class AdvanceReceiveForAccountingUnitComponent implements OnInit {

  public gstr1AdvanceReceiveList;
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
      this.accountingUnitId = params['accountingUnitId'];

  this.gstr1ForAccountingUnitService.getAdvanceReceiveForAccountingUnit(this.monthId,this.year,this.accountingUnitId)
      .subscribe(gstr1AdvanceReceive => {
          this.gstr1AdvanceReceiveList = gstr1AdvanceReceive;
          });
  });
  }
  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
