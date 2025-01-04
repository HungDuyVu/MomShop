import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppAdminMangementCustomerComponent } from './app-admin-mangement-customer.component';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { AppBsModalModule } from 'src/directive/app-bs-modal.module';
import { CreateOrEditCustomerComponent } from './create-or-edit-customer/create-or-edit-customer.component';
import { SharedModule } from 'src/share.module';
import { AppAdminMenuComponent } from '../app-admin-menu/app-admin-menu.component';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TagModule } from 'primeng/tag';
import { MenuModule } from 'primeng/menu';
import { CalendarModule } from 'primeng/calendar';



@NgModule({
  declarations: [
    AppAdminMangementCustomerComponent,
    CreateOrEditCustomerComponent,
  ],
  imports: [
    CommonModule,
    TableModule,
    BrowserAnimationsModule,
    AppBsModalModule,FormsModule,
    DropdownModule,
    InputTextModule,
    ButtonModule,
    ToastrModule,
    ToastModule,
    ConfirmDialogModule,
    TagModule,
    MenuModule,
    CalendarModule
  ]
})
export class AppAdminMangementCustomerModule { }
