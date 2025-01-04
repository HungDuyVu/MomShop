import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductDto } from "src/models/product";

@Injectable({
  providedIn: 'root',
})
export class ImageService {

    constructor(private http: HttpClient) {}
    baseUrl = 'http://localhost:5001/api/product/';
  
    public uploadImage(formData: FormData, productId: number): Observable<any> {

      return this.http.post(this.baseUrl + 'upload-image?productId='+ productId, formData);
    }
  }