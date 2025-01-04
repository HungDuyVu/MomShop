import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Collection } from 'src/models/collection';
import { CollectionService } from 'src/services/collection.service';
import { ProductConst } from 'src/shared/AppConst';
import { CreateOrEditProductCollectionComponent } from '../create-or-edit-product-collection/create-or-edit-product-collection.component';
import { ProductService } from 'src/services/product.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create-or-edit-received-order',
  templateUrl: './create-or-edit-collection.component.html',
  styleUrls: ['./create-or-edit-collection.component.scss'],
  providers: [DialogService, MessageService]
})
export class CreateOrEditCollectionComponent implements OnInit {
  baseUrl = 'http://localhost:5001';
  collection = new Collection();
  statuses = ProductConst.productStatus;
  collectionId;
  colProducts: any[] = [];
  products: any[] = [];
  selectedProduct: any[] = []
  sourceProducts: [];
  rows: any[] = [];
  targetProducts: [];
  selectedItems: any[] = [];
  productIds: any[] = [];
  keyword;
  id;
  listTypeProduct = [
    { name: "Áo thun", value: 1 },
    { name: "Áo sơ mi", value: 2 },
    { name: "Áo khoác", value: 3 },
    { name: "Quần", value: 4 },
    { name: "Phụ kiện", value: 5 },
  ];
  screenHeight: number = window.innerHeight;
  listStatus = [
    { code: 'Tất cả', value: undefined },
    { code: 'Đang bán', value: 1 },
    { code: 'Chưa mở bán', value: 2 },
    { code: 'Khoá', value: 3 },
  ];
  cols;
  timer;
  ref: DynamicDialogRef
  constructor(private http: HttpClient,
    public messageService: MessageService,
    public collectionService: CollectionService,
    public productService: ProductService,
    public toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute,
    public dialogService: DialogService,

  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const id = params['id'];
      this.id = id
      if (id) {
        this.find(id);
      } else {
        this.getProductData();
      }
    });
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

  }

  save() {
    console.log("213",this.collection);
    
    if (this.validate()) {
      this.selectedItems.forEach((item) => {
        this.productIds.push(item.id);
      });
      this.collection.products = this.productIds
      this.collectionService.createOrEdit(this.collection).subscribe((data: any) => {
        console.log("res", data);
        if (this.collection.id == undefined) {
          this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Thêm thành công', life: 3000 });
        } else {
          this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Cập nhật thành công', life: 3000 });
          this.collection.products = data.products;
        }
      })
    }
    else {
      this.messageService.add({ severity: 'warn', summary: 'Thông báo', detail: 'Vui lòng nhập đầy đủ thông tin', life: 3000 });
    }

  }
  getData(): void {
    this.collectionService.getAllCollection().subscribe((data) => {
      console.log("data", data);
      this.rows = data;
      this.genlistAction(this.rows);
      this.rows.forEach(element => {
        var statusDisplay = this.listStatus.find(e => e.value == element.status).code
        if (statusDisplay) {
          element.statusDisplay = statusDisplay
        }
        if (statusDisplay) {
          element.statusDisplay = statusDisplay
        }
      });
    });
  }
  close() {
  }
  validate(): boolean {
    console.log("product", this.collection.code);
    if (this.collection.code == null || this.collection.name == null) {
      return false;
    }
    return true;
  }

  genlistAction(data = []) {
    // this.listAction = data.map((productDetail, index) => {
    //   const actions = [];
    //     actions.push({
    //       data: productDetail,
    //       label: "Sửa",
    //       icon: "pi pi-pencil",
    //       command: ($event) => {
    //         console.log("$22222222222", productDetail);
    //         //this.editDetail(productDetail);
    //       },
    //     });
    //     //
    //     actions.push({
    //       data: productDetail,
    //       index: index,
    //       label: "Xoá",
    //       icon: "pi pi-trash",
    //       command: ($event) => {
    //         //this.detailProduct($event.item.data, $event.item.index);
    //       },
    //     });
    //   return actions;
    // });
  }

  addProductCollection() {

  }
  getProductData(): void {
    this.productService.getAllProduct().subscribe((data) => {
      console.log("data", data?.items);
      this.rows = data?.items;
      this.genlistAction(this.rows);
      this.rows.forEach(element => {
        var productTypeName = this.listTypeProduct.find(e => e.value == element.productType).name
        var productStatusName = this.listStatus.find(e => e.value == element.status).code
        if (productTypeName) {
          element.productTypeName = productTypeName
        }
        if (productStatusName) {
          element.productStatusName = productStatusName
        }
        element.imageUrl = element.imageUrl;
      });
    });
  }

  find(id): void {
    this.collectionService.getCollection(id).subscribe((data) => {
      console.log("data", data?.items);
      this.collection = data;
      this.rows = data.products;
      this.genlistAction(this.rows);
      this.rows.forEach(element => {
        var productTypeName = this.listTypeProduct.find(e => e.value == element.productType).name
        var productStatusName = this.listStatus.find(e => e.value == element.status).code
        if (productTypeName) {
          element.productTypeName = productTypeName
        }
        if (productStatusName) {
          element.productStatusName = productStatusName
        }
        element.imageUrl = element.imageUrl;
      });
    });
  }
  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.collectionService.getCollection(this.id, this.keyword).subscribe((data) => {
        console.log("data", data?.items);
        this.collection = data;
        this.rows = data.products;
        this.genlistAction(this.rows);
        this.rows.forEach(element => {
          var productTypeName = this.listTypeProduct.find(e => e.value == element.productType).name
          var productStatusName = this.listStatus.find(e => e.value == element.status).code
          if (productTypeName) {
            element.productTypeName = productTypeName
          }
          if (productStatusName) {
            element.productStatusName = productStatusName
          }
          element.imageUrl = element.imageUrl;
        });
      });
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }
  backToCollectionList() {
    this.router.navigate(['admin/collection-management/collection']);
  }

  updateProductCollection() {
    this.ref = this.dialogService.open(CreateOrEditProductCollectionComponent, {
      data: {
        collection: this.collection
      },
      header: 'Chi tiết bộ sưu tập',
      width: '70%',
      contentStyle: { "max-height": "1900px", overflow: "auto", "margin-bottom": "40px" },
      baseZIndex: 10000,
    });
    this.ref.onClose.subscribe((data) => {
      if (data) {
        this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Cập nhật thành công', life: 3000 });
        this.route.queryParams.subscribe(params => {
          const id = params['id'];
          if (id) {
            this.find(id);
            this.productIds = this.collection.products;
          }
        });
      }
    });
  }
}
