import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class DiscountService {
  baseUrl = 'http://localhost:5001/api/discount/';
  constructor(private http: HttpClient) { }
  // getAllCart(): Observable<any> {
  //   const user = JSON.parse(localStorage.getItem('user'));
  //   return this.http.get(this.baseUrl + 'get-all' +'?customerId=' + user.id);
  // } 
  // deleteCart(id:number) {
  //   return this.http.delete(this.baseUrl + 'delete/' + id);
  // }
  // addToCart(productId: number,customerId: number, size: string ) {
  //   return this.http.post(this.baseUrl + 'add', {
  //       "id": 0,
  //       "productId": productId,
  //       "customerId": customerId,
  //       "size": size
  //     },{responseType: 'text'});
  //   }
  getAllCartByStatus(): Observable<any> {
    return this.http.get(this.baseUrl + 'get-all-by-status' + '?status=1');
  }
  applyDiscount(discountCode: any) {
    return this.http.put(this.baseUrl + 'check-discount?discountCode=' + discountCode, null);
  }
}
