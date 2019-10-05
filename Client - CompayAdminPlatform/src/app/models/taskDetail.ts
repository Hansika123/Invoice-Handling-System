import { CategoryDetail } from './categoryDetails';
import { WorkOrderDetail } from './workOrder';
import { ServiceProviderDetail } from './ServiceProviderDetail';

export interface TaskDetail {
  Id: number;
  DueDate: Date;
  PropertyHolderName: string;
  PHAddress: string;
  PHPhoneNumber: string;
  AssignStatus: string;
  CreatedAt: Date;
  Purpose: string;
  CallerSystemUserId: number;
  JobCategoryId: number;
  MaximumBudget: number;
  ServiceProvider: ServiceProviderDetail;
  WorkOrderDetail: WorkOrderDetail;
  JobCategoryDetail: CategoryDetail;
}
