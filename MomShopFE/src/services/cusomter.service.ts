import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root',
})
export class CustomerService extends ServiceBase{
  baseUrl = 'http://localhost:5001/api/user-account/';
  constructor(private http: HttpClient) {
    super();
  }

  getAllCustomer(status?: number, keyword: string = "", date?: string): Observable<any> {
    let url_ = this.baseUrl + `get-all?`;
    url_ += this.convertParamUrl('status', status ?? '');
    url_ += this.convertParamUrl("keyword", keyword ?? '');
    url_ += this.convertParamUrl("birthDate", date ?? '');
    return this.http.get(url_);
  } 
  deleteCustomer(id:number) {
    return this.http.delete(this.baseUrl + 'delete/' + id);
  }
  
  get(id) : Observable<any>{
    return this.http.get(this.baseUrl+ 'get/' + id)
  }

  changeStatus(id) : Observable<any>{
    return this.http.put(this.baseUrl+ 'change-status/' + id, null)
  }
  getCollection(id, keyword: string = "") : Observable<any>{
    console.log("keyword", keyword);
    
    let url_ = this.baseUrl + `find/` + id + "?";
    url_ += this.convertParamUrl("keyword", keyword);
    return this.http.get(url_);
  }
}
