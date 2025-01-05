import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppAdminFeedbackUserComponent } from "./app-admin-view/app-admin-feedback-user/app-admin-feedback-user.component";
import { AppAdminManagementProductComponent } from "./app-admin-view/app-admin-management-product/app-admin-management-product.component";
import { AppAdminMangementCustomerComponent } from "./app-admin-view/app-admin-mangement-customer/app-admin-mangement-customer.component";
import { AppAdminManagementImportProductComponent } from "./app-admin-view/app-admin-management-import-product/app-admin-management-import-product.component";
import { AppUserMainComponent } from "./app-user-view/app-user-main/app-user-main.component";
import { AdminManagementCollectionComponent } from "./app-admin-view/admin-management-collection/admin-management-collection.component";
import { AppUserCartComponent } from "./app-user-view/app-user-cart/app-user-cart.component";
import { LoginComponent } from "./app-login/login/login.component";
import { AppUserRegisterComponent } from "./app-user-view/app-user-register/app-user-register.component";
import { AppAdminMenuComponent } from "./app-admin-view/app-admin-menu/app-admin-menu.component";
import { ExchangePolicyComponent } from "./app-user-view/app-user-policy/exchange-policy/exchange-policy.component";
import { GeneralTradingTermsComponent } from "./app-user-view/app-user-policy/general-trading-terms/general-trading-terms.component";
import { AppUserHomeComponent } from "./app-user-view/app-user-home/app-user-home.component";
import { UserCollectionComponent } from "./app-user-view/user-collection/user-collection.component";
import { CartComponent } from "./app-user-view/cart/cart.component";
import { UserCollectionDetailComponent } from "./app-user-view/user-collection-detail/user-collection-detail.component";
import { CheckOutComponent } from "./app-user-view/check-out/check-out.component";
import { AppUserOrderComponent } from "./app-user-view/app-user-order/app-user-order.component";
import { CreateOrEditCollectionComponent } from "./app-admin-view/admin-management-collection/create-or-edit-collection/create-or-edit-collection.component";
import { CreateOrEditProductTestComponent } from "./app-admin-view/app-admin-management-product/create-or-edit-product-test/create-or-edit-product-test.component";
import { AppUserProfileComponent } from "./app-user-view/app-user-profile/app-user-profile.component";
import { CreatOrEditImportProductComponent } from "./app-admin-view/app-admin-management-import-product/creat-or-edit-import-product/creat-or-edit-import-product.component";
import { AppAdminManagementOrderComponent } from "./app-admin-view/app-admin-management-order/app-admin-management-order.component";
import { CreateOrEditOrderComponent } from "./app-admin-view/app-admin-management-order/create-or-edit-order/create-or-edit-order.component";
import { DashboardComponent } from "./app-admin-view/dashboard/dashboard.component";
import { AppUserViewResponseComponent } from "./app-user-view/app-user-view-response/app-user-view-response.component";

const routes: Routes = [
  { path: "", redirectTo: "/login", pathMatch: "full" },
  //{ path: 'admin/product', component: AppAdminManagementProductComponent },
  
  { path: "login", component: LoginComponent },
  {
    path: "admin/importProduct",
    component: AppAdminManagementImportProductComponent,
  },

  { path: "home", component: AppUserHomeComponent },
  // { path: 'cart', component: AppUserCartComponent},
  { path: "register", component: AppUserRegisterComponent },
  // Menu
  {
    path: "admin",
    component: AppAdminMenuComponent,
    children: [
      { path: "dashboard", component: DashboardComponent },
      { path: "customer", component: AppAdminMangementCustomerComponent },
      {
        path: "product-management",
        children: [
          { path: "product", component: AppAdminManagementProductComponent },
          { path: "product/create", component: CreateOrEditProductTestComponent },
          { path: "product/detail", component: CreateOrEditProductTestComponent },
        ],
      },
      {
        path: "collection-management",
        children: [
          { path: "collection", component: AdminManagementCollectionComponent },
          {
            path: "collection/create",
            component: CreateOrEditCollectionComponent,
          },
          {
            path: "collection/detail",
            component: CreateOrEditCollectionComponent,
          },
        ],
      },
      {
        path: "order-management",
        children: [
          { path: "order", component: AppAdminManagementOrderComponent },
          { path: "order/:id", component: CreateOrEditOrderComponent },
        ],
      },
      {
        path: "received-order",
        children: [
          { path: "orders", component: AppAdminManagementImportProductComponent },
          { path: "order/create", component: CreatOrEditImportProductComponent },
          { path: "order/detail", component: CreatOrEditImportProductComponent },
        ],
      },
      { path: "feedback", component: AppAdminFeedbackUserComponent },
    ],
  },
  { path: "general-trading-terms", component: GeneralTradingTermsComponent },
  { path: "view", component: AppUserMainComponent },
  { path: "collection", component: UserCollectionComponent },
  { path: "collection-detail", component: UserCollectionDetailComponent },
  { path: "cart", component: CartComponent },
  { path: "check-out", component: CheckOutComponent },
  { path: "order", component: AppUserOrderComponent },
  { path: "profile", component: AppUserProfileComponent },
  { path: "response", component: AppUserViewResponseComponent },
  //{ path: 'vote', component: AppUserProfileComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
