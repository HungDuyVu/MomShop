import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Collection } from 'src/models/collection';
import { CollectionService } from 'src/services/collection.service';
import { ProductService } from 'src/services/product.service';

@Component({
  selector: 'app-create-or-edit-product-collection',
  templateUrl: './create-or-edit-product-collection.component.html',
  styleUrls: ['./create-or-edit-product-collection.component.scss']
})
export class CreateOrEditProductCollectionComponent implements OnInit {
  baseUrl = 'http://localhost:5001';
  rows: any[] = [];
  cols: any[] = [];
  listTypeProduct = [
    { name: "Áo thun", value: 1 },
    { name: "Áo sơ mi", value: 2 },
    { name: "Áo khoác", value: 3 },
    { name: "Quần", value: 4 },
    { name: "Phụ kiện", value: 5 },
  ];
  screenHeight: number = window.innerHeight;
  listStatus = [
    {code :'Tất cả',value:undefined},
    {code :'Đang bán',value:1},
    {code :'Chưa mở bán',value:2},
    {code :'Khoá',value:3},
  ];
  keyword;
  timer;
  filter;
  productIds: any[] = [];
  selectedItems: any[] = [];
  collection = new Collection();
  constructor(private http: HttpClient,
    public collectionService : CollectionService,
    public toastr: ToastrService,
    public dialogService: DialogService, 
    public messageService: MessageService,
    public productService: ProductService,
    private router: Router,
    public ref: DynamicDialogRef,
    public configDialog: DynamicDialogConfig,
    ) {}
  ngOnInit(): void {
    this.cols = [
      {
        field: 'code',
        header: 'Mã sản phẩm',
        width: '10rem'
      },
      {
        field: 'name',
        header: 'Tên sản phẩm',
        width: '25rem'
      },
      {
        field: 'productTypeName',
        header: 'Loại',
        width: '10rem'
      },
      {
        field: 'price',
        header: 'Giá bán',
        width: '10rem'
      },
      {
        field: 'productStatusName',
        header: 'Trạng thái',
        width: '10rem'
      },
    ];
    this.getProductData();
    this.collection = this.configDialog?.data.collection;
  }

  close() {
    this.ref.close(false);
  }

  getProductData(): void {
    this.productService.getAllProduct(this.filter,this.keyword).subscribe((data) => {
      console.log("data", data?.items);
      this.rows = data?.items;
      //this.genlistAction(this.rows);
      this.rows.forEach(element => {
        var productTypeName = this.listTypeProduct.find( e=> e.value == element.productType).name
        var productStatusName = this.listStatus.find( e=> e.value == element.status).code
        if(productTypeName)
        {
          element.productTypeName = productTypeName
        }
        if(productStatusName){
          element.productStatusName = productStatusName
        }
        element.imageUrl = element.imageUrl;
      });
    });
  }
  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getProductData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }
  save(){
    if(this.validate()){
      this.selectedItems.forEach((item) => {
        this.productIds.push(item.id);
      });
      this.collection.products = this.productIds
      console.log("collection", this.collection);
      this.collectionService.createOrEdit(this.collection).subscribe(() => {
        this.ref.close(true);
      });
    }
    else {
      // this.toastr.warning(
      //   "Vui lòng chọn sản phẩm",
      //   "Thông báo",
      //   { timeOut: 3000 }
      // );
      this.messageService.add({ severity: 'warn', summary: 'Thông báo', detail: 'Vui lòng chọn sản phẩm', life: 3000 });
    }
  }
  validate(): boolean{
    if (this.selectedItems.length == 0)
    {
      return false;
    }
    return true;
  }
}
