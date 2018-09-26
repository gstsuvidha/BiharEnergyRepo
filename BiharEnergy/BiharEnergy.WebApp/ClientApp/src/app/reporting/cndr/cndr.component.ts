import { Component, OnInit } from '@angular/core';
import { StateList } from '../../shared/state-list';
import { Gstr1Service } from '../gstr1.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cndr',
  templateUrl: './cndr.component.html',
  styleUrls: ['./cndr.component.css'],
  
})
export class CndrComponent implements OnInit {

  public gstr1CreditDebitNoteRegisteredList;
    public placeOfSupply;
    stateList = StateList;
    monthId: number;
    year: number;
    companyId : number;

  constructor(private gstr1ReportingService: Gstr1Service,private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.year = params['yearId'];
      this.companyId = params['companyId'];

       this.gstr1ReportingService.getCreditDebitNoteRegister(this.monthId,this.year,this.companyId)
      .subscribe(gstr1CreditDebitNoteRegistered => {
          this.gstr1CreditDebitNoteRegisteredList = gstr1CreditDebitNoteRegistered;
          });

       this.gstr1ReportingService.getPlaceOfSupplyCdnr(this.monthId,this.year,this.companyId).subscribe
           (pos => {
               this.placeOfSupply = pos;
          });

  });
  }

  getPlaceOfSupply(id: string): string {
    return this.stateList.find(s => s.value == id).label
}

}
