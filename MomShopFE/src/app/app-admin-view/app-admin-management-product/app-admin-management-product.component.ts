import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProductDto } from 'src/models/product';
import { ProductService } from 'src/services/product.service';
import { ConfirmEventType, ConfirmationService, MenuItem, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CreateOrEditProductTestComponent } from './create-or-edit-product-test/create-or-edit-product-test.component';
import { ProductStatus } from 'src/shared/AppConst';
import { CommonModule } from "@angular/common";
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-app-admin-management-product',
  templateUrl: './app-admin-management-product.component.html',
  styleUrls: ['./app-admin-management-product.component.scss'],
  providers: [DialogService,ConfirmationService, MessageService]
})
export class AppAdminManagementProductComponent{
  baseUrl = 'http://localhost:5001';
  ref: DynamicDialogRef;
  rows: ProductDto[] = [];
  rowsAfter: ProductDto[] = [];
  timer: any;
  product: ProductDto;
  colsProduct;
  colsDetailProduct;
  filter = true;
  filterStatus = null;
  selectedProduct;
  selectedDetailProduct
  totalRecords;
  status = undefined;
  detailProduct :any;
  menuItems: MenuItem[] = [];
  listAction: any[] = [];
  screenHeight: number = window.innerHeight;
  metaKeySelection: boolean = true;
  ProductStatus = ProductStatus;
  keyword;
  isNew = true;
  listTypeProduct = [
    { name: "Áo thun", value: 1 },
    { name: "Áo sơ mi", value: 2 },
    { name: "Áo khoác", value: 3 },
    { name: "Quần", value: 4 },
    { name: "Phụ kiện", value: 5 },
  ];
  listStatus = [
    {code :'Tất cả',value:undefined},
    {code :'Đang bán',value:1},
    {code :'Chưa mở bán',value:2},
    {code :'Khoá',value:3},
    {code :'Hết hàng',value:4},
  ]
  ngOnInit(): void {}
  constructor(private http: HttpClient,
    public productServices : ProductService,
    public toastr: ToastrService,
    public dialogService: DialogService, 
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private router: Router,
    ) {
    this.colsProduct = [
      {
        field: 'code',
        header: 'Mã sản phẩm',
        width: '15rem'
      },
      {
        field: 'name',
        header: 'Tên sản phẩm',
        width: '25rem'
      },
      // {
      //   field: 'productTypeName',
      //   header: 'Loại',
      //   width: '10rem'
      // },
      {
        field: 'price',
        header: 'Giá bán',
        width: '10rem'
      },
    ];
    this.colsDetailProduct = [
      {
        field: 'size',
        header: 'Kích thước',
      },
      {
        field: 'quantity',
        header: 'Số lượng',
      },
      {
        field: 'description',
        header: 'Mô tả',
      },
    ];
    this.getProductData();
  }
  getProductData(): void {
    this.productServices.getAllProduct(this.filterStatus, this.keyword).subscribe((data) => {
      console.log("data", data?.items);
      this.rows = data?.items;
      this.genlistAction(this.rows);
      this.rows.forEach(element => {
        var productTypeName = this.listTypeProduct.find( e=> e.value == element.productType).name
        var productStatusName = this.listStatus.find( e=> e.value == element.status).code
        if(productTypeName)
        {
          element.productTypeName = productTypeName
        }
        element.imageUrl = element.imageUrl;
      });
    });
  }
  getDetailProductData():void {
    this.productServices.getAllViewDetailProduct(this.selectedProduct.id).subscribe((data)=>{
      this.detailProduct = data;
    })
  }

  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getProductData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }

  onSelectionChange() {
   this.getDetailProductData();
  }
  createProduct() {
    this.router.navigate(['admin/product-management/product/create']);
  }
  editProduct(row) {
    const navigationExtras: NavigationExtras = {
      queryParams: { id: row.id }
    };
    this.router.navigate(['admin/product-management/product/detail'],navigationExtras);
  }
  deleteProduct(row) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa?',
      header: 'Xác nhận',
      icon: 'pi pi-info-circle',
      accept: () => {
          this.productServices.deleteProduct(row.id).subscribe((data)=>{
          this.productServices.getAllProduct().subscribe(()=>{
            this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Xóa thành công', life: 3000 });
            this.getProductData();
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
  viewDetailProduct(data){
    console.log("abc",data);
    //this.viewDetail.show(data.id);
  }
  onFilterChange(){
    this.filter = !this.filter;
  }
  deleteDetailProduct(row){
    this.productServices.deleteDetailProduct(row.id).subscribe(()=>{
      this.toastr.success('Xoá thành công','Thông báo',{timeOut: 3000});
      this.productServices.getAllProduct().subscribe(()=>{
        this.getDetailProductData();
      })
    });
  }
  getTableHeight(percent = 65) {
    return (this.screenHeight*(percent/100) + 'px');     
  }

  genlistAction(data = []) {
    this.listAction = data.map((product, index) => {
      const actions = [];
        actions.push({
          data: product,
          label: "Sửa",
          icon: "pi pi-pencil",
          command: ($event) => {
            console.log("$22222222222", product);
            this.editProduct($event.item.data);
          },
        });
        //
        actions.push({
          data: product,
          index: index,
          label: "Xoá",
          icon: "pi pi-trash",
          command: ($event) => {
            this.deleteProduct($event.item.data);
          },
        });
      return actions;
    });
  }
}
