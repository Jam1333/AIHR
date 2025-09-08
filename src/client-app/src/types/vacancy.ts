export interface IVacancy {
  id: string;
  title: string;
  language: string;
  text: string;
  requirements: Record<string, string[]>; //  Категория: требования[]
  isLoaded: boolean; // false - нельзя зайти
  userId: string;
  createdOnUtc: string;
}
