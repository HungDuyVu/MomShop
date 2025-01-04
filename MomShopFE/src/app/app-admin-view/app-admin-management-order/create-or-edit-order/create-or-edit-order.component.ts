import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Order } from 'src/models/order';
import { OrderService } from 'src/services/order.service';
import { OrderConst, PaymentType } from 'src/shared/AppConst';

@Component({
  selector: 'app-create-or-edit-order',
  templateUrl: './create-or-edit-order.component.html',
  styleUrls: ['./create-or-edit-order.component.scss'],
  providers: [DialogService, ConfirmationService, MessageService, DatePipe]
})
export class CreateOrEditOrderComponent implements OnInit {
  baseUrl = 'http://localhost:5001';
  order: Order = new Order();
  statuses = OrderConst.orderStatus;
  addressDisplay: string;
  isEdit: boolean = false;
  delivery: string = "VIETTEL POST"
  cols;
  paymentTypeDisplay;
  events: any[];
  PaymentType = PaymentType;
  constructor(
    public messageService: MessageService,  
    public toastr: ToastrService,
    public orderService: OrderService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private route: ActivatedRoute,
    public dialogService: DialogService, 
    private datePipe: DatePipe
  ) {}
  
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const id = params['id'];
      if (id){
        this.show(id);
      } else {
        console.log("id", id);
        this.getData();
      }
    });
    this.cols = [
      {
        field: 'productCode',
        header: 'Mã sản phẩm',
        width: '10rem'
      },
      {
        field: 'productName',
        header: 'Tên sản phẩm',
        width: '25rem'
      },
      {
        field: 'size',
        header: 'Size',
        width: '10rem'
      },
      {
        field: 'quantity',
        header: 'Số lượng',
        width: '10rem'
      },
      {
        field: 'price',
        header: 'Đơn giá',
        width: '10rem'
      },
      {
        field: 'priceDisplay',
        header: 'Thành tiền',
        width: '10rem'
      },
    ];
  }


  backToCollectionList(){
    this.router.navigate(['admin/order-management/order']);
  }
  getData(){

  }

   show(id?) {
    if (id) {
      this.orderService.get(id).subscribe((data: Order) => {
        console.log("data", data);
        this.order = data;
        this.order.orderDetails = data.orderDetails;
        this.events = data.events;
        this.addressDisplay = data.address + ", " + data.province + ", " + data.district;
        const dateObj = new Date(this.order.createdDate); 
        dateObj.setDate(dateObj.getDate() + 1);
        this.order.createdDate = dateObj;
        const dateObj1 = new Date(this.order.intendedTime); 
        dateObj1.setDate(dateObj.getDate() + 1);
        this.order.intendedTime = dateObj1;
        this.order.orderDetails.forEach(element => {
          element.priceDisplay = element.price * element.quantity;
        });
        this.events.forEach(element => {
          element.createdDate = this.datePipe.transform(element.createdDate, 'dd/MM/yyyy hh:mm');
        });
        this.paymentTypeDisplay = PaymentType.getPaymentType(this.order.paymentType);
      })
    }
  }
  save(){

  }
}
