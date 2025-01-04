import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserCartService } from 'src/services/cartService.service';
import { ProductService } from 'src/services/product.service';

@Component({
  selector: 'app-user-collection-detail',
  templateUrl: './user-collection-detail.component.html',
  styleUrls: ['./user-collection-detail.component.scss']
})
export class UserCollectionDetailComponent {
  baseUrl = 'http://localhost:5001';
  visible = false;

  product:any;
  value: string = '';
    
    productSize: any[] = [
        { size: 'XS', value: 'XS' },
        { size: 'S', value: 'S' },
        { size: 'M', value: 'M' },
        { size: 'L', value: 'L' },
        { size: 'XL', value: 'XL' },
    ];

    constructor(
      private route: ActivatedRoute,
      private router: Router,
      private productService: ProductService,
      private cartService: UserCartService,
      public toastr: ToastrService
        ) {}
    
    ngOnInit() {
      const id = this.route.snapshot.queryParamMap.get('id');
      this.productService.getforEditProduct(id).subscribe((res) => {
        this.product = res;
      })
    }

  // plusQuantity(param){
  //   param.quantity++;
  // }

  // minusQuantity(param){
  //   param.quantity--;
  //   param.quantity =  param.quantity < 0 ? 0 : param.quantity;
  // }

  addToCart(param){
    const user = JSON.parse(localStorage.getItem('user'));
    if(!user) {  this.toastr.warning(
      "Cần đăng nhập để thêm giỏ hàng",
      "Thông báo",
      { timeOut: 3000 }
    );
    }
    else {
      this.value = this.value == ''? 'S' : this.value;
      this.cartService.addToCart(this.product.id,user.id, this.value).subscribe((res) => {
        if(res == 'error')
        {
          this.toastr.warning(
            "Giỏ hàng đã có sản phẩm",
            "Thông báo",
            { timeOut: 3000 }
            );
        }
        else if(res == 'success')
        {
          this.toastr.success(
            "Đã thêm vào giỏ hàng",
            "Thông báo",
            { timeOut: 3000 }
            );
           // this.router.navigateByUrl('/cart');
        }
        });
      }
  }

  showDialog() {
    this.visible = true;
}
}
