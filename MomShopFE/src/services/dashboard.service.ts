import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root',
})
export class DashboardService extends ServiceBase{
  baseUrl = 'http://localhost:5001/api/dashboard';
  constructor(private http: HttpClient) {
    super();
  }
  dashboard() {
      return this.http.get(this.baseUrl);
  }


  dashboardSecond(month?:number, year?:number) {
    let url_ = this.baseUrl + `/get-by-time?`;
    url_ += this.convertParamUrl('month', month ?? '');
    url_ += this.convertParamUrl("year", year ?? '');
    return this.http.get(url_);
  }
}
