import { useParams } from "react-router-dom";
import { analysisApi } from "../api/analysisApi";
import { useAppSelector } from "../hooks/redux";
import { ErrorComponent } from "../UI/ErrorComponent";
import { toProblemDetails } from "../utils/toProblemDetails";
import { UnauthorizedMessage } from "../UI/UnauthorizedMessage";
import { Spinner } from "../UI/Spinner";
import { RedirectToHome } from "../UI/RedirectToHome";
import { numberToPercentage } from "../utils/numberToPercentage";

export const Analysis = () => {
  const { id } = useParams();

  const {
    data: analysis,
    isLoading: isAnalysisLoading,
    error: fetchAnalysisError,
  } = analysisApi.useFetchAnalysisByIdQuery(id ?? "");

  const { isLoading: isUserLoading, error: fetchUserError } = useAppSelector(
    (state) => state.userReducer
  );

  if (isAnalysisLoading || isUserLoading) {
    <Spinner />;
  }

  if (fetchAnalysisError) {
    return (
      <ErrorComponent problemDetails={toProblemDetails(fetchAnalysisError)} />
    );
  }

  if (fetchUserError) {
    return <UnauthorizedMessage />;
  }

  if (!analysis) {
    return <></>;
  }

  return (
    <>
      <div className="bg-gray-700 pt-6 px-10 rounded-[2vw] w-[80vw] min-h-[80vh] min-w-[350px]">
        <div className="text-center text-4xl  mb-3">
          Анализ "{analysis.title}"
        </div>
        <div className="h-1 bg-white rounded-full"></div>

        <div className="mt-4 mx-4 min-h-[22vh] flex flex-col space-y-2">
          <div className="text-xl">Категории</div>
          <div className="">
            {Object.entries(analysis.weights).map(([category, weight], i) => (
              <div key={i} className="">
                <span>{category}: </span>
                <span>{weight}</span>
              </div>
            ))}
          </div>
          <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

          <div className="md:px-8 py-4 flex flex-row justify-start items-center self-center">
            <table className="table-auto">
              <thead className="border-b-2 border-b-white">
                <tr>
                  <th>
                    <span className="md:p-4">Название Резюме</span>
                  </th>
                  <th className="border-l-2 border-l-white">
                    <span className="md:p-4">Общий процент сходства</span>
                  </th>
                </tr>
              </thead>
              <tbody className="">
                {analysis.resumeResults.map((r, i) => (
                  <tr>
                    <td>
                      <span className="md:p-4">{r.title}</span>
                    </td>
                    <td className="border-l-2 border-l-white">
                      <span className="md:p-4">
                        {numberToPercentage(
                          r.requirementsTotal.totalSimilarity
                        )}
                      </span>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </>
  );
};
