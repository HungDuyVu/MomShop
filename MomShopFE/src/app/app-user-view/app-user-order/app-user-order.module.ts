import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppUserVoteComponent } from './app-user-vote/app-user-vote.component';
import { AppBsModalModule } from 'src/directive/app-bs-modal.module';
import { AppUserOrderDeliveryComponent } from './app-user-order-delivery/app-user-order-delivery.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    AppUserVoteComponent,
    AppUserOrderDeliveryComponent
  ],
  imports: [
    CommonModule,
    AppBsModalModule,
    FormsModule
  ],
  exports:[AppUserVoteComponent,AppUserOrderDeliveryComponent]
})
export class AppUserOrderModule { }
