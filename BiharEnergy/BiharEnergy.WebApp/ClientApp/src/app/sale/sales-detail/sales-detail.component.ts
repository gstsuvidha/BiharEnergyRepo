import { Component, OnInit } from '@angular/core';
import { StateList } from '../../shared/state-list';
import { SalesInvoiceService } from '../sales-invoice-service';
import { ActivatedRoute } from '@angular/router';
import { ISalesInvoice } from '../isalesinvoice';

@Component({
  selector: 'app-sales-detail',
  templateUrl: './sales-detail.component.html',
  styleUrls: ['./sales-detail.component.css']
})
export class SalesDetailComponent implements OnInit {

  id: number;
   
  stateList = StateList;
  termAndCondition: string[];
  billedCustomerAddress: string[];
  accountingUnitAddress: string[];
  shppingCustomerAddress: string[];
  totalInvoiceValue: number;
  receiptAmt: number;
  dueAmt: number;
  salesInvoice;
  
  constructor(private salesInvoiceService: SalesInvoiceService,
              private route : ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getSalesInvoice(this.id);
});

  }

  private getSalesInvoice(id: number) {
    this.salesInvoiceService.getInvoiceForPrint(id).subscribe(sI => {
        this.salesInvoice = sI; this.termAndCondition = this.salesInvoice.accountingUnit.termsAndCondition.split("\n");
        this.billedCustomerAddress = this.salesInvoice.customer ? this.salesInvoice.customer.address.split(",") : null;
        this.accountingUnitAddress = this.salesInvoice.accountingUnit.address.split("\n");
        this.shppingCustomerAddress = this.salesInvoice.shippingAddress.split(",");
        this.totalInvoiceValue = this.salesInvoice.totalInvoiceValue;
        this.receiptAmt = this.salesInvoice.receivedAmount;
        this.dueAmt = Math.abs(this.totalInvoiceValue - this.receiptAmt);
     
    });
}
getPlaceOfSupply(id:string):string{
    return this.stateList.find(s => s.value == id).label
}
inWords(amount) :string {
  var words = new Array();
  words[0] = '';
  words[1] = 'One';
  words[2] = 'Two';
  words[3] = 'Three';
  words[4] = 'Four';
  words[5] = 'Five';
  words[6] = 'Six';
  words[7] = 'Seven';
  words[8] = 'Eight';
  words[9] = 'Nine';
  words[10] = 'Ten';
  words[11] = 'Eleven';
  words[12] = 'Twelve';
  words[13] = 'Thirteen';
  words[14] = 'Fourteen';
  words[15] = 'Fifteen';
  words[16] = 'Sixteen';
  words[17] = 'Seventeen';
  words[18] = 'Eighteen';
  words[19] = 'Nineteen';
  words[20] = 'Twenty';
  words[30] = 'Thirty';
  words[40] = 'Forty';
  words[50] = 'Fifty';
  words[60] = 'Sixty';
  words[70] = 'Seventy';
  words[80] = 'Eighty';
  words[90] = 'Ninty';
  amount = amount.toString();
 var atemp = amount.split(".");
  var number = atemp[0].split(",").join("");
  var n_length = number.length;
  var words_string = "";
  if (n_length <= 9) {
      var n_array = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
      var received_n_array = new Array();
      for (var i = 0; i < n_length; i++) {
          received_n_array[i] = number.substr(i, 1);
      }
      for (var i = 9 - n_length, j = 0; i < 9; i++, j++) {
          n_array[i] = received_n_array[j];
      }
      for (var i = 0, j = 1; i < 9; i++, j++) {
          if (i == 0 || i == 2 || i == 4 || i == 7) {
              if (n_array[i] == 1) {
                  n_array[j] = 10 + Number(n_array[j]);
                  n_array[i] = 0;
              }
          }
      }
      let value;
      for (var i = 0; i < 9; i++) {
          if (i == 0 || i == 2 || i == 4 || i == 7) {
              value = n_array[i] * 10;
          } else {
              value = n_array[i];
          }
          if (value != 0) {
              words_string += words[value] + " ";
          }
          if ((i == 1 && value != 0) || (i == 0 && value != 0 && n_array[i + 1] == 0)) {
              words_string += "Crores ";
          }
          if ((i == 3 && value != 0) || (i == 2 && value != 0 && n_array[i + 1] == 0)) {
              words_string += "Lakhs ";
          }
          if ((i == 5 && value != 0) || (i == 4 && value != 0 && n_array[i + 1] == 0)) {
              words_string += "Thousand ";
          }
          if (i == 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
              words_string += "Hundred and ";
          } else if (i == 6 && value != 0) {
              words_string += "Hundred ";
          }
      }
      words_string = words_string.split("  ").join(" ");
  }
  return words_string;
}
  print(): void {
  let printContents, popupWin;
  printContents = document.getElementById('print-section').innerHTML;
  popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
  popupWin.document.open();
  popupWin.document.write(`
<br/><br/><br/>
    <html>
      <head>
        <title>Print tab</title>
        <style type="text/css" media="print">
              
        @media print{
            @page {                
              size: A4;
              margin: 0mm;
          }

          html, body {
              width:1024px;
          }

          body {
              margin: 0 auto;
              padding: 10px 10px 10px 10px;
          }
          p{
              line-height: 10px;
             width:auto;
          padding:0px;
          magin-bottom:10px;
          font-size: 16px;
          text-align:left;
          font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
          }
          fieldset{
              position: relative;
          }
          legend {
          color:black;
          width:100px;
          border-collapse: collapse;
          font-size:110%;
          background-color: white;
          text-align:center;
          font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
          }
          table {
              table-layout:auto;
              border-collapse: collapse;
             align:center;
             
          }

.inWordsInv{
width:auto;
}
.receiveAmt{
width:20%;
}
.amt{
width:15%;
}
.invoice{
width:50%;
}

.tabletd{
width:50%;
}
.ttable{
width:100%;
}
.textleft{
text-align:left;
}
.textright{
text-align:right;
}

.texty {
  font-family: Calibri;
  font-weight: bold;
  font-size: 36px;
  color: black;
text-align:center;
 margin-bottom:30px;
}

.taxrateDiscount{
width:5%;
text-align:center;
}
.totalCal{
width:9%;
text-align:center;
}

.tdth{
width:7%;
text-align:center;
}
.sn{
width:3%;
text-align:center;
}
.description{
width:9%;
text-align:center;
}

          td,th,tbody,thead {
       
              border: 1px solid black;
              font-size: 14px; 
              font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;    
          }
          thead.head th{
              height: 40px; 
              text-align: center;
         
          }
          tbody.body td{
              
              height: 30px;
              text-align: center;
       
          }
          tbody.body th{
              
              height: 30px;
              text-align: right;

          }
          tfoot.foot th{
              border:none;
              height: 30px;
              text-align: right;
              }
          tfoot.foot td{
              border:none;
              height: 30px;
              text-align: right;
              }
          label {
          
          }

             }
          </style>
      </head>
  <body onload="window.print();window.close()">${printContents}</body>
    </html>`
  );
  popupWin.document.close();
}


}
