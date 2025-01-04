import { ViewOrderDetail } from "./viewOrderDetail";

export class ViewOrderDto{
    id: number;
    orderCode : string;
    orderStatus : number;
    totalAmount : number
    listDetailOrder : Array<ViewOrderDetail>
}