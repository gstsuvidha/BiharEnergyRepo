import { Component, OnInit } from '@angular/core';
import { MonthList } from '../../../shared/month-list';
import { StateList } from '../../../shared/state-list';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Gstr1ForAccountingUnitService } from '../../gstr1-for-accounting-unit.service';

@Component({
  selector: 'app-b2b-for-accounting-unit',
  templateUrl: './b2b-for-accounting-unit.component.html',
  styleUrls: ['./b2b-for-accounting-unit.component.css']
})
export class B2bForAccountingUnitComponent implements OnInit {

  monthList = MonthList;
  public gstr1B2BList;
  public getCusName;
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

      this.gstr1ForAccountingUnitService.getGstr1ForAccountingUnitB2B(this.accountingUnitId,this.monthId,this.year)
      .subscribe(gstr1B2B =>{ 
        this.gstr1B2BList = gstr1B2B;
        
   });

   this.gstr1ForAccountingUnitService.getCustomerName(this.accountingUnitId,this.monthId,this.year).subscribe
          (cus => {
              this.getCusName = cus;
              
          });
  })
}

getPlaceOfSupply(id: string): string {
  return this.stateList.find(s => s.value == id).label
}

}
