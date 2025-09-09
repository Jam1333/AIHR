import { Link } from "react-router-dom";
import { IVacancy } from "../types/vacancy";

interface VacancyCardProps {
  vacancy: IVacancy;
}

export const VacancyCard = ({ vacancy }: VacancyCardProps) => {
  return (
    <div className="bg-gray-800 p-5 rounded-3xl relative">
      <div className="absolute top-3 right-3">
        <div
          className={`w-4 h-4 rounded-full ${
            vacancy.isLoaded ? "bg-green-800" : "bg-red-800"
          }`}
        ></div>
      </div>
      <h3 className="text-white font-medium">
        {vacancy.isLoaded ? (
          <Link to={`/vacancies/${vacancy.id}`}>{vacancy.title}</Link>
        ) : (
          vacancy.title
        )}
      </h3>
    </div>
  );
};
