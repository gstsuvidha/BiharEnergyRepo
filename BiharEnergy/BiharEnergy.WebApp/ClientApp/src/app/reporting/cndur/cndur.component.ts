import { Component, OnInit } from '@angular/core';
import { StateList } from '../../shared/state-list';
import { ActivatedRoute } from '@angular/router';
import { Gstr1Service } from '../gstr1.service';

@Component({
  selector: 'app-cndur',
  templateUrl: './cndur.component.html',
  styleUrls: ['./cndur.component.css']
})
export class CndurComponent implements OnInit {

    public gstr1CreditDebitNoteUnregisteredList;
    placeOfSupply;
    stateList = StateList;
    monthId: number;
    year : number;
    companyId : number;

  constructor(private gstr1ReportingService: Gstr1Service,private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.year = params['yearId'];
      this.companyId = params['companyId'];

      this.gstr1ReportingService.getCreditDebitNoteUnregister(this.monthId,this.year,this.companyId)
    .subscribe(gstr1CreditDebitNoteUnregistered => {
         this.gstr1CreditDebitNoteUnregisteredList = gstr1CreditDebitNoteUnregistered;
          });

      this.gstr1ReportingService.getPlaceOfSupplyCdnur(this.monthId,this.year,this.companyId).subscribe
          (pos => {
              this.placeOfSupply = pos;
          });
  });
  }

  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
