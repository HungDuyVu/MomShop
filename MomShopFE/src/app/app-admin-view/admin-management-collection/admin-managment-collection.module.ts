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
import { ToastModule } from 'primeng/toast';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { AdminManagementCollectionComponent } from './admin-management-collection.component';
import { MenuModule } from 'primeng/menu';
import { CreateOrEditCollectionComponent } from './create-or-edit-collection/create-or-edit-collection.component';
import { CreateOrEditProductCollectionComponent } from './create-or-edit-product-collection/create-or-edit-product-collection.component';
import { TreeTableModule } from 'primeng/treetable';
import { TagModule } from 'primeng/tag';

@NgModule({
  declarations: [
    AdminManagementCollectionComponent,
    CreateOrEditCollectionComponent,
    CreateOrEditProductCollectionComponent
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
    TreeTableModule,
    TagModule
  ]
})
export class AdminManagementCollectionModule { }
