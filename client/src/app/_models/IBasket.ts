// export interface IBasket {
//   id: string;
//   items: IBasketItem[];
//   clientSecret?: string;
//   paymentIntentId?: string;
//   deliveryMethodId?: number;
//   shippingPrice?: number;
// }

export interface IBasketItem {
  id: number;
  productName: string;
  productId: string;
  quantity: number;
  pictureUrl: string;
}

// export class Basket implements IBasket {
//   id = uuid();
//   items: IBasketItem[] = [];
// }

export interface IBasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
