import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { AppBsModalModule } from 'src/directive/app-bs-modal.module';
import { CalendarModule } from 'primeng/calendar';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MenuModule } from 'primeng/menu';
import { AppAdminManagementOrderComponent } from './app-admin-management-order.component';
import { CreateOrEditOrderComponent } from './create-or-edit-order/create-or-edit-order.component';
import { TimelineModule } from 'primeng/timeline';
import { ViewOrderComponent } from './view-order/view-order.component';
import { TagModule } from 'primeng/tag';

@NgModule({
  declarations: [
    AppAdminManagementOrderComponent,
    CreateOrEditOrderComponent,
    ViewOrderComponent
  ],
  imports: [
    CommonModule,
    TableModule,
    BrowserAnimationsModule,
    AppBsModalModule, FormsModule,
    DropdownModule,
    InputTextModule,
    ButtonModule,
    ToastrModule,
    CalendarModule,
    ToastModule,
    DynamicDialogModule,
    ConfirmDialogModule,
    MenuModule,
    TimelineModule,
    TagModule
  ]
})
export class AppAdminManagementOrdertModule { }

