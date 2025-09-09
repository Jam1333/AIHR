import { interviewApi } from "../api/interviewApi";
import { toProblemDetails } from "../utils/toProblemDetails";
import { ErrorComponent } from "./ErrorComponent";
import { InterviewCard } from "./InterviewCard";
import { Spinner } from "./Spinner";
import { Link } from "react-router-dom";

interface InterviewListProps {
  vacancyId: string;
}

export const InterviewsList = ({ vacancyId }: InterviewListProps) => {
  const {
    data: interviews,
    isLoading,
    error,
  } = interviewApi.useFetchAllInterviewsQuery({ vacancyId: vacancyId });

  if (error) {
    return <ErrorComponent problemDetails={toProblemDetails(error)} />;
  }

  if (isLoading) {
    return <Spinner />;
  }

  return (
    <>
      <div className="w-[20vw] min-w-[275px] min-h-2 flex flex-col  space-y-2">
        <div className="flex p-3 m-2 flex-row justify-around gap-2">
          <span className="text-2xl font-bold tracking-wide">
            Собеседования
          </span>
          <Link to={`/vacancies/${vacancyId}/interviews/create`}>
            <button className="flex justify-center items-center bg-emerald-500 px-4 py-1  rounded-full text-xl text-center align-text-top transition duration-200 hover:scale-105 shadow-lg shadow-emerald-400/0  hover:shadow-emerald-400/50">
              <span className="pb-1">+</span>
            </button>
          </Link>
        </div>

        <div className="flex flex-col-reverse gap-2">
          {interviews?.map((interview, index) => (
            <InterviewCard key={index} interview={interview} />
          ))}
          {interviews?.length === 0 && (
            <p className="text-center">Пока нет собеседований...</p>
          )}
        </div>
      </div>
    </>
  );
};
