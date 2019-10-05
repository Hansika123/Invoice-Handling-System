import { JobCategoryDetail } from './JobCategoryDetail';
export class RequestQuotation {
  createdAt: Date;
  modifiedAt: Date;
  refId: number;
  requestDescription: string;
  dueDate: Date;
  requestName: string;
  quoteRequestId: number;
  serviceProviderId: number;
  PropertyHolderName: string;
  PHAddress: string;
  PHPhoneNumber: string;
  JobCategoryId: number;
  JobCategoryDetail: JobCategoryDetail;
  TaskId: number;
}
