import { Component, OnInit } from '@angular/core';
import { Company } from './company';
import { Month } from '../../shared/month-list';
import { UserProfileService } from '../../user-profile/userprofile.service';
import { SelectItem } from 'primeng/api';
import { UserSetting } from '../../shared/Utilities/user-setting';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  companySelectList : SelectItem[];
  companies : Company[];
  period : string;
  month = Month;
  selectedMonth: number;
  financialYear : number;
  selectedCompany : Company;
  company: number;
  financialYearList : SelectItem[];

  constructor(private userProfileService : UserProfileService) { }

  ngOnInit() {


    this.userProfileService.getCompany().subscribe(companies => {
      this.companies = companies;
      this.companySelectList = this.companies.map(cl => (
        {
          label: cl.name,
          value: cl.id
        }));
    });

    this.financialYearList = [
      {label : "2018-19",value : 2018},
      {label : "2019-20",value : 2019},
      {label : "2020-21",value : 2020},
      {label : "2021-22",value : 2021},
      {label : "2022-23",value : 2022}
    ]
   

    
    this.financialYear = 2018;
    this.company = 1;
    this.getReportingForSelectedMonth(UserSetting.getReportingMonth());

  }
  getReportingForSelectedMonth(monthId : number){
     UserSetting.setReportingMonth(monthId);
     this.selectedMonth = monthId;
  }

}
