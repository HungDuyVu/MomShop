export class ReceiveOrderDto {
    id: number;
    code: string;
    createdDate: Date;
    receivedDate: Date;
    supplier: string;
    receiver: string;
    description: string;
    status: number;
    totalMoney: number;
    details: [];
}