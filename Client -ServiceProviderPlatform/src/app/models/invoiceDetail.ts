import { Item } from './item';

export class InvoiceDetail {
  invoiceNumber: number;
  itemCode: number;
  quentity: number;
  subTotal: number;
  discount: number;
  total: number;
  date: Date;
  description: String;
  invoiceSubject: String;
  serviceFee: number;
  invoiceStatus: number;
  serviceProvideId: number;
  items: Item[] = [];
}
