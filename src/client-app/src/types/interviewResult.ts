import { IRequirementsTotal } from "./requirementsTotal";

export interface IInterviewResult {
  id: string;
  requirementsTotal: IRequirementsTotal;
  redFlags: string[];
  inconsistencies: string[];
  createdOnUtc: string;
}
