export class ProductConst {
    public static mediaStatus = {
        ACTIVE: 'Đã đăng',
        PENDING: 'Trình duyệt',
        DELETED: 'Đã xoá',
        DRAFT: 'Bản nháp'
    }

    public static productType = [
        {
            name: 'Áo thun',
            code: 1
        },
        {
            name: 'Áo Sơ Mi',
            code: 2
        },
        {
            name: 'Áo Khoác',
            code: 3
        },
        {
            name: 'Quần',
            code: 4
        },
        {
            name: 'Phụ Kiến',
            code: 5
        },
    ]

    public static productStatus = [
        {
            name: 'Đang bán',
            code: 1
        },
        {
            name: 'Chưa mở bán',
            code: 2
        },
        {
            name: 'Khóa',
            code: 3
        },
    ]

    public static getProductType(code){
        const result = this.productType.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }

    public static getProductStatus(code){
        const result = this.productStatus.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
    public static statusSeverity = {
        ACTIVE: 'success',
        PENDING: 'warning',
        DELETED: 'danger',
        DRAFT: 'secondary'
    }
    
}
export class OrderConst {
    public static mediaStatus = {
        ACTIVE: 'Đã đăng',
        PENDING: 'Trình duyệt',
        DELETED: 'Đã xoá',
        DRAFT: 'Bản nháp'
    }

    public static orderStatus = [
        {
            name: 'Tất cả',
            code: undefined
        },
        {
            name: 'Khởi tạo',
            code: 1
        },
        {
            name: 'Đã nhận',
            code: 2
        },
        {
            name: 'Đang giao',
            code: 3
        },
        {
            name: 'Hoàn thành',
            code: 4
        },
        {
            name: 'Đã hủy',
            code: 5
        },
        {
            name: 'Đã thanh toán',
            code: 6
        },
    ]
    public static getStatus(code){
        const result = this.orderStatus.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
    public static statusSeverity = {
        ACTIVE: 'success',
        PENDING: 'warning',
        DELETED: 'danger',
        DRAFT: 'secondary'
    }
}
export class ReceivedOrderConst {
    public static mediaStatus = {
        ACTIVE: 'Đã đăng',
        PENDING: 'Trình duyệt',
        DELETED: 'Đã xoá',
        DRAFT: 'Bản nháp'
    }

    public static receiveStatus = [
        {
            name: 'Tất cả',
            code: undefined
        },
        {
            name: 'Chưa thanh toán',
            code: 1,
            severity: 'warning'
        },
        {
            name: 'Đã thanh toán',
            code: 2,
            severity: 'success'
        },
        {
            name: 'Đã hoàn thành',
            code: 3,
            severity: 'primary'
        },
    ]
    public static getOrderStatus(code: number){
        const result = this.receiveStatus.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
    public static getOrderTag(code: number){
        const result = this.receiveStatus.find(item => 
            item.code == code
        );
        return result?.severity ?? "";
    }
    public static getStatus(code){
        const result = this.receiveStatus.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
    public static statusSeverity = {
        ACTIVE: 'success',
        PENDING: 'warning',
        DELETED: 'danger',
        DRAFT: 'secondary'
    }
}
export class ProductStatus{
    public static productType = [
        {
            name: 'Áo thun',
            code: 1
        },
        {
            name: 'Áo Sơ Mi',
            code: 2
        },
        {
            name: 'Áo Khoác',
            code: 3
        },
        {
            name: 'Quần',
            code: 4
        },
        {
            name: 'Phụ Kiến',
            code: 5
        },
    ]

    public static productStatus = [
        {
            name: 'Đang bán',
            code: 1,
            severity: 'success'
        },
        {
            name: 'Chưa mở bán',
            code: 2,
            severity: 'primary'
        },
        {
            name: 'Khóa',
            code: 3,
            severity: 'warning'
        },
        {
            name: 'Hết hàng',
            code: 4,
            severity: 'danger'
        },
    ]

    public static getProductType(code: number){
        const result = this.productType.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }

    public static getProductStatus(code: number){
        const result = this.productStatus.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
    public static getProductTag(code: number){
        const result = this.productStatus.find(item => 
            item.code == code
        );
        return result?.severity ?? "";
    }
}

export class DashboardConst {
    public static getSeverity(status: number) {
        switch (status) {
            case 1:
                return 'success';
            case 2:
                return 'warning';
            case 3:
                return 'info';
            case 4:
                return 'danger';
        }
        return "";
    }
}

export class CustomerConst {
    public static customerStatus = [
        {
            name: 'Hoạt động',
            code: 1,
            severity: 'success'
        },
        {
            name: 'Khóa',
            code: 2,
            severity: 'primary'
        },
    ]
    public static getCustomerStatus(code: number){
        const result = this.customerStatus.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
    public static getCustomerTag(code: number){
        const result = this.customerStatus.find(item => 
            item.code == code
        );
        return result?.severity ?? "";
    }

    public static getSeverity(status: number) {
        switch (status) {
            case 1:
                return 'success';
            case 2:
                return 'warning';
            case 3:
                return 'info';
            case 4:
                return 'danger';
        }
        return "";
    }
}

export class OrderStatus {
    public static orderStatus = [
        {
            name: 'Khởi tạo',
            code: 1,
            severity: 'primary'
        },
        {
            name: 'Đã nhận',
            code: 2,
            severity: 'primary'
        },

        {
            name: 'Đang giao',
            code: 3,
            severity: 'warning'
        },
        {
            name: 'Hoàn thành',
            code: 4,
            severity: 'success'
        },
        {
            name: 'Đã hủy',
            code: 5,
            severity: 'danger'
        },
        {
            name: 'Đã thanh toán',
            code: 6,
            severity: 'success'
        },
    ]
    public static getOrderStatus(code: number){
        const result = this.orderStatus.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
    public static getOrderTag(code: number){
        const result = this.orderStatus.find(item => 
            item.code == code
        );
        return result?.severity ?? "";
    }

    public static getSeverity(status: number) {
        switch (status) {
            case 1:
                return 'success';
            case 2:
                return 'warning';
            case 3:
                return 'info';
            case 4:
                return 'danger';
        }
        return "";
    }
}

export class CollectionStatus {
    public static collectionStatus = [
        {
            name: 'Chưa mở bán',
            code: 2,
            severity: 'primary'
        },
        {
            name: 'Hoạt động',
            code: 1,
            severity: 'success'
        },

        {
            name: 'Khóa',
            code: 3,
            severity: 'warning'
        },
    ]
    public static getOrderStatus(code: number){
        const result = this.collectionStatus.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
    public static getOrderTag(code: number){
        const result = this.collectionStatus.find(item => 
            item.code == code
        );
        return result?.severity ?? "";
    }

    public static getSeverity(status: number) {
        switch (status) {
            case 1:
                return 'success';
            case 2:
                return 'warning';
            case 3:
                return 'info';
            case 4:
                return 'danger';
        }
        return "";
    }
}

export class PaymentType {
    public static paymentType = [
        {
            name: 'Chuyển khoản',
            code: 2,
        },
        {
            name: 'COD',
            code: 1,
        },
    ]
    public static getPaymentType(code: number){
        const result = this.paymentType.find(item => 
            item.code == code
        );
        return result?.name ?? "";
    }
}