import { IAddress } from './IAddress';

export interface IOrder {
  id: string;
  orderDate: string;
  shipToAddress: IAddress;
  orderItems: IOrderItem[];
  status: string;
  buyerEmail: string;
  total: number;
}
export interface IOrderItem {
  productItemId: string;
  productName: string;
  pictureUrl: string;
  price: number;
  quantity: number;
  id: string;
}
/**
 * {
        "id": "169930e6-5b60-4ff9-94b4-ef27cfbdb23c",
        "orderDate": "2020-06-13T20:30:34.8334562",
        "shipToAddress": {
            "name": "Eric",
            "street": "1011st 32 ave",
            "city": "Vancouver",
            "province": "BC",
            "country": "Canada",
            "postalCode": "V4n2l1"
        },
        "orderItems": [
            {
                "productItemId": "f9e1a467-f56e-4d02-9f27-660afcd44eb1",
                "productName": "Green React Woolen Hat",
                "pictureUrl": "images/products/hat-react1.png",
                "price": 8,
                "quantity": 3,
                "id": "4cb765b9-34ee-48da-b49b-94adb575ee88"
            }
        ],
        "status": "Order Pending",
        "buyerEmail": "eric@email.com",
        "total": 24
    }
 */
