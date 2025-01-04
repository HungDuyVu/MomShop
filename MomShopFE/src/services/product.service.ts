import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ServiceBase } from './service-base';

@Injectable({
  providedIn: 'root',
})
export class ProductService extends ServiceBase{
  baseUrl = 'http://localhost:5001/api/product/';
  constructor(private http: HttpClient) {
    super();
  }
  getAllProduct(status?: number, keyword: string = ""): Observable<any> {
    let url_ = this.baseUrl + `find-all?`;
    url_ += this.convertParamUrl('status', status ?? '');
    url_ += this.convertParamUrl("keyword", keyword);
    return this.http.get(url_);
  } 
  deleteProduct(id:number) {
    return this.http.delete(this.baseUrl + 'delete/' + id);
  }
  createOrEdit(product: any) {
    if (product.id) {
      console.log("update");
      
      return this.http.put(this.baseUrl + 'update', product);
    } else {
      return this.http.post(this.baseUrl + 'add', product);
    }
  }
  getforEditProduct(id) : Observable<any>{
    return this.http.get(this.baseUrl+ 'find-by-id/' + id)
  }

  getforEditProductDetail(id) : Observable<any>{
    return this.http.get(this.baseUrl+ 'product-detail/' + id)
  }
  createOrEditDetailProduct(productDetail:any,){
    if (productDetail.id) {
      return this.http.put(this.baseUrl + 'update-detail', productDetail);
    } else {
      return this.http.post(this.baseUrl + 'add-detail', productDetail);
    }
  }
  deleteDetailProduct(productDetailId:number) {
    return this.http.delete(this.baseUrl + 'delete-detail/' + productDetailId);
  }
  getAllViewDetailProduct(productDetailId:number) : Observable<any> {
    return this.http.get(this.baseUrl + 'detail/' + productDetailId);
  }
}
