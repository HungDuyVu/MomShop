import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root'
})
export class UserOrderService extends ServiceBase{
  baseUrl = 'http://localhost:5001/api/user/order/';
  constructor(private http: HttpClient) {
    super();
  }

  addOrder(input: any) {
    return this.http.post(this.baseUrl + 'add', input)
  }
  getAllOrder(customerId?: number) {
    let url_ = this.baseUrl + `get?`;
    url_ += this.convertParamUrl('customerId', customerId ?? '');
    return this.http.get(url_);
  }

  cancelOrder(id) {
    console.log("service id", id);
    return this.http.put(this.baseUrl + 'cancel-order/' + id, null);
  }
  deleteOrder(id) {
    return this.http.delete(this.baseUrl + 'delete/' + id);
  }

  receiveNofity(data: any) {
    return this.http.put(this.baseUrl + 'receive-notify', data);
  }
}