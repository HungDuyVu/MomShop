export class Order{
    id:number;
    orderCode:string;
    customerName:string;
    address:string;
    province:string;
    district:string;
    email:string;
    phone:string;
    createdDate:Date;
    intendedTime:Date;
    paymentType:number;
    orderStatus:number;
    totalAmount:number;
    orderDetails: any[];
    events: any[];
}