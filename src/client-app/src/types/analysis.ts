import { IResumeResult } from "./resumeResult";

export interface IAnalysis {
  id: string;
  title: string;
  resumeResults: IResumeResult[];
  isLoaded: boolean;
  vacancyId: string;
  userId: string;
  createdOnUtc: string;
}
