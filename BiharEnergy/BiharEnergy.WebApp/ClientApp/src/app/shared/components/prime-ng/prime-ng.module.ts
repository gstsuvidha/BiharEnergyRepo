import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTableModule } from 'primeng/components/datatable/datatable';
import { TableModule } from 'primeng/components/table/table';
import { PanelMenuModule } from 'primeng/components/panelmenu/panelmenu';
import { MenubarModule } from 'primeng/components/menubar/menubar';
import { ButtonModule } from 'primeng/components/button/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MessageService} from  'primeng/components/common/messageservice';
import { ConfirmationService } from 'primeng/api';
import {FieldsetModule} from 'primeng/fieldset';
import { GrowlModule } from 'primeng/growl';
import {InputTextModule} from 'primeng/inputtext';
import {DropdownModule} from 'primeng/dropdown';
import {CheckboxModule} from 'primeng/checkbox';
import {CardModule} from 'primeng/card';
import {DialogModule} from 'primeng/dialog';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {CalendarModule} from 'primeng/calendar';
import {RadioButtonModule} from 'primeng/radiobutton';
import {MessagesModule} from 'primeng/messages';
import {MessageModule} from 'primeng/message';


@NgModule({
  imports: [
    CommonModule,
    DataTableModule,
    TableModule,
    GrowlModule,
    ButtonModule, 
    MenubarModule, 
    PanelMenuModule,
    FieldsetModule,
    InputTextModule,
    DropdownModule,
    BrowserAnimationsModule,
    CheckboxModule,
    CardModule,
    DialogModule,
    ConfirmDialogModule,
    CalendarModule,
    RadioButtonModule,
    MessageModule,
    MessagesModule
  ],

  exports: [CommonModule,
    DataTableModule,
    TableModule,
    ButtonModule,
    MenubarModule,
    PanelMenuModule,
    BrowserAnimationsModule,
    GrowlModule,
    FieldsetModule,
    InputTextModule,
    DropdownModule,
    CheckboxModule,
    CardModule,
    DialogModule,
    ConfirmDialogModule,
    CalendarModule,
    RadioButtonModule,
    MessageModule,
    MessagesModule
  ],
  declarations: [],
  providers: [MessageService, ConfirmationService]
})
export class PrimeNgModule { }
