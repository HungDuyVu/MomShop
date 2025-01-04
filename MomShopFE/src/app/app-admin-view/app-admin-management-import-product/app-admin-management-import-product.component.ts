import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProductDto } from 'src/models/product';
import { CreatOrEditImportProductComponent } from './creat-or-edit-import-product/creat-or-edit-import-product.component';
import { ReceiveOrderService } from 'src/services/receiveOrder.service';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { DatePipe } from '@angular/common';
import { ReceivedOrderConst } from 'src/shared/AppConst';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-app-admin-management-import-product',
  templateUrl: './app-admin-management-import-product.component.html',
  styleUrls: ['./app-admin-management-import-product.component.scss'],
  providers: [ConfirmationService, MessageService, DatePipe]
})
export class AppAdminManagementImportProductComponent implements OnInit{
  ref: DynamicDialogRef;
  product: ProductDto = new ProductDto;
  listAction: any[] = [];
  cols;
  receiveOrderDetailData;
  receiveOrderData;
  selectedRow;
  totalRecords;
  filter = true;
  filterStatus = null;
  keyword: string | undefined;
  timer: any;
  listStatus = ReceivedOrderConst.receiveStatus;
  createdDate;
  intendedTime

  ReceivedOrderConst = ReceivedOrderConst;
  constructor(private http: HttpClient, 
    public receiveOrder: ReceiveOrderService, 
    public toastr: ToastrService, 
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private datePipe: DatePipe,
    private router: Router,
    ) {}
  ngOnInit(): void {
    this.cols = [
      {
        field: 'code',
        header: 'Mã hóa đơn',
      },
      {
        field: 'createdDateDisplay',
        header: 'Ngày tạo đơn hàng',
      },
      {
        field: 'receivedDateDisplay',
        header: 'Ngày nhận đơn hàng',
      },
      {
        field: 'supplier',
        header: 'Tên nhà cung cấp',
      },
      {
        field: 'receiver',
        header: 'Tên người nhận',
      },
    ];
    this.getReceiveOrderData();
  }

  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getReceiveOrderData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }

  getReceiveOrderData(): void {
    const created = this.datePipe.transform(this.createdDate, 'yyyy-MM-dd')
    const intended = this.datePipe.transform(this.intendedTime, 'yyyy-MM-dd')
    this.receiveOrder.getAllReceiveOrder(this.filterStatus, this.keyword, created, intended).subscribe((data) => {
      this.receiveOrderData = data;
      this.genlistAction(this.receiveOrderData);
      this.receiveOrderData.forEach(element => {
        element.createdDateDisplay = this.formatDate(element.createdDate);
        element.receivedDateDisplay = this.formatDate(element.receivedDate);
        element.statusDisplay = ReceivedOrderConst.getStatus(element.status);
      });
    });
  }
  getReceiveOrderDetailData(): void {
    this.receiveOrder.getDetailReceiveOrder(this.selectedRow.id).subscribe((data) => {
      this.receiveOrderDetailData = data;
    });
  }
  formatDate(date: Date) {
    return this.datePipe.transform(date, 'dd/MM/yyyy');
  }
  onSelectionChange(event) {
    this.getReceiveOrderDetailData();
  }
  createReceiveOrder() {
    this.router.navigate(['admin/received-order/order/create']);
  }
  startTimer1() {
    this.intendedTime = null;
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getReceiveOrderData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }

  startTimer2() {
    this.createdDate = null;
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getReceiveOrderData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }
  paymentOrder(row){
    console.log(row);
    this.receiveOrder.paymentOrder(row.id).subscribe( (res) => {
      this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Cập nhật thành công', life: 3000 });
      this.getReceiveOrderData();
    });
  }
  editReceiveOrder(row) {
    const navigationExtras: NavigationExtras = {
      queryParams: { id: row.id }
    };
    this.router.navigate(['admin/received-order/order/detail'],navigationExtras);
  }
  deleteReceiveOrder(row) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa?',
      header: 'Xác nhận',
      icon: 'pi pi-info-circle',
      accept: () => {
          this.receiveOrder.deleteReceiveOrder(row.id).subscribe((data)=>{
          this.receiveOrder.getAllReceiveOrder().subscribe(()=>{
            this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Xóa thành công', life: 3000 });
            this.getReceiveOrderData();
          })
    });
      },
      reject: (type) => {
          switch (type) {
              case ConfirmEventType.REJECT:
                  break;
              case ConfirmEventType.CANCEL:
                  break;
          }
      }
    });
  }
  //add Detail ReceiveOrder
  addDetailReceiveOrder() {
    //this.modalDetailProductImport.show(this.selectedRow.id)
  }
  genlistAction(data = []) {
    this.listAction = data.map((receivedOrder, index) => {
      const actions = [];
        actions.push({
          data: receivedOrder,
          label: "Sửa",
          icon: "pi pi-pencil",
          command: ($event) => {
            console.log("$22222222222", receivedOrder);
            this.editReceiveOrder($event.item.data);
          },
        });

        //
        actions.push({
          data: receivedOrder,
          index: index,
          label: "Đã Thanh Toán",
          icon: "pi pi-wallet",
          command: ($event) => {
            this.paymentOrder($event.item.data);
          },
        });
        //

        actions.push({
          data: receivedOrder,
          index: index,
          label: "Xoá",
          icon: "pi pi-trash",
          command: ($event) => {
            this.deleteReceiveOrder($event.item.data);
          },
        });

        
      return actions;
    });
  }
  onFilterChange(){
    this.filter = !this.filter;
  }
}
