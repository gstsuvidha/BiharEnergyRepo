import { Component, OnInit } from '@angular/core';
import { StateList } from '../../shared/state-list';
import { Gstr1Service } from '../gstr1.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-advance-receive',
  templateUrl: './advance-receive.component.html',
  styleUrls: ['./advance-receive.component.css']
})
export class AdvanceReceiveComponent implements OnInit {

  public gstr1AdvanceReceiveList;
  stateList = StateList;
  monthId: number;
  year : number;
  companyId : number;

  constructor(private gstr1ReportingService: Gstr1Service,private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.year = params['yearId'];
      this.companyId = params['companyId']

  this.gstr1ReportingService.getAdvanceReceive(this.monthId,this.year,this.companyId)
      .subscribe(gstr1AdvanceReceive => {
          this.gstr1AdvanceReceiveList = gstr1AdvanceReceive;
          });
  });
  }

  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
