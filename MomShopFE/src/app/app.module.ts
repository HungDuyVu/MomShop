import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { FileUploadModule } from 'primeng/fileupload';
import { InputTextModule } from 'primeng/inputtext';
import { MenuModule } from 'primeng/menu';
import { TableModule } from 'primeng/table';
import { AppBsModalModule } from 'src/directive/app-bs-modal.module';
import { SharedModule } from 'src/share.module';
import { SidebarComponent } from './admin-layout/sidebar/sidebar.component';
import { CreateOrEditWeatherComponent } from './admin-layout/weather/create-or-edit-weather/create-or-edit-weather.component';
import { WeatherComponent } from './admin-layout/weather/weather.component';
import { AppAdminMainComponent } from './app-admin-view/app-admin-main/app-admin-main.component';
import { LoginComponent } from './app-login/login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { AppFooterComponent } from './app-user-view/app-footer/app-footer.component';
import { AppHeaderComponent } from './app-user-view/app-header/app-header.component';
import { AppNavComponent } from './app-user-view/app-nav/app-nav.component';
import { AppUserCartComponent } from './app-user-view/app-user-cart/app-user-cart.component';
import { AppUserHomeComponent } from './app-user-view/app-user-home/app-user-home.component';
import { AppUserMainComponent } from './app-user-view/app-user-main/app-user-main.component';
import { ExchangePolicyComponent } from './app-user-view/app-user-policy/exchange-policy/exchange-policy.component';
import { GeneralTradingTermsComponent } from './app-user-view/app-user-policy/general-trading-terms/general-trading-terms.component';
import { AppUserRegisterComponent } from './app-user-view/app-user-register/app-user-register.component';
import { AppComponent } from './app.component';
import { UserCollectionComponent } from './app-user-view/user-collection/user-collection.component';
import { CartComponent } from './app-user-view/cart/cart.component';
import { CheckOutComponent } from './app-user-view/check-out/check-out.component';
import { UserCollectionDetailComponent } from './app-user-view/user-collection-detail/user-collection-detail.component';
import { AppUserOrderComponent } from './app-user-view/app-user-order/app-user-order.component';
import { AppUserProfileComponent } from './app-user-view/app-user-profile/app-user-profile.component';
import { AppUserOrderDeliveryComponent } from './app-user-view/app-user-order/app-user-order-delivery/app-user-order-delivery.component';
import { AppAdminManagementOrderComponent } from './app-admin-view/app-admin-management-order/app-admin-management-order.component';
import { DashboardComponent } from './app-admin-view/dashboard/dashboard.component';
import { AppUserViewResponseComponent } from './app-user-view/app-user-view-response/app-user-view-response.component';

@NgModule({
  declarations: [
    AppComponent,
    WeatherComponent,
    SidebarComponent,
    AppUserMainComponent,
    AppAdminMainComponent,
    CreateOrEditWeatherComponent,
    LoginComponent,
    AppHeaderComponent,
    AppFooterComponent,
    AppUserCartComponent,
    AppUserRegisterComponent,
    AppUserHomeComponent,
    AppNavComponent,
    ExchangePolicyComponent,
    GeneralTradingTermsComponent,
    UserCollectionComponent,
    CartComponent,
    CheckOutComponent,
    UserCollectionDetailComponent,
    AppUserOrderComponent,
    AppUserProfileComponent,
    AppUserViewResponseComponent,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppBsModalModule,
    DropdownModule,
    SharedModule,
    InputTextModule,
    ButtonModule,
    FormsModule,
    CalendarModule,
    ToastrModule,
		MenuModule,
    FileUploadModule,
    DynamicDialogModule,
    ToastrModule.forRoot({
      positionClass :'toast-top-right',
    }),
  ],
  providers: [
    ToastrService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
