import { ServiceProvider } from './serviceProvider';

export class WorkOrder {
  id: number;
  status: number;
  dueDate: Date;
  description: string;
  serviceProvider: ServiceProvider;
  createdAt: Date;
  PropertyHolderName: string;
  PHPhoneNumber: string;
  PHAddress: string;
}
