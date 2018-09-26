import { Component, OnInit } from '@angular/core';
import { MonthList } from '../../../shared/month-list';
import { StateList } from '../../../shared/state-list';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { UserProfileService } from '../../../user-profile/userprofile.service';
import { Gstr3bForAccountingService } from '../../gstr3bForAccounting.service';

@Component({
  selector: 'app-gstr3b-for-accounting',
  templateUrl: './gstr3b-for-accounting.component.html',
  styleUrls: ['./gstr3b-for-accounting.component.css']
})
export class Gstr3bForAccountingComponent implements OnInit {

  currentYear = new Date();
  displayYear: string;
  monthId: number;
  monthList = MonthList;
  gstr3b;
  yearId : number;
  company : number;
  accountingUnit : number;

  stateList = StateList

  constructor(private route : ActivatedRoute,
              private userProfileService : UserProfileService,
              private gstr3bService: Gstr3bForAccountingService) { }

  ngOnInit() {
    this.displayYear = this.userProfileService.getYearDisplaySettings();
    this.route.params.subscribe(params => {

    this.monthId = params['monthId'];
    this.yearId = params['yearId'];
    this.company = params['companyId'];
    this.accountingUnit = params['accountingUnitId']
       this.gstr3bService.index(this.monthId,this.yearId,this.accountingUnit).subscribe(data => {
         this.gstr3b = data;
         console.log(data);
        });
       
    });
  }

  getPlaceOfSupply(id:string):string{
    return this.stateList.find(s => s.value == id).label
}

getMonthName(monthId: number): string {
  return this.monthList.find(m => m.value == monthId).label
}


}
