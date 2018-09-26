import { Component, OnInit } from '@angular/core';
import { StateList } from '../../shared/state-list';
import { ActivatedRoute } from '@angular/router';
import { Gstr1Service } from '../gstr1.service';

@Component({
  selector: 'app-b2c',
  templateUrl: './b2c.component.html',
  styleUrls: ['./b2c.component.css']
})
export class B2cComponent implements OnInit {

  public gstr1B2CList;
  stateList = StateList;
  monthId: number;
  year : number;
  companyId : number;

  constructor(private route : ActivatedRoute,private gstr1ReportingService: Gstr1Service) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.year = params['yearId'];
      this.companyId = params['companyId']

      this.gstr1ReportingService.getGstr1B2C(this.monthId,this.year,this.companyId)
      .subscribe(gstr1B2C => {
          this.gstr1B2CList = gstr1B2C;
          });
  });
  }
  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
