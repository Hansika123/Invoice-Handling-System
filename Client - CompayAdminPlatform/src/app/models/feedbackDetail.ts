import { ServiceProviderDetail } from './ServiceProviderDetail';

export interface FeedbackDetail {
  Id: number;
  PropertyHolderName: string;
  CategoryId: number;
  ServiceProviderId: number;
  RatingLevel: number;
  JobType: string;
  CreatedAt: Date;
  ServiceProvider: ServiceProviderDetail;
}
