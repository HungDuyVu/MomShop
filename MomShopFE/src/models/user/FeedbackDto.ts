export class FeedbackDto {
    id: number;
    customerName: string;
    orderId: number;
    orderCode: string;
    content: string;
    email: string;
    createdDate: Date;
    rating: number;
}