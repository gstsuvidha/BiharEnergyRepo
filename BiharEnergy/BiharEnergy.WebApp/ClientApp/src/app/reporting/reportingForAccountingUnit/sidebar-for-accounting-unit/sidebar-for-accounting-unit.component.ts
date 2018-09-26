import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-sidebar-for-accounting-unit',
  templateUrl: './sidebar-for-accounting-unit.component.html',
  styleUrls: ['./sidebar-for-accounting-unit.component.css']
})
export class SidebarForAccountingUnitComponent implements OnInit {

  monthId : number;
  yearId : number;
  companyId : number;
  accountingUnitId : number;

  constructor(private route : ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.companyId = params['companyId'];
      this.accountingUnitId = params['accountingUnitId'];
  });
  }

}
