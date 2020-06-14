import { IBasketItem } from '../_models/IBasket';

export const _checkItemExistsInCart = (b: IBasketItem[], productId: string) => {
  if (b.some((e: IBasketItem) => e.productId === productId)) {
    return true;
  } else {
    return false;
  }
};
export const _findItemExistsInCart = (b: IBasketItem[], productId: string) => {
  if (b) {
    return b.find((e: IBasketItem) => e.productId === productId);
  }
};
