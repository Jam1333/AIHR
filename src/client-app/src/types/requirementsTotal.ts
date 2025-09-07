import { IRequirementResult } from "./requirementResult";

export interface IRequirementsTotal {
  requirementResults: Record<string, IRequirementResult>;
  totalSimilarity: number;
}
