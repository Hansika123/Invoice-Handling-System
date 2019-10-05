import { Item } from 'src/app/models/item';

export class Quotation {
  refferenceId: number;
  approvedAdminId: number;
  estimatedSubTotal: number;
  estimatedServiceFee: number;
  estimatedTotal: number;
  jobDescription: string;
  serviceProviderId: number;
  status: number;
  items: Item[] = [];
  quatationRequestId: number;
}
