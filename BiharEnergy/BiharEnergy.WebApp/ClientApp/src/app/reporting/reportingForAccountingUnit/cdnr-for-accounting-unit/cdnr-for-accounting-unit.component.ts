import { Component, OnInit } from '@angular/core';
import { StateList } from '../../../shared/state-list';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-cdnr-for-accounting-unit',
  templateUrl: './cdnr-for-accounting-unit.component.html',
  styleUrls: ['./cdnr-for-accounting-unit.component.css']
})
export class CdnrForAccountingUnitComponent implements OnInit {

  public gstr1CreditDebitNoteRegisteredList;
    public placeOfSupply;
    stateList = StateList;
    monthId: number;
    year: number;
    accountingUnitId : number;
    
  constructor(private route : ActivatedRoute,
              private gstr1ForAccountingUnitService : Gstr1ForAccountingUnitService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.year = params['yearId'];
      this.accountingUnitId = params['accountingUnitId'];

       this.gstr1ForAccountingUnitService.getCreditDebitNoteRegisterForAccountingUnit(this.monthId,this.year,this.accountingUnitId)
      .subscribe(gstr1CreditDebitNoteRegistered => {
          this.gstr1CreditDebitNoteRegisteredList = gstr1CreditDebitNoteRegistered;
          });

       this.gstr1ForAccountingUnitService.getPlaceOfSupplyCdnrForAccountingUnit(this.monthId,this.year,this.accountingUnitId).subscribe
           (pos => {
               this.placeOfSupply = pos;
          });

  });
  }
  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
