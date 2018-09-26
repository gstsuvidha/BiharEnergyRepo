import { PrimeNgModule } from './components/prime-ng/prime-ng.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { LoadingService } from './Utilities/loading.service';
import { AuthGuardService } from './guards/auth-guard.service';



const SharedComponents = [

 SidebarComponent
];



@NgModule({
  imports: [PrimeNgModule,
    CommonModule
  ],
  exports:[PrimeNgModule,
          SharedComponents],
 
          declarations: [ SidebarComponent],
          providers:[ LoadingService,AuthGuardService]
})
export class SharedModule { }
