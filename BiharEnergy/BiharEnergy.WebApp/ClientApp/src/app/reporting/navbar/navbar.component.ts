import { Component, OnInit } from '@angular/core';
import {MenuItem} from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';
import { Month } from '../../shared/month-list';
import { Company, CompanyList } from '../home/company';
import { UserProfileService } from '../../user-profile/userprofile.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  items: MenuItem[];
  monthId : number;
  companyId : number;
  monthList = Month
  companyList = CompanyList;
  yearId : number;
  companyDisplay : string;
  constructor(private route : ActivatedRoute,private userprofileService : UserProfileService, 
              private router : Router) { }

  ngOnInit() {
    
   
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.companyId = params['companyId'];

    })


}
getMonthName(monthId: number): string {
  return this.monthList.find(m => m.value == monthId).label
}

getCompanyName(companyId : number) : string {
  return this.companyList.find(c => c.value == companyId).label
}

// navigate(){
//   this.router.navigate(['../../../',{relativeTo : this.route}])
// }


}