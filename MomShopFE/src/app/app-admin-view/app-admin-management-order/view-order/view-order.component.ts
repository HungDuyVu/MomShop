import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { format } from 'date-fns';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Order } from 'src/models/order';
import { OrderService } from 'src/services/order.service';

@Component({
  selector: 'app-view-order',
  templateUrl: './view-order.component.html',
  styleUrls: ['./view-order.component.scss'],
  providers: [DialogService, ConfirmationService, MessageService, DatePipe]

})
export class ViewOrderComponent implements OnInit {
  tableData: any[] = [];
  orderCode;
  order;
  orderDetails;
  events;
  addressDelivery;
  addressDisplay;
  constructor(public ref: DynamicDialogRef,
    public configDialog: DynamicDialogConfig,
    private dialogService: DialogService,
    public orderService: OrderService,
    private datePipe: DatePipe
    ) { }

  ngOnInit() {
    console.log("1",this.configDialog.data);
    this.show(this.configDialog.data.id);
  }


  show(id){
    this.orderService.view(id).subscribe((data: Order) => {
      console.log("data", data);
      this.order = data;
      this.orderDetails = data.orderDetails;
      this.events = data.events;
      this.addressDisplay = data.address + ", " + data.province + ", " + data.district;
      const formattedDateTime = format(new Date(this.order.createdDate), 'dd/MM/yyyy HH:mm:ss');

      this.order.createdDate = formattedDateTime;
      const dateObj1 = new Date(this.order.intendedTime); 
     
      this.order.intendedTime = dateObj1;
      this.order.orderDetails.forEach(element => {
        element.priceDisplay = element.price * element.quantity;
      });
    })
  }
}
