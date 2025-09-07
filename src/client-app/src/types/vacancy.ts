export interface IVacancy {
  id: string;
  title: string;
  language: string;
  text: string;
  requirements: Record<string, string[]>;
  isLoaded: boolean;
  userId: string;
  createdOnUtc: string;
}
