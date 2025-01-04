import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { UserProductService } from 'src/services/user-product.service';
import { UserService } from 'src/services/user.service';
import { ProductStatus } from 'src/shared/AppConst';

@Component({
  selector: 'app-user-collection',
  templateUrl: './user-collection.component.html',
  styleUrls: ['./user-collection.component.scss']
})
export class UserCollectionComponent {
  baseUrl = 'http://localhost:5001';
  layout: string = 'list';
  listCollection;
  products: any[];
  productsPagination: any[];

  first: number = 0;

  rows: number = 12;
  total: number;
  page = 0;
  keyword;
  timer: any;
  param;
  ProductStatus = ProductStatus;
  constructor(private router: Router,
    private http: HttpClient,
    public productServices : UserProductService,
    public userService : UserService
    ) {
      this.userService.getAllCollection().subscribe((result:any)=>{
        this.listCollection = result.data;
      })
    }

  ngOnInit() {
    this.getProduct('all');
  }

  onPageChange(event) {
    console.log(event);
    this.first = event.first;
    this.rows = event.rows;
    this.page = event.page;
    this.productsPagination = this.products.slice(this.first, (this.page + 1) * this.rows);
  }

  showDetail(param){
    this.router.navigate(['/collection-detail'], {queryParams: {id: param.id}});  
  }

  getProduct(param){
    console.log("k", this.keyword);
    
    this.param = param;
    this.productServices.getShirt(param, this.keyword).pipe(finalize(() => {
      this.resetPage();
    })).subscribe((data) => {
      this.products = data;
      this.total = this.products.length;
    });
  }
  startTimer() {
    clearTimeout(this.timer); // Đảm bảo rằng timer trước đó được hủy
    this.timer = setTimeout(() => {
      this.getProduct(this.param);
    }, 1000); // Thời gian chờ: 3000 milliseconds (3 giây)
  }
  getDetailCollection(id){
    console.log("123142", id);
      this.userService.getDetailCollection(id).subscribe((result:any)=>{
        console.log(result.data);
        this.productsPagination = result.data.products;
      })
  }

  resetPage(){
    this.first = 0;
    this.page = 0;
    this.productsPagination = this.products.slice(this.first, (this.page + 1) * this.rows);
  }

}