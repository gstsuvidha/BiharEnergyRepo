import { Component, OnInit } from '@angular/core';
import { MonthList } from '../../shared/month-list';
import { StateList } from '../../shared/state-list';
import { ActivatedRoute } from '@angular/router';
import { Gstr1Service } from '../gstr1.service';
import { UserProfileService } from '../../user-profile/userprofile.service';

@Component({
  selector: 'app-b2b',
  templateUrl: './b2b.component.html',
  styleUrls: ['./b2b.component.css']
})
export class B2bComponent implements OnInit {

  
  monthList = MonthList;
  public gstr1B2BList;
  public getCusName;
  stateList = StateList;
  monthId: number;
  year: number;
  companyId : number;
  invoicePrefix : string;

  constructor(private route: ActivatedRoute,private gstr1ReportingService: Gstr1Service,
              private userProfileService : UserProfileService) { }

  ngOnInit() {
     this.userProfileService.getInvoicePrefix().subscribe(invoicePrefix => this.invoicePrefix = invoicePrefix);
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.year = params['yearId'];
      this.companyId=params['companyId'];

      this.gstr1ReportingService.getGstr1B2B(this.monthId,this.year,this.companyId)
     .subscribe(gstr1B2B =>{ 
         this.gstr1B2BList = gstr1B2B;
         
    });


      this.gstr1ReportingService.getCustomerName(this.monthId,this.year,this.companyId).subscribe
          (cus => {
              this.getCusName = cus;
              
          });

  });
 

  }

  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
