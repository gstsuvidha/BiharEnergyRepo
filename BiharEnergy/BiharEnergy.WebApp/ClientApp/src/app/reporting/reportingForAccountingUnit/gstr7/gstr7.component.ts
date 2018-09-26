import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserProfileService } from '../../../user-profile/userprofile.service';
import { Gstr7Service } from '../../gstr7.service';
import { MonthList } from '../../../shared/month-list';
import { StateList } from '../../../shared/state-list';

@Component({
  selector: 'app-gstr7',
  templateUrl: './gstr7.component.html',
  styleUrls: ['./gstr7.component.css']
})
export class Gstr7Component implements OnInit {
  currentYear = new Date();
  displayYear: string;
  monthId: number;
  monthList = MonthList;
  gstr7;
  gstr7Details;
  yearId : number;
  company : number;
  accountingUnit : number;

  stateList = StateList

  constructor(private route : ActivatedRoute,
    private userProfileService : UserProfileService,
    private gstr7service: Gstr7Service) { }

  ngOnInit()
  {
    this.displayYear = this.userProfileService.getYearDisplaySettings();
    this.route.params.subscribe(params => {

    this.monthId = params['monthId'];
    this.yearId = params['yearId'];
    this.company = params['companyId'];
    this.accountingUnit = params['accountingUnitId']

    this.gstr7service.getGstr7Details(this.accountingUnit).subscribe(data => {
      this.gstr7Details = data;
    
    });

       this.gstr7service.getAll(this.monthId,this.yearId,this.accountingUnit).subscribe(data => {
         this.gstr7 = data;
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
