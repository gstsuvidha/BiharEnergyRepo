import { Component, OnInit } from '@angular/core';
import { StateList } from '../../../shared/state-list';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-cdnur-for-accounting-unit',
  templateUrl: './cdnur-for-accounting-unit.component.html',
  styleUrls: ['./cdnur-for-accounting-unit.component.css']
})
export class CdnurForAccountingUnitComponent implements OnInit {

  public gstr1CreditDebitNoteUnregisteredList;
  placeOfSupply;
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

      this.gstr1ForAccountingUnitService.getCreditDebitNoteUnregisterForAccountingUnit(this.monthId,this.year,this.accountingUnitId)
    .subscribe(gstr1CreditDebitNoteUnregistered => {
         this.gstr1CreditDebitNoteUnregisteredList = gstr1CreditDebitNoteUnregistered;
          });

      this.gstr1ForAccountingUnitService.getCreditDebitNoteUnregisterForAccountingUnit(this.monthId,this.year,this.accountingUnitId).subscribe
          (pos => {
              this.placeOfSupply = pos;
          });
  });
  }
  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
