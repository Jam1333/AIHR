import { IRequirementsTotal } from "./requirementsTotal";

export interface IResumeResult {
  id: string;
  title: string; //Название по имени файла
  requirementsTotal: IRequirementsTotal;
  createdOnUtc: string;
}
