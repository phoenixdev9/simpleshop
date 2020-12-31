// export interface IBasket {
//   id: string;
//   items: IBasketItem[];
//   clientSecret?: string;
//   paymentIntentId?: string;
//   deliveryMethodId?: number;
//   shippingPrice?: number;
// }

export interface IBasketItem {
  id: string;
  productName: string;
  productId: string;
  quantity: number;
  price: number;
  pictureUrl: string;
}

export interface IAddBasket {
  ProductId: string;
  Quantity: number;
}

export interface IBasket {
  cart: IBasketItem[];
  total: number;
  totalItems: number;
}
