import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {InputTextModule} from 'primeng/inputtext';
import {DropdownModule} from 'primeng/dropdown';
import {ButtonModule} from 'primeng/button';
import {MenubarModule} from 'primeng/menubar';
import {PanelMenuModule} from 'primeng/panelmenu';
import {TableModule} from 'primeng/table';
import {FieldsetModule} from 'primeng/fieldset';
import {CardModule} from 'primeng/card';
import {CheckboxModule} from 'primeng/checkbox';
import {InputTextareaModule} from 'primeng/inputtextarea';



@NgModule({
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    InputTextModule,
    DropdownModule,
    ButtonModule,
    MenubarModule,
    PanelMenuModule,
    TableModule,
    FieldsetModule,
    CardModule,
    CheckboxModule,
    InputTextareaModule
  ],
  exports: [
    BrowserAnimationsModule,
    InputTextModule,
    DropdownModule,
    ButtonModule,
    MenubarModule,
    PanelMenuModule,
    TableModule,
    FieldsetModule,
    CardModule,
    CheckboxModule,
    InputTextareaModule
  ],
  declarations: [
    
]
})
export class NgPrimeModule { }
