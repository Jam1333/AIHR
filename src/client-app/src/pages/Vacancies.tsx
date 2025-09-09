import { Link } from "react-router-dom";
import { vacancyApi } from "../api/vacancyApi";
import { useAppSelector } from "../hooks/redux";
import { ErrorComponent } from "../UI/ErrorComponent";
import { RedirectToLogin } from "../UI/RedirectToLogin";
import { Spinner } from "../UI/Spinner";
import { VacancyCard } from "../UI/VacancyCard";
import { toProblemDetails } from "../utils/toProblemDetails";

export const Vacancies = () => {
  const {
    currentUser,
    isLoading: isCurrentUserLoading,
    error: fetchCurrentUserError,
  } = useAppSelector((state) => state.userReducer);

  const {
    data: vacancies,
    isLoading: isVacanciesLoading,
    error: fetchVacanciesError,
  } = vacancyApi.useFetchAllVacanciesQuery({
    userId: currentUser?.id ?? "",
  });

  if (fetchCurrentUserError) {
    return <RedirectToLogin />;
  }

  if (isCurrentUserLoading || isVacanciesLoading || !vacancies) {
    return <Spinner />;
  }

  if (fetchVacanciesError) {
    return (
      <ErrorComponent problemDetails={toProblemDetails(fetchVacanciesError)} />
    );
  }

  return (
    <div className="min-h-screen">
      <div className="flex justify-center items-center mb-8">
        <Link to={`/vacancies/create`}>
          <button className="bg-green-800 hover:bg-green-700 text-white px-6 py-3 rounded-3xl font-medium">
            Создать вакансию
          </button>
        </Link>
      </div>

      <div className="grid grid-cols-3 gap-5">
        {vacancies.map((vacancy, index) => (
          <VacancyCard key={index} vacancy={vacancy} />
        ))}
      </div>
      {vacancies.length === 0 && (
        <p className="text-center mt-2">Вакансий пока нет...</p>
      )}
    </div>
  );
};
