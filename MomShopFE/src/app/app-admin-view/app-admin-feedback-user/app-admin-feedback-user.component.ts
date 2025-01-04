import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProductDto } from 'src/models/product';
import { ProductService } from 'src/services/product.service';
import { CreateOrEditCustomerComponent } from '../app-admin-mangement-customer/create-or-edit-customer/create-or-edit-customer.component';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { FeedbackService } from 'src/services/feedback.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-app-admin-feedback-user',
  templateUrl: './app-admin-feedback-user.component.html',
  styleUrls: ['./app-admin-feedback-user.component.scss'],
  providers: [ConfirmationService, MessageService,DatePipe]
})
export class AppAdminFeedbackUserComponent implements OnInit {
  @ViewChild('createCustomer', { static: true })
  modalUser: CreateOrEditCustomerComponent;
  //filter
  orderCode;
  pointEvaluate;
  selectedCity;
  listPoint;
  //---
  cols;
  tableData;
  filter: boolean = true
  selectedRow;
  totalRecords;
  filterStatus;
  keyword;
  listAction: any[] = [];
  timer;
  rating;

  ngOnInit(): void {}
  constructor(private http: HttpClient,public feedbackServices : FeedbackService,public toastr: ToastrService,private datePipe: DatePipe,
    private router: Router,
    private route: ActivatedRoute,) {
    this.cols = [
      {
        field: 'orderCode',
        header: 'Mã đơn hàng',
      },
      {
        field: 'customerName',
        header: 'Tên khách hàng',
      },
      {
        field: 'content',
        header: 'Phản hồi',
      },
      {
        field: 'email',
        header: 'Email',
      },
      {
        field: 'createdDateDisplay',
        header: 'Ngày tạo',
      },
    ];
    this.listPoint = [
      {name: '1 sao ', code: '1'},
      {name: '2 sao', code: '2'},
      {name: '3 sap', code: '3'},
      {name: '4 sao', code: '4'},
      {name: '5 sao', code: '5'}
  ];
    this.getAllFeedback();
  }

  backToMenu(){
    this.router.navigate(['admin/feedback']);
  }
  formatDate(date: Date) {
    return this.datePipe.transform(date, 'dd/MM/yyyy');
  }
  getAllFeedback(): void {
    this.feedbackServices.getAllfeedback(this.rating, this.keyword).subscribe((data)=>{
      this.tableData = data;
      this.totalRecords = this.tableData
      this.totalRecords.forEach(element => {
        element.createdDateDisplay = this.formatDate(element.createdDate);
      });
      this.genlistAction(this.totalRecords)
    });
  }
  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getAllFeedback();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }

  onSelectionChange(event) {}
  // createUsers() {
  //   this.modalUser.show();
  // }
  // editUser() {
  //   this.modalUser.show(this.selectedRow.id);
  // }
  deleteUser() {
    // this.feedbackServices.deleteProduct(this.selectedRow.id).subscribe((data)=>{
    //   this.producfeedbackServicestServices.getAllProduct().subscribe(()=>{
    //     this.toastr.success('Xoá thành công','Thông báo',{
    //       timeOut:100
    //     });
    //     this.getAllCustomer();
    //   })
    // });
  }
  onFilterChange(){
    this.filter = !this.filter;
  }

  genlistAction(data = []) {
    this.listAction = data.map((receivedOrder, index) => {
      const actions = [];
        // actions.push({
        //   data: receivedOrder,
        //   label: "Sửa",
        //   icon: "pi pi-pencil",
        //   command: ($event) => {
        //     console.log("$22222222222", receivedOrder);
        //     //this.editReceiveOrder($event.item.data);
        //   },
        // });

        //
        actions.push({
          data: receivedOrder,
          index: index,
          label: "Phản hồi",
          icon: "pi pi-wallet",
          command: ($event) => {
            //this.paymentOrder($event.item.data);
          },
        });
        //

        actions.push({
          data: receivedOrder,
          index: index,
          label: "Xoá",
          icon: "pi pi-trash",
          command: ($event) => {
            //this.deleteReceiveOrder($event.item.data);
          },
        });
      return actions;
    });
  }
}
