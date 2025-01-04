import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ServiceBase } from "./service-base";


@Injectable({
    providedIn: 'root',
})

export class ReceiveOrderService extends ServiceBase{
    baseUrl = 'http://localhost:5001/api/receive-order/';
    constructor(private http: HttpClient) {
        super();
    }
    //xem thông tin đơn hàng
    getAllReceiveOrder(status?: string, keyword: string = "", createdDate? :string, intendedTime?: string): Observable<any> {
        let url_ = this.baseUrl + `find-all?`;
        url_ += this.convertParamUrl('status', status ?? '');
        url_ += this.convertParamUrl("keyword", keyword);
        url_ += this.convertParamUrl("createdDate", createdDate ?? '');
        url_ += this.convertParamUrl("intendedTime", intendedTime ?? '');
        return this.http.get(url_);
    }
    //xem thông tin đơn hàng theo id
    getReceiveOrderById(id: number): Observable<any> {
        return this.http.get(this.baseUrl + 'find-by-id/' + id)
    }
    //thêm, sửa thông tin đơn hàng
    createOrEditReceiveOrder(order: any) {
        if (order.id) {
            return this.http.put(this.baseUrl + 'update', order);
        }
        else {
            return this.http.post(this.baseUrl + 'add', order);
        }
    }
    //xóa thông tin đơn hàng
    deleteReceiveOrder(id: number) {
        return this.http.delete(this.baseUrl + 'delete/' + id)
    }
    /// chi tiết đơn hàng
    getDetailReceiveOrder(id: number) {
        return this.http.get(this.baseUrl + 'details/' + id)
    }
    /// chi tiết đơn hàng
    getDetai(id: number) {
        return this.http.get(this.baseUrl + 'detail/find/' + id)
    }
    //thêm thông tin chi tiết đơn hàng
    createOrEditDetailReceiveOrder(order: any) {
        if (order.id) {
            return this.http.put(this.baseUrl + "update-detail", order)
        }
        else {
            return this.http.post(this.baseUrl + "add-detail", order);
        }
    }
    deleteDetailReceiveOrder(id: number){
        return this.http.delete(this.baseUrl + 'delete-detail/' + id)
    }

    /// chuuyeenr trạng thái sang thanh toán
    paymentOrder(id: number) {
        return this.http.put(this.baseUrl + 'payment/' + id, null)
    }
}

