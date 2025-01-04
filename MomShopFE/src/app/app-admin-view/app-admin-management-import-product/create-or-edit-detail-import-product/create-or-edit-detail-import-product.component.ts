import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ReceivedOrderDetail } from 'src/models/receivedOrderDetail';
import { UpdateProductDto } from 'src/models/updateProduct';
import { ProductService } from 'src/services/product.service';
import { ReceiveOrderService } from 'src/services/receiveOrder.service';

@Component({
  selector: 'app-create-or-edit-detail-import-product',
  templateUrl: './create-or-edit-detail-import-product.component.html',
  styleUrls: ['./create-or-edit-detail-import-product.component.scss']
})
export class CreateOrEditDetailImportProductComponent implements OnInit {
  //detail: ReceivedOrderDetail = new ReceivedOrderDetail();
  detail: ReceivedOrderDetail = new ReceivedOrderDetail();
  saving = false;
  active;
  cities;
  name: string;
  test;
  listStatus;
  listTypeProduct;
  category;
  quantity;
  constructor(
    public dialogService: DialogService, 
    public messageService: MessageService,  
    public configDialog: DynamicDialogConfig,
    public receivedOrderService: ReceiveOrderService,
    public toastr: ToastrService,
    public ref: DynamicDialogRef,
  ) {
    
  }
  ngOnInit(): void {
    this.listTypeProduct = [
      { name: "Áo thun", value: 1 },
      { name: "Áo sơ mi", value: 2 },
      { name: "Áo khoác", value: 3 },
      { name: "Quần", value: 4 },
      { name: "Phụ kiện", value: 5 },
    ];
    this.getData();
  }
  show(id?) {
    if (id) {
    }
    //this.modal.show();
    this.active = true;
  }
  close() {
    this.ref.close();
  }
  save() {
    console.log("detail", this.detail);
    this.receivedOrderService.createOrEditDetailReceiveOrder(this.detail).subscribe(() => {
      this.ref.close(true);
    });
  }

  getData(){
    if(this.configDialog?.data.receivedOrder) {
      this.receivedOrderService.getDetai(this.configDialog?.data.receivedOrder.id).subscribe((data: ReceivedOrderDetail) => {
        this.detail = data;
      });
    }
  }
}
