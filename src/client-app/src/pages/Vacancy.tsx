import { useParams } from "react-router-dom";
import { AnalysesList } from "../UI/AnalysesList";
import { InterviewsList } from "../UI/InterviewsList";
import { useAppSelector } from "../hooks/redux";
import { UnauthorizedMessage } from "../UI/UnauthorizedMessage";
import { vacancyApi } from "../api/vacancyApi";
import { Spinner } from "../UI/Spinner";
import { ErrorComponent } from "../UI/ErrorComponent";
import { toProblemDetails } from "../utils/toProblemDetails";
import { utcStringToLocalDate } from "../utils/utcStringToLocalDate";

export const Vacancy = () => {
  const { id } = useParams();

  const { currentUser } = useAppSelector((state) => state.userReducer);

  const {
    data: vacancy,
    isLoading: isVacancyLoading,
    error: fetchVacancyError,
  } = vacancyApi.useFetchVacancyByIdQuery(id ?? "");

  if (fetchVacancyError) {
    return (
      <ErrorComponent problemDetails={toProblemDetails(fetchVacancyError)} />
    );
  }

  if (isVacancyLoading) {
    return <Spinner />;
  }

  if (!currentUser || vacancy?.userId !== currentUser.id) {
    return <UnauthorizedMessage />;
  }

  return (
    <>
      <div className="bg-gray-700 pt-6 px-10 rounded-[2vw] w-[80vw] min-w-[350px] min-h-[80vh]">
        <div className="text-center text-4xl  mb-3">Вакансия "{vacancy.title}"</div>
        <div className="h-1 bg-white rounded-full"></div>

        <div className="mt-4 mx-4 min-h-[22vh] flex flex-col space-y-2">
          <div className="flex flex-row gap-8 mb-2">
            <span>Язык вакансии:</span>
            <span>{vacancy.language}</span>
          </div>
          <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

          <div className="text-xl">Категории</div>
          <div className="">
            {Object.entries(vacancy.requirements).map(
              ([category, requirements], i) => (
                <div key={i}>
                  <span className="font-bold">{category}: </span>
                  <ul className="flex flex-col">
                    {requirements.map((r, i) => (
                      <li key={i}>{r}</li>
                    ))}
                  </ul>
                </div>
              )
            )}
          </div>
          <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>
          <div className="">
            <span>Дата создания: </span>
            <span>{utcStringToLocalDate(vacancy.createdOnUtc)}</span>
          </div>
        </div>

        <div className="flex flex-col md:flex-row justify-around items-start py-12 md:px-12 min-h-[28vh]">
          <InterviewsList vacancyId={vacancy.id} />
          <AnalysesList vacancyId={vacancy.id} />
        </div>
      </div>
    </>
  );
};
