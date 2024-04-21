import { Address } from "./address"

export interface Order {
  id: number
  clientEmail: string
  orderShippingAddress: Address
  shippingMethod: string
  shippingCost: number
  orderItemDtos: OrderItemDto[]
  sumCost: number
  fullCost: number
  orderDate: string
  orderStatus: string
}

export interface OrderItemDto {
  id: number
  name: string
  cost: number
  productNumber: number
  imageUrl: string
}

export interface OrderCreate{
    cartId: string,
    shippingMethodId: number,
    ShippingAddressDto: AddressDto
}

export interface AddressDto{
  FirstName :string,
  LastName:string,
  Country :string,
  Voivodeship :string,
  City :string,
  Street :string,
  BuildingNumber :string,
  ZipCode :string,
}