import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CollectionService } from 'src/services/collection.service';
import { CreateOrEditCollectionComponent } from './create-or-edit-collection/create-or-edit-collection.component';
import { NavigationExtras, Router } from '@angular/router';
import { CollectionStatus } from 'src/shared/AppConst';

@Component({
  selector: 'app-admin-management-collection',
  templateUrl: './admin-management-collection.component.html',
  styleUrls: ['./admin-management-collection.component.scss'],
  providers: [ConfirmationService, MessageService]
})
export class AdminManagementCollectionComponent implements OnInit {
  ref: DynamicDialogRef;
  rows: any[] = [];
  cols;
  listAction: any[] = [];
  filter = true;
  filterStatus = null;
  tableData: any;
  metaKeySelection: boolean = true;
  selectedCollection;
  screenHeight: number = window.innerHeight;
  keyword;
  timer: any;
  CollectionStatus = CollectionStatus;
  listStatus = [
    {code :'Tất cả',value:undefined},
    {code :'Đang bán',value:1},
    {code :'Chưa mở bán',value:2},
    {code :'Khoá',value:3},
  ];
  constructor(private http: HttpClient,
    public collectionService : CollectionService,
    public toastr: ToastrService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private router: Router,
    ) {}
  ngOnInit(): void {
    this.cols = [
      {
        field: 'id',
        header: '#ID',
      },
      {
        field: 'code',
        header: 'Mã bộ sưu tập',
      },
      {
        field: 'name',
        header: 'Tên bộ sưu tập',
      },
    ];
    this.getData();
  }

  getTableHeight(percent = 65) {
    return (this.screenHeight*(percent/100) + 'px');     
  }

  onFilterChange(){
    this.filter = !this.filter;
  }
  editProduct(row: any) {
    const navigationExtras: NavigationExtras = {
      queryParams: { id: row.id }
    };
    this.router.navigate(['admin/collection-management/collection/detail'],navigationExtras);
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
            this.deleteCollection($event.item.data);
          },
        });
      return actions;
    });
  }
  getData(): void {
    this.collectionService.getAllCollection(this.filterStatus, this.keyword).subscribe((data) => {
      console.log("data", data);
      this.rows = data;
      this.genlistAction(this.rows);
      console.log(this.tableData);
    });
  }
  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getData();
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }
  createCollection(){
    this.router.navigate(['admin/collection-management/collection/create']);
  }

  deleteCollection(row) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa?',
      header: 'Xác nhận',
      icon: 'pi pi-info-circle',
      accept: () => {
          this.collectionService.deleteCollection(row.id).subscribe((data)=>{
          this.collectionService.getAllCollection().subscribe(()=>{
            this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Xóa thành công', life: 3000 });
            this.getData();
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
}
