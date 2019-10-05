import { UserAccountDetail } from './userAccountDetails';
import { CategoryDetail } from './categoryDetails';
import { BankDetail } from './bankDetail';

export interface ServiceProviderDetail {
  Id: number;
  companyName: string;
  CatId: number;
  Address: string;
  CompanyName: string;
  Category: string;
  CreatedAt: Date;
  FeedbackLevel: number;
  BankDetail: BankDetail;
  UserAccountDetails: UserAccountDetail;
  JobCategoryDetail: CategoryDetail;
}
