import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TdsFormComponent } from './tds-form/tds-form.component';
import { TdsListComponent } from './tds-list/tds-list.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticatedUserComponent } from '../authenticated-user/authenticated-user.component';
import { AuthGuardService } from '../shared/guards/auth-guard.service';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


const routes: Routes = [{
  path : 'authenticated',
  component : AuthenticatedUserComponent,
  canActivate: [AuthGuardService],
  children : [
    { path: 'tds', component: TdsListComponent },
    { path: 'tds/:id', component: TdsFormComponent },
    
  ]
}]


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ],
  declarations: [TdsFormComponent, TdsListComponent]
})
export class TdsModule { }
