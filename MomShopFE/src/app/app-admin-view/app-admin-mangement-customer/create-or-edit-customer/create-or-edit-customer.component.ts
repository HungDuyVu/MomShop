import { DatePipe } from '@angular/common';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { UpdateProductDto } from 'src/models/updateProduct';
import { ProductService } from 'src/services/product.service';

@Component({
  selector: 'app-create-or-edit-customer',
  templateUrl: './create-or-edit-customer.component.html',
  styleUrls: ['./create-or-edit-customer.component.scss'],
  providers: [DatePipe]
})
export class CreateOrEditCustomerComponent implements OnInit {
  product: UpdateProductDto = new UpdateProductDto();
  saving = false;
  active;
  cities;
  name:string;
  test;
  category
  quantity;
  customer;
  constructor(public productServices: ProductService,public toastr: ToastrService,
    private datePipe: DatePipe,
    public configDialog: DynamicDialogConfig,) {
    this.cities = [
      { name: 'New York', code: 'NY' },
      { name: 'Rome', code: 'RM' },
      { name: 'London', code: 'LDN' },
      { name: 'Istanbul', code: 'IST' },
      { name: 'Paris', code: 'PRS' }
  ];
  }
  ngOnInit(): void {
    this.customer = this.configDialog?.data.customer;
    this.customer.birthDay = this.formatDate(this.customer.birthDay)
    console.log(this.customer);
  }
  formatDate(date: Date) {
    return this.datePipe.transform(date, 'dd/MM/yyyy');
  }
}
