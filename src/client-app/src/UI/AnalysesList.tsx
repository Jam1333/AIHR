import { analysisApi } from "../api/analysisApi";
import { toProblemDetails } from "../utils/toProblemDetails";
import { AnalysisCard } from "./AnalysisCard";
import { ErrorComponent } from "./ErrorComponent";
import { Spinner } from "./Spinner";
import { Link } from "react-router-dom";

interface AnalyzesListProps {
  vacancyId: string;
}

export const AnalysesList = ({ vacancyId }: AnalyzesListProps) => {
  const {
    data: analyses,
    isLoading,
    error,
  } = analysisApi.useFetchAllAnalysesQuery({ vacancyId: vacancyId });

  if (isLoading) {
    return <Spinner />;
  }

  if (error) {
    return <ErrorComponent problemDetails={toProblemDetails(error)} />;
  }

  return (
    <>
      <div className=" w-[20vw] min-w-[275px] min-h-2 flex flex-col  space-y-2">
        <div className="flex p-3 m-2 flex-row justify-around">
          <span className="text-2xl font-bold tracking-wide">Анализы</span>
          <Link to={`/vacancies/${vacancyId}/analyses/create`}>
            <button className="flex justify-center items-center bg-emerald-500 px-4 py-1  rounded-full text-xl text-center align-text-top transition duration-200 hover:scale-105 shadow-lg shadow-emerald-400/0  hover:shadow-emerald-400/50">
              <span className="pb-1">+</span>
            </button>
          </Link>
        </div>

        {analyses?.map((a, i) => (
          <AnalysisCard key={i} analysis={a} />
        ))}
        {analyses?.length === 0 && (
          <p className="text-center">Пока нет анализов...</p>
        )}
      </div>
    </>
  );
};
