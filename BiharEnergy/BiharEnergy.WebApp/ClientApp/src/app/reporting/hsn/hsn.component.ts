import { Component, OnInit } from '@angular/core';

import { Router, ActivatedRoute } from "@angular/router";
import { Gstr1Service } from '../gstr1.service';

@Component({
  selector: 'app-hsn',
  templateUrl: './hsn.component.html',
  styleUrls: ['./hsn.component.css']
})
export class HsnComponent implements OnInit {

 
  monthId: number;
  yearId : number;
  company : number

  public gstr1HsnList;
  
  constructor(
              private route : ActivatedRoute,
              private gstr1ReportingService : Gstr1Service ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.company = params['companyId'];


      this.gstr1ReportingService.getGstr1Hsn(this.monthId,this.yearId,this.company)
      .subscribe(gstr1Hsn => {
          this.gstr1HsnList = gstr1Hsn;
      });

  });
}
  
}
