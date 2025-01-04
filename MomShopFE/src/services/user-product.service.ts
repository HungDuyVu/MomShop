import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root'
})
export class UserProductService extends ServiceBase {
baseUrl = 'http://localhost:5001/api/user/product/';
  constructor(private http: HttpClient) {
    super();
  }

  getShirt(shirtName:string, keyword?: string): Observable<any> {
    console.log("l",keyword);
    
    let url_ = this.baseUrl + shirtName;
    if (keyword != undefined){
      url_ = url_ + '?keyword=' + keyword;
    }
    console.log("url", url_);
    
    return this.http.get(url_);
  } 
}