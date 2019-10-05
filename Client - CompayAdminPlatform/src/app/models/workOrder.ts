export interface WorkOrderDetail {
  Id: number;
  Status: number;
  DueDate: Date;
  Description: string;
  ServiceProviderID: number;
  CreatedAt: Date;
  PropertyHolderName: string;
  PHAddress: string;
  PHPhoneNumber: string;
  TaskId: number;
}
