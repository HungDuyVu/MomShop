import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NgModel } from "@angular/forms";
import { ServiceBase } from "./service-base";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
  })
  export class OrderService extends ServiceBase{
    baseUrl = 'http://localhost:5001/api/order/';
    constructor(private http: HttpClient) {
      super();
    }

    
    getAllOrder(status?: number, keyword: string = "", createdDate? :string, intendedTime?: string) : Observable<any>{
      let url_ = this.baseUrl + `find-all?`;
      url_ += this.convertParamUrl('status', status ?? '');
      url_ += this.convertParamUrl("keyword", keyword ?? '');
      url_ += this.convertParamUrl("createdDate", createdDate ?? '');
      url_ += this.convertParamUrl("intendedTime", intendedTime ?? '');
      return this.http.get(url_);
    }

    get(id) {
      return this.http.get(this.baseUrl + 'detail/' + id);
    }

    view(id) {
      return this.http.get(this.baseUrl + 'view/' + id);
    }

    acceptOrder(id) {
      return this.http.put(this.baseUrl + 'accept-order/' + id, null);
    }
    processOrder(id) {
      return this.http.put(this.baseUrl + 'process-order/' + id, null);
    }

    completeOrder(id) {
      return this.http.put(this.baseUrl + 'complete-order/' + id,null);
    }

    Delete(id) {
      return this.http.delete(this.baseUrl + 'delete/' + id);
    }

    cancelOrder(id) {
      console.log("service id", id);
      
      return this.http.put('http://localhost:5001/api/user/order/cancel-order/' + id, null);
    }
  }