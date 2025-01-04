import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProductDto } from 'src/models/product';
import { ProductService } from 'src/services/product.service';
import { CreateOrEditCustomerComponent } from './create-or-edit-customer/create-or-edit-customer.component';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { DatePipe } from '@angular/common';
import { CustomerService } from 'src/services/cusomter.service';
import { CustomerConst } from 'src/shared/AppConst';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import * as moment from 'moment';

@Component({
  selector: 'app-app-admin-mangement-customer',
  templateUrl: './app-admin-mangement-customer.component.html',
  styleUrls: ['./app-admin-mangement-customer.component.scss'],
  providers: [ConfirmationService, MessageService, DatePipe, DialogService,]
})
export class AppAdminMangementCustomerComponent {
  @ViewChild('createCustomer', { static: true })
  modalUser: CreateOrEditCustomerComponent;
  product: ProductDto = new ProductDto;
  //filter
  name;
  phone;
  email
  cols;
  tableData;
  filter: boolean = true;
  selectedRow;
  totalRecords;
  listStatus;
  filterStatus;
  keyword;
  date: Date;
  timer;
  listAction: any[] = [];
  ref: DynamicDialogRef;
  isLock = true;
  CustomerConst = CustomerConst;
  ngOnInit(): void { }
  constructor(private http: HttpClient,
    public customerServices: CustomerService,
    public toastr: ToastrService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private datePipe: DatePipe,
    public dialogService: DialogService,

  ) {
    this.cols = [
      // {
      //   field: 'id',
      //   header: '#Id',
      // },
      {
        field: 'fullName',
        header: 'Họ tên',
      },
      {
        field: 'email',
        header: 'Email',
      },
      {
        field: 'phone',
        header: 'SĐT',
      },
      {
        field: 'gender',
        header: 'Giới tính',
      },
      {
        field: 'birthDayDisplay',
        header: 'Ngày sinh',
      },
      {
        field: 'address',
        header: 'Họ tên',
      },
    ];
    this.getAllCustomer();
  }
  test(){
    console.log("date", this.date);
    
  }
  getAllCustomer(): void {
    const dateSearch = this.datePipe.transform(this.date, 'yyyy-MM-dd');

    this.customerServices.getAllCustomer(this.filterStatus, this.keyword, dateSearch).subscribe((data) => {
      console.log("dataCustomer", data);
      this.tableData = data;
      this.tableData.forEach(element => {
        element.birthDayDisplay = this.formatDate(element.birthDay);
      });
      this.genlistAction(this.tableData);
    });
  }
  formatDate(date: Date) {
    return this.datePipe.transform(date, 'dd/MM/yyyy');
  }
  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getAllCustomer();
    }, 1000);
  }

  onFilterChange() {
    this.filter = !this.filter;
  }
  genlistAction(data = []) {
    this.listAction = data.map((customer, index) => {
      const actions = [];
      actions.push({
        data: customer,
        label: "Xem",
        icon: "pi pi-eye",
        command: ($event) => {
          console.log("$22222222222", customer);
          this.view($event.item.data);
        },
      });

      if (customer.status == 1) {
        //
        actions.push({
          data: customer,
          index: index,
          label: "Khóa tài khoản",
          icon: "pi pi-lock",
          command: ($event) => {
            this.lock($event.item.data.id);
          },
        });
      }

      if (customer.status == 2) {
        //
        this.isLock = false;
        actions.push({
          data: customer,
          index: index,
          label: "Mở khóa",
          icon: "pi pi-lock-open",
          command: ($event) => {
            this.lock($event.item.data.id);
          },
        });
      }

      //

      actions.push({
        data: customer,
        index: index,
        label: "Xoá",
        icon: "pi pi-trash",
        command: ($event) => {
          this.delete($event.item.data.id);
        },
      });


      return actions;
    });
  }

  view(data: any) {
    this.ref = this.dialogService.open(CreateOrEditCustomerComponent, {
      data: {
        customer: data
      },
      header: 'Thông tin tài khoản',
      width: '50%',
      contentStyle: { "max-height": "1900px", overflow: "auto" },
      baseZIndex: 10000,
    });
  }
  lock(id) {
    this.customerServices.changeStatus(id).subscribe((data) => {
      if (this.isLock){
        this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Khóa tài khoản thành công', life: 3000 });
      } else {
        this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Mở khóa thành công', life: 3000 });
      }
      this.getAllCustomer();
    })
  }

  delete(id) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa?',
      header: 'Xác nhận',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.customerServices.deleteCustomer(id).subscribe((data) => {
          this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Xóa thành công', life: 3000 });
          this.getAllCustomer();
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
}
