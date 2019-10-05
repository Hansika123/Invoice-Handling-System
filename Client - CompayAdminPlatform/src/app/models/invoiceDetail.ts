import { Item } from './item';

export class InvoiceDetail {
  Id: number;
  InvoiceNumber: number;
  ItemCode: number;
  Quentity: number;
  SubTotal: number;
  Discount: number;
  Total: number;
  Date: Date;
  Description: String;
  ServiceFee: number;
  InvoiceStatus: number;
  ServiceProvideId: number;
  InvoiceSubject: string;
  Items: Item[];
}
