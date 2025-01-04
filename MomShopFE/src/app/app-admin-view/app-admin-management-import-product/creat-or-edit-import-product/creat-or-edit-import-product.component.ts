import { Component, EventEmitter, OnInit, Output, ViewChild } from "@angular/core";
import { ModalDirective } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";
import { ConfirmEventType, ConfirmationService, MessageService } from "primeng/api";
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from "primeng/dynamicdialog";
import { ReceiveOrderDto } from "src/models/receiverOrder";
import { ReceiveOrderService } from "src/services/receiveOrder.service";
import { ProductConst, ProductStatus, ReceivedOrderConst } from "src/shared/AppConst";
import { CreateOrEditDetailImportProductComponent } from "../create-or-edit-detail-import-product/create-or-edit-detail-import-product.component";
import { ReceivedOrderDetail } from "src/models/receivedOrderDetail";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-creat-or-edit-import-product",
  templateUrl: "./creat-or-edit-import-product.component.html",
  styleUrls: ["./creat-or-edit-import-product.component.scss"],
  providers: [DialogService, ConfirmationService, MessageService]
})
export class CreatOrEditImportProductComponent implements OnInit {
  receiveOrder: ReceiveOrderDto = new ReceiveOrderDto();
  saving = false;
  active;
  cities;
  name: string;
  test;
  listStatus = [
    {name :'Chưa thanh toán',code:1},
    {name :'Đã thanh toán',code:2},
    {name :'Hoàn thành',code:3},
  ];
  listTypeProduct;
  category;
  quantity;
  selectedRow;
  colDetails;
  listAction: any[] = [];
  totalMoney;
  receivedOrderDetails: any = [];
  ProductStatus = ProductStatus;
  ref: DynamicDialogRef
  types = ProductConst.productType;
  id;
  
  constructor(
    public messageService: MessageService,  
    public receivedOrderService: ReceiveOrderService,
    public toastr: ToastrService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private route: ActivatedRoute,
    public dialogService: DialogService, 
  ) {}
  ngOnInit(): void {
    this.listTypeProduct = [
      { name: "Áo thun", value: 1 },
      { name: "Áo sơ mi", value: 2 },
      { name: "Áo khoác", value: 3 },
      { name: "Quần", value: 4 },
      { name: "Phụ kiện", value: 5 },
    ];

    this.route.queryParams.subscribe(params => {
      const id = params['id'];
      this.id = id;
      if (id){
        console.log("123");
        
        this.show(id);
      } else {
        this.getData();
      }
    });
    
    
    this.receivedOrderDetails.forEach(element => {
      this.totalMoney += element.quantity * element.unitPrice;
    });
    console.log("total", this.totalMoney);
    
  }

  getData(){
    //   this.receivedOrderService.getReceiveOrderById(this.configDialog?.data.receivedOrder.id).subscribe((data) => {
    //     console.log("edit", data);
    //     this.receiveOrder = data;
    //     this.receiveOrder.createdDate = this.configDialog?.data.receivedOrder.createdDateDisplay;
    //     this.receiveOrder.receivedDate = this.configDialog?.data.receivedOrder.receivedDateDisplay;
    //     if(this.receiveOrder.details){
    //       this.showData(this.receiveOrder.details)
    //       this.genlistAction(this.receiveOrder.details);
    //     }
    //   }); 
  }

  showData(rows){
    for(let row of rows){
        row.typeDisplay = ProductStatus.getProductType(row.type)
    }
  }
  show(id?) {
    if (id) {
      this.receivedOrderService.getReceiveOrderById(id).subscribe((data) => {
        this.receiveOrder = data;
        const dateObj = new Date(this.receiveOrder.receivedDate); 
        dateObj.setDate(dateObj.getDate() + 1);
        this.receiveOrder.receivedDate = dateObj;
        this.receivedOrderDetails = data.details;
      })
    }
  }
  close() {
  }

  save() {
    let check = false;
    this.receivedOrderDetails.forEach(element => {
      console.log("ele", element);
      if(element.size == undefined || element.quantity == undefined || element.code == undefined || element.name == undefined){
        check = true;
      }
    });
    console.log("1",this.validate());
    
    if(this.validate() && !check){
      let receivedDate = this.receiveOrder.receivedDate.getDate() + 1;
      this.receiveOrder.receivedDate.setDate(receivedDate);
      this.receiveOrder.details = this.receivedOrderDetails;
      this.receivedOrderService.createOrEditReceiveOrder(this.receiveOrder).subscribe(() => {
        console.log(this.receiveOrder.id);
        if (this.receiveOrder.id == undefined){
          this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Thêm mới thành công', life: 3000 });
        } else {
          this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Cập nhật thành công', life: 3000 });
        }
      });
    }
    else {
      this.messageService.add({ severity: 'error', summary: 'Thông báo', detail: 'Vui lòng nhập đầy đủ thông tin', life: 3000 });
    }
  }

  validate(): boolean{
    console.log("2",this.receiveOrder);
    if(this.receiveOrder.supplier == null || this.receiveOrder.status == null || this.receiveOrder.receiver == null){
      return false;
    }
    return true;
  }

  genlistAction(data = []) {
    this.listAction = data.map((detail, index) => {
      const actions = [];
        actions.push({
          data: detail,
          label: "Sửa",
          icon: "pi pi-pencil",
          command: ($event) => {
            this.editDetail($event.item.data);
          },
        });
        //
        actions.push({
          data: detail,
          index: index,
          label: "Xoá",
          icon: "pi pi-trash",
          command: ($event) => {
            this.deleteDetail($event.item.data);
          },
        });
      return actions;
    });
  }

  backToCollectionList(){
    this.router.navigate(['admin/received-order/order']);
  }
  addvalue() {
    let check = false;
    if (this.receivedOrderDetails.length == 0) {
      this.receivedOrderDetails.push({});
    }
    else {
      if (this.receivedOrderDetails.length > 0) {
        this.receivedOrderDetails.forEach(element => {
          console.log("ele", element);
          if(element.size == undefined || element.quantity == undefined){
            check = true;
          }
        });
      };
      if (!check) {
        this.receivedOrderDetails.push({});
      }
      else {
        this.messageService.add({ severity: 'warn', summary: 'Thông báo', detail: 'Vui lòng nhập đầy đủ thông tin', life: 3000 });
      }
    }
    
  }
  removeDetail(index){
    this.confirmationService.confirm({
      message: 'Xóa giá trị này?',
      acceptLabel: 'Đồng ý',
      rejectLabel: 'Hủy',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.receivedOrderDetails.splice(index, 1);
      }
    });
  }
  createDetail(){
    this.ref = this.dialogService.open(CreateOrEditDetailImportProductComponent, {
      data: {
        receivedOrderId: this.receiveOrder.id
      },
      header: 'Thêm mới',
      width: '70%',
      height: '70%',
      contentStyle: { "max-height": "1900px", overflow: "auto", "margin-bottom": "40px" },
      baseZIndex: 10000,
    });
    this.ref.onClose.subscribe((data) => {
      if (data) {
        this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Thêm thành công', life: 3000 });
        this.getData();
      }
    });
  }

  editDetail(row){
  }

  deleteDetail(row){
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa?',
      header: 'Xác nhận',
      icon: 'pi pi-info-circle',
      accept: () => {
          this.receivedOrderService.deleteDetailReceiveOrder(row.id).subscribe((data)=>{
            this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Xóa thành công', life: 3000 });
            this.getData();
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
