import * as cuid from "cuid";
import { CartItem } from "./cartItem";

export interface Cart {
    id: string
    items: CartItem[]
  }

export interface CartSums{
  shippingCost: number;
  sumCost: number;
  fullCost: number;
}
  
export class Cart implements Cart{
    id = cuid();
    items: CartItem[] = [];
}
