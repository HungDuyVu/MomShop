import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseData } from 'src/models/ResponseData';
import { UserCartService } from 'src/services/cartService.service';
import { UserOrderService } from 'src/services/user-order.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-app-user-view-response',
  templateUrl: './app-user-view-response.component.html',
  styleUrls: ['./app-user-view-response.component.scss']
})
export class AppUserViewResponseComponent {
  data: ResponseData = new ResponseData();
  isPayment;
  statusCode: string;
  orderId: string;
  constructor(private http: HttpClient, private router: Router,private cartService: UserCartService,
    private userService: UserService,public toastr: ToastrService,
    private userOrderService: UserOrderService,
    private route: ActivatedRoute){
      
    }

  backtoHome(){
    this.router.navigateByUrl('/view');
    // this.route.queryParams.subscribe(params => {
    //   this.statusCode = params['vnp_ResponseCode']; // 'query' ở đây tương ứng với tên query parameter bạn muốn lấy giá trị
    //   if (this.statusCode == "00"){
    //     this.orderId = params['vnp_TxnRef'];
    //     this.data.statusCode = this.statusCode;
    //     this.data.orderId = this.orderId;

    //     this.userOrderService.receiveNofity(this.data).subscribe((data) => {
    //       this.router.navigateByUrl('/view');
    //     });
    //     this.isPayment = true;
        
    //   } else {
    //     this.isPayment = false;
    //   }
    // });
  }
}
