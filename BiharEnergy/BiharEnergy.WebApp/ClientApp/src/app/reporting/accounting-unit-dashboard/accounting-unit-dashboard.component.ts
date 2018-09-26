import { Component, OnInit } from '@angular/core';
import { UserProfileService } from '../../user-profile/userprofile.service';
import { Iuserprofile } from '../../user-profile/iuserprofile';
import { SelectItem } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';
import { UserSetting } from '../../shared/Utilities/user-setting';

@Component({
  selector: 'app-accounting-unit-dashboard',
  templateUrl: './accounting-unit-dashboard.component.html',
  styleUrls: ['./accounting-unit-dashboard.component.css']
})
export class AccountingUnitDashboardComponent implements OnInit {

  userProfile : Iuserprofile[];
  accountingUnitSelectList : SelectItem[];
  accountingUnit : Iuserprofile;
  companyId : number;
  selectedMonth : number;
  financialYear : number;
  accountingUnitId : number;

  constructor(private userProfileService : UserProfileService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.companyId = params['companyId'];
      this.selectedMonth = params['monthId'];
      this.financialYear = params['yearId'];
      
      this.userProfileService.getAccountingUnitByCompanyId(this.companyId).subscribe(userProfile =>{
        this.userProfile = userProfile;
        this.accountingUnitSelectList = this.userProfile.map(au =>({
        label : au.businessName,
        value : au.id
  
        })) 
      })
    })

    this.getSelectedAccountingUnit(UserSetting.getAccountingUnit());
    
  }

  getSelectedAccountingUnit(accountingUnit : number){
    UserSetting.setAccountingUnit(accountingUnit);
    this.accountingUnitId = accountingUnit;
    console.log(this.accountingUnitId);

  }

}
