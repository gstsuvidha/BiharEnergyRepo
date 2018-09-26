import { Component, OnInit } from '@angular/core';
import {MenuItem} from 'primeng/api';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-consolidated-sidebar',
  templateUrl: './consolidated-sidebar.component.html',
  styleUrls: ['./consolidated-sidebar.component.css']
})
export class ConsolidatedSidebarComponent implements OnInit {

  monthId : number;
  yearId : number;
  companyId : number;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    
    this.route.params.subscribe(params => {
      this.monthId = params['monthId'];
      this.yearId = params['yearId'];
      this.companyId = params['companyId'];
  });
  }

}
