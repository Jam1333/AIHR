import { IRequirementsTotal } from "./requirementsTotal";

export interface IResumeResult {
  id: string;
  title: string;
  requirementsTotal: IRequirementsTotal;
  createdOnUtc: string;
}
