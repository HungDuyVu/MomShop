import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { OrderService } from 'src/services/order.service';
import { OrderConst, OrderStatus } from 'src/shared/AppConst';
import { ViewOrderComponent } from './view-order/view-order.component';

@Component({
  selector: 'app-app-admin-management-order',
  templateUrl: './app-admin-management-order.component.html',
  styleUrls: ['./app-admin-management-order.component.scss'],
  providers: [ConfirmationService, MessageService, DatePipe, DialogService]
})
export class AppAdminManagementOrderComponent  implements OnInit {

  listAction: any[] = [];
  rows;
  cols;
  receiveOrderDetailData;
  receiveOrderData;
  selectedRow;
  totalRecords;
  filter = true;
  filterStatus = null;
  keyword: string | undefined;
  timer: any;
  selectedOrder;
  createdDate;
  intendedTime;
  listStatus = [
    {code :'Tất cả',value:undefined},
    {code :'Khởi tạo',value:1},
    {code :'Đã nhận',value:2},
    {code :'Đang giao',value:3},
    {code :'Hoàn thành',value:4},
    {code :'Đã hủy',value:5},
  ];
  OrderConst = OrderConst;
  OrderStatus = OrderStatus;
  constructor(private http: HttpClient, 
    private orderService: OrderService,
    public toastr: ToastrService, 
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private datePipe: DatePipe,
    private dialogService: DialogService,
    ) {}

  ngOnInit(): void {
    
    this.cols = [
      {
        field: 'customerName',
        header: 'Tên khách hàng',
      },
      {
        field: 'createdDateDisplay',
        header: 'Ngày đặt hàng',
      },
      {
        field: 'intendedTimeDisplay',
        header: 'Ngày nhận (dự kiến)',
      },
      {
        field: 'totalAmount',
        header: 'Tổng giá trị',
      },
      {
        field: 'addressDelivery',
        header: 'Địa chỉ giao hàng',
      },
    ];
    this.getData();
  }

  getData(): void {
    const created = this.datePipe.transform(this.createdDate, 'yyyy-MM-dd')
    const intended = this.datePipe.transform(this.intendedTime, 'yyyy-MM-dd')

    this.orderService.getAllOrder(this.filterStatus, this.keyword, created, intended).subscribe((data) => {
      this.rows = data;
      this.genlistAction(this.rows);
      this.rows.forEach(element => {
        element.addressDelivery = element.address + ", " + element.province + ", " + element.district;
        element.createdDateDisplay = this.formatDate(element.createdDate);
        element.intendedTimeDisplay = this.formatDate(element.intendedTime);
      });
    });
  }
  onFilterChange(){
    this.filter = !this.filter;
  }
  
  formatDate(date: Date) {
    return this.datePipe.transform(date, 'dd/MM/yyyy');
  }
  genlistAction(data = []) {
    this.listAction = data.map((product, index) => {

      const actions = [];
        actions.push({
          data: product,
          label: "Chi tiết",
          icon: "pi pi-eye",
          command: ($event) => {
            console.log("$22222222222", product);
            this.editOrder($event.item.data);
          },
        });

        if (product.orderStatus == 1 || product.orderStatus == 6){
          actions.push({
            data: product,
            label: "Tiếp nhận đơn",
            icon: "pi pi-arrow-circle-down",
            command: ($event) => {
              console.log("$22222222222", $event.item.data.id);
              this.acceptOrder($event.item.data.id);
            },
          });
        }
        
        if (product.orderStatus == 2){
          actions.push({
            data: product,
            label: "Đang giao đơn",
            icon: "pi pi-truck",
            command: ($event) => {
              console.log("$22222222222", product);
              this.processOrder($event.item.data.id);
            },
          });
        }

        if (product.orderStatus == 3){
          actions.push({
            data: product,
            label: "Hoàn thành đơn",
            icon: "pi pi-check-circle",
            command: ($event) => {
              console.log("$22222222222", product);
              this.completedOrder($event.item.data.id);
            },
          });
        }
        
        if (product.orderStatus != 4){
          actions.push({
            data: product,
            label: "Hủy đơn",
            icon: "pi pi-times",
            command: ($event) => {
              console.log("$22222222222", product);
              this.cancelorder($event.item.data.id);
            },
          });
        }
        
        //
        actions.push({
          data: product,
          index: index,
          label: "Xoá",
          icon: "pi pi-trash",
          command: ($event) => {
            this.deleteOrder($event.item.data.id);
          },
        });

         actions.push({
          data: product,
          index: index,
          label: "Xem hóa đơn",
          icon: "pi pi-eye",
          command: ($event) => {
            this.viewOrder($event.item.data);
          },
        });
      return actions;
    });
  }
  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }

  startTimer1() {
    this.intendedTime = null;
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }

  startTimer2() {
    this.createdDate = null;
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }
  editOrder(data: any){
    console.log(data);
    const navigationExtras: NavigationExtras = {
      queryParams: { id: data.id }
    };
    this.router.navigate(['admin/order-management/order/detail'],navigationExtras);
  }

  acceptOrder(data: any){
   this.orderService.acceptOrder(data).subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Cập nhật thành công', life: 3000 });
      this.getData();
   })
  }

  processOrder(data: any){
    this.orderService.processOrder(data).subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Cập nhật thành công', life: 3000 });
      this.getData();
   })
  }

  completedOrder(data: any){
    this.orderService.completeOrder(data).subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Cập nhật thành công', life: 3000 });
      this.getData();
   })
  }

  cancelorder(data: any){
    this.orderService.cancelOrder(data).subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Cập nhật thành công', life: 3000 });
      this.getData();
   })
  }

  deleteOrder(id){
    this.orderService.Delete(id).subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Xóa thành công', life: 3000 });
      this.getData();
   })
  }

  viewOrder(data: any){
    console.log(data);
    
    const ref = this.dialogService.open(
			ViewOrderComponent,
			{
				header: 'Xem hóa đơn',
				width: '1000px',
				contentStyle: { "max-height": "1000px", "overflow": "auto", "margin-bottom": "60px" },
				baseZIndex: 10000,
        data: data
			}
		);
  }
}
