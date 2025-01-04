import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserCartService } from 'src/services/cartService.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnDestroy{
  baseUrl = 'http://localhost:5001';
  products: any[] = [];
  value = '';
  // user;
  totalPrice: number;

  constructor(
    private router: Router,
    private cartService: UserCartService,
    public toastr: ToastrService) { }
  ngOnDestroy(): void {
    // debugger;
    // window.alert('1');
  }

  ngOnInit() {
   // this.user = JSON.parse(localStorage.getItem('user'));
    this.getProducts();
  }

  getProducts(){
    this.cartService.getAllCart().subscribe((res) => {
      this.products = res;
      this.priceChange();
    })

  }
  plusQuantity(param){
    param.quantity++;
    this.priceChange();
    const product = Object.assign({},{
      "id": param.id ?? 0,
      "quantity": param.quantity ?? 1
    })
    this.cartService.updateQuantity(product).subscribe();
  }

  minusQuantity(param){
    param.quantity--;
    if(param.quantity < 1 ) param.quantity = 1;
    this.priceChange();
    const product = Object.assign({},{
      "id": param.id ?? 0,
      "quantity": param.quantity ?? 1
    })
    this.cartService.updateQuantity(product).subscribe();
  }

  delete(param){
    this.products = this.products.filter(e => e.id != param.id);
    this.cartService.deleteCart(param.id).subscribe(() => {
      this.priceChange();
    });
  }

  checkout(){
    this.router.navigateByUrl('/check-out');
  }

  priceChange(){
    this.totalPrice = 0;
    this.products.map(e => {
      this.totalPrice += (e.quantity*e.price);
    })
  }
}