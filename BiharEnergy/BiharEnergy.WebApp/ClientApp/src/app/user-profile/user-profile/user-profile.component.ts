import { Component, OnInit } from '@angular/core';
import { SelectItem, Message } from 'primeng/api';
import { StateList } from '../../shared/state-list';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserProfileService } from '../userprofile.service';
import { Iuserprofile } from '../iuserprofile';
import { ActivatedRoute, Router } from '@angular/router';
import { LoadingService } from '../../shared/Utilities/loading.service';
import { UserSetting } from '../../shared/Utilities/user-setting';
import { Company } from '../../reporting/home/company';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  registrationType : SelectItem[];
  stateList = StateList;
  userprofileForm: FormGroup;
  action: string;
  userprofile : Iuserprofile;
  pageTitle;
  companies : Company[];
  companySelectList : SelectItem[];
  msgs:Message[] = [];
  financialYearList : SelectItem[];
  constructor(private fb: FormBuilder,
              private userProfileService : UserProfileService,
              private route : ActivatedRoute,
              private loadingService : LoadingService,
              private router : Router) { }

  ngOnInit() {

    this.userProfileService.getCompany().subscribe(companies => {
        this.companies = companies;
        this.companySelectList = this.companies.map(cl => (
          {
            label: cl.name,
            value: cl.id
          }));
      });
    this.registrationType = [
      {label : "Registered", value : 0},
      {label : "Unregistered", value : 1},
      {label : "Composite Dealer", value : 2},
    ];
    this.financialYearList = [
      {label : "2018-19",value : 2018},
      {label : "2019-20",value : 2019},
      {label : "2020-21",value : 2020},
      {label : "2021-22",value : 2021},
      {label : "2022-23",value : 2022}
    ];
    

    this.userprofileForm = this.fb.group({
      id: '',
      businessName: ['', [Validators.required, Validators.minLength(2)]],
      gstin: ['', [Validators.required, Validators.pattern('[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}')]],
      email: ['', [Validators.required, Validators.email]],
      address: [''],
      companyId:[''],
      contactNumber: ['', [Validators.required, Validators.maxLength(10), Validators.minLength(10)]],
      turnOver: ['', [Validators.required]],
      bankAccountName: [''],
      bankAccountNumber: [''],
      ifscCode: [''],
      placeOfSupply:[''],
      registrationType: ['',Validators.required ],
      selectedYear:['',Validators.required],
      termsAndCondition: ['',[]],
      currentGrossTurnOver: ['', [Validators.required]],
      invoicePrefix : [''],
      pan : ['']
  });
  this.userProfileService.getProfile().subscribe(up => {
    this.userprofile = up;
    console.log(up);
    this.userprofileForm.patchValue({
        id: this.userprofile.id,
        businessName: this.userprofile.businessName,
        gstin: this.userprofile.gstin,
        email: this.userprofile.email,
        address: this.userprofile.address,
        contactNumber: this.userprofile.contactNumber,
        turnOver: this.userprofile.turnOver,
        bankAccountName: this.userprofile.bankAccountName,
        bankAccountNumber: this.userprofile.bankAccountNumber,
        ifscCode: this.userprofile.ifscCode,
        registrationType: this.userprofile.registrationType,
        companyId:this.userprofile.companyId,
        placeOfSupply: this.userprofile.placeOfSupply,
        selectedYear: this.userprofile.selectedYear,
        termsAndCondition: this.userprofile.termsAndCondition,
        invoicePrefix:this.userprofile.invoicePrefix,
        currentGrossTurnOver: this.userprofile.currentGrossTurnOver,
        pan : this.userprofile.pan
    });
        this.route.params.subscribe(params => {
            this.action = params['action'];
            if (this.action == 'edit') {
                this.userprofileForm.enable();
            }
            else {
                this.userprofileForm.disable();
            }
        
    });
});
this.pageTitle = `User Profile`;


  }

  onSave(): void {

    if (this.userprofileForm.dirty && this.userprofileForm.valid) {

        let p = Object.assign({}, this.userprofile, this.userprofileForm.value);
     
        this.loadingService.busy = this.userProfileService.updateProfile(p)
            .subscribe(() => this.onSaveComplete());
  
  

    }


    else if (!this.userprofileForm.dirty) {
        this.onSaveComplete();
    }
}

private onSaveComplete(): void {

   
  
  UserSetting.setYearSettings(this.userprofileForm.get('selectedYear').value);
  this.msgs = [];
  this.msgs.push({
    severity : 'success',
    summary : 'Success Message',
    detail : 'User Profile Updated' 
  });

  // Reset the form to clear the flags
  this.userprofileForm.reset();
  this.router.navigate(['/customer']);
}

}
