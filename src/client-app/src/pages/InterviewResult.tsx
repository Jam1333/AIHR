import { Link, useParams } from "react-router-dom";
import { useAppSelector } from "../hooks/redux";
import { UnauthorizedMessage } from "../UI/UnauthorizedMessage";
import { Spinner } from "../UI/Spinner";
import { ErrorComponent } from "../UI/ErrorComponent";
import { toProblemDetails } from "../utils/toProblemDetails";
import { utcStringToLocalDate } from "../utils/utcStringToLocalDate";
import { interviewApi } from "../api/interviewApi";
import { numberToPercentage } from "../utils/numberToPercentage";

export const InterviewResult = () => {
  const { id } = useParams();

  const { currentUser } = useAppSelector((state) => state.userReducer);

  const {
    data: interview,
    isLoading: isInterviewLoading,
    error: fetchInterviewError,
  } = interviewApi.useFetchInterviewByIdQuery(id ?? "");

  if (fetchInterviewError) {
    return (
      <ErrorComponent problemDetails={toProblemDetails(fetchInterviewError)} />
    );
  }

  if (isInterviewLoading) {
    return <Spinner />;
  }

  if (!currentUser || interview?.userId !== currentUser.id) {
    return <UnauthorizedMessage />;
  }

  return (
    <>
      <div className="bg-gray-700 py-6 px-10 rounded-[2vw] w-[80vw] min-w-[350px] min-h-[80vh]">
        <h2 className="text-center text-4xl  mb-3">
          Собеседование "{interview.title}"
        </h2>
        <div className="h-1 bg-white rounded-full"></div>

        <div className="mt-4 mx-4 min-h-[22vh] flex flex-col space-y-2">
          <div className="flex flex-col">
            <div className="flex flex-row gap-8 mb-2">
              <span>Язык вакансии:</span>
              <span>{interview.language}</span>
            </div>
            <div className="flex flex-row gap-8 mb-2">
              <span>Дата создания:</span>
              <span>{utcStringToLocalDate(interview.createdOnUtc)}</span>
            </div>
            <div className="flex flex-row gap-8 mb-2">
              <span>Ссылка на интервью:</span>
              <Link
                to={`/interviews/${id}`}
                className="underline"
              >{`${window.location.hostname}/interviews/${id}`}</Link>
            </div>
          </div>

          <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

          <h2 className="text-xl">Веса</h2>
          <div className="">
            {Object.entries(interview.weights).map(([category, weight], i) => (
              <div key={i} className="">
                <span>{category}: </span>
                <span>{weight}</span>
              </div>
            ))}
          </div>

          <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

          <h2 className="text-xl">Категории</h2>
          <div className="">
            {interview.interviewResult &&
              Object.entries(
                interview.interviewResult.requirementsTotal.requirementResults
              ).map(([category, requirements], i) => (
                <div key={i}>
                  <span className="font-bold">{category}: </span>
                  <ul className="flex flex-col">
                    {requirements.map((r, i) => (
                      <li key={i}>
                        <p>
                          {r.text}: {numberToPercentage(r.similarity)}
                        </p>
                      </li>
                    ))}
                  </ul>
                </div>
              ))}
          </div>

          <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

          <h2 className="text-xl">Красные флаги</h2>

          <ul className="flex flex-col gap-2">
            {interview?.interviewResult?.redFlags.map((rf, i) => (
              <li key={i}>{rf}</li>
            ))}
          </ul>

          <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

          <h2 className="text-xl">Несоответсвия</h2>

          <ul className="flex flex-col gap-2">
            {interview?.interviewResult?.inconsistencies.map((rf, i) => (
              <li key={i}>{rf}</li>
            ))}
          </ul>

          <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

          <div className="">
            <span>Общий процент соответствия: </span>
            <span className="font-medium">
              {interview.interviewResult
                ? numberToPercentage(
                    interview.interviewResult.requirementsTotal.totalSimilarity
                  )
                : "нет"}
            </span>
          </div>
        </div>
      </div>
    </>
  );
};
