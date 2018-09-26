import { Component, OnInit } from '@angular/core';
import { MonthList } from '../../shared/month-list';
import { StateList } from '../../shared/state-list';
import { ActivatedRoute } from '@angular/router';
import { UserProfileService } from '../../user-profile/userprofile.service';
import { Gstr3bService } from '../gstr3b.service';

@Component({
  selector: 'app-gstr3b',
  templateUrl: './gstr3b.component.html',
  styleUrls: ['./gstr3b.component.css']
})
export class Gstr3bComponent implements OnInit {

  currentYear = new Date();
  displayYear: string;
  monthId: number;
  monthList = MonthList;
  
  gstr3b;
  yearId : number;
  company : number;
  

  stateList = StateList

  constructor(private route : ActivatedRoute,
              private userProfileService : UserProfileService,
              private gstr3bService: Gstr3bService) { }

  ngOnInit() {
    this.displayYear = this.userProfileService.getYearDisplaySettings();
    this.route.params.subscribe(params => {

    this.monthId = params['monthId'];
    this.yearId = params['yearId'];
    this.company = params['companyId'];
       this.gstr3bService.index(this.monthId,this.yearId,this.company).subscribe(data => {
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
