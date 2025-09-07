import { IContactInformation } from "./contactInformation";
import { IInterviewMessage } from "./interviewMessage";
import { IInterviewResult } from "./interviewResult";

export interface IInterview {
  id: string;
  title: string;
  resumeText: string;
  language: string;
  weights: Record<string, number>;
  maxMessagesCount: number;
  interviewMessages: IInterviewMessage[];
  contactInformation: IContactInformation | null;
  interviewResult: IInterviewResult | null;
  conclusion: string | null;
  hasEnded: boolean;
  vacancyId: string;
  userId: string;
  createdOnUtc: string;
}
