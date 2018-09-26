import { Component, OnInit } from '@angular/core';
import { Gstr1Service } from '../gstr1.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-document-detail',
  templateUrl: './document-detail.component.html',
  styleUrls: ['./document-detail.component.css']
})
export class DocumentDetailComponent implements OnInit {

  public gstr1DocumentDetailList;
  monthId: number;
  yearId: number;
  company: number


  constructor(private gstr1ReportingService: Gstr1Service, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.company = params['companyId'];

      this.gstr1ReportingService.getGstr1DocumentDetail(this.monthId,this.yearId,this.company)
        .subscribe(gstr1DocumentDetail => {
          this.gstr1DocumentDetailList = gstr1DocumentDetail;

        });
    });
  }

}
