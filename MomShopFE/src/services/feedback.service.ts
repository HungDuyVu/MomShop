import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root',
})
export class FeedbackService extends ServiceBase{
  baseUrl = 'http://localhost:5001/api/feedback/';
  constructor(private http: HttpClient) {
    super();
  }

  getAllfeedback(rating?: number, keyword: string = ""): Observable<any> {
    let url_ = this.baseUrl + `get-all?`;
    url_ += this.convertParamUrl('rating', rating ?? '');
    url_ += this.convertParamUrl("keyword", keyword);
    return this.http.get(url_);
  } 
}
