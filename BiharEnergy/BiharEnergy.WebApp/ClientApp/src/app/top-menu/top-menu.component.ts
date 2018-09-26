import { Component, OnInit, ElementRef } from '@angular/core';
import { Router } from "@angular/router";

import { LoginService, IUserProfile } from '../login/login.service';


@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  host: {
    '(document:click)': 'handleClick($event)',
},
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {
  
  userProfile: IUserProfile =this.loginService.getUserProfile();
  toggleMenu:boolean=false;


  constructor(private router : Router,
              private loginService : LoginService,
              private elementRef: ElementRef) { }

  ngOnInit() {
  }
  //for getting the click event
  //outside the component to close the open dropdown
  //complete code is for reference this.toggleLogin = false if clicked outside is required
  //https://stackoverflow.com/questions/35712379/how-can-i-close-a-dropdown-on-click-outside
  handleClick(event){
    if (!this.elementRef.nativeElement.contains(event.target)) {
        this.toggleLogin =false;
      this.toggleMenu=false;
    }
}
 
  toggleLogin:boolean=false;
  onToggleLogin():void{
    //for logged in user profile/login toggle
    this.toggleLogin = !this.toggleLogin;     
  }

  logOut(): void {        
     this.loginService.logOut();        
    this.router.navigate(['/login']);
}

}
