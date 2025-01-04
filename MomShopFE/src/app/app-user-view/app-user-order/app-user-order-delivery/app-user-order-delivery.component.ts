import { ViewChild } from '@angular/core';
import { Component } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { OrderService } from 'src/services/order.service';

@Component({
  selector: 'app-user-order-delivery',
  templateUrl: './app-user-order-delivery.component.html',
  styleUrls: ['./app-user-order-delivery.component.scss']
})
export class AppUserOrderDeliveryComponent {
  @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
  constructor(private userOrder: OrderService){
    
  }
  show(orderStatus) {
    this.modal.show();
    setTimeout(()=>{
      this.updateVisalizeStatusVod(orderStatus);
    },200)
  }
  close() {
    this.modal.hide();
  }
  updateVisalizeStatusVod(statusId) {
    let progress = document.querySelector<HTMLElement>(".vod-progress-visualize");
    let progressColor = document.querySelector<HTMLElement>(".vod-progress-color");
    progressColor.style.width = "0px";
    let block_process = document.querySelectorAll<HTMLElement>(".block-icon")
    let text_block = document.querySelectorAll<HTMLElement>(".text-block")

    for (let i = 0; i < 4 ; i++) {
      if (i < statusId) {
        block_process[i].style.backgroundColor = "#efcd9f";
        block_process[i].style.border = "1px solid #cf6439"
        text_block[i].style.color = "red"
      }
      else {
        block_process[i].style.backgroundColor = 'white'
        block_process[i].style.border = "1px solid #ccc"
        text_block[i].style.color = 'gray';
      }
    }
    let size = (progress.offsetWidth / 3) * (statusId-1);
    progressColor.style.width = size + "px";
  }
}
