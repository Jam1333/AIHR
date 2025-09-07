export interface IInterviewMessage {
  id: string;
  question: string;
  answer: string | null;
  answeredOnUtc: string | null;
  createdOnUtc: string;
}
