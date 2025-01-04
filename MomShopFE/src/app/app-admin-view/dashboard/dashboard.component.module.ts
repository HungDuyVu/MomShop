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
import { SharedModule } from 'src/share.module';
import { AppAdminMenuComponent } from '../app-admin-menu/app-admin-menu.component';
import { DashboardComponent } from './dashboard.component';
import { ChartModule } from 'primeng/chart';
import { StyleClassModule } from 'primeng/styleclass';
import { CarouselModule } from 'primeng/carousel';
import { TagModule } from 'primeng/tag';
import { CalendarModule } from 'primeng/calendar';


@NgModule({
  declarations: [
    DashboardComponent,
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
    ChartModule,
    StyleClassModule,
    CarouselModule,
    TagModule,
    CalendarModule
  ]
})
export class DashboardModule { }
