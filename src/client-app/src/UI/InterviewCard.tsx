import { Link } from "react-router-dom";
import { IInterview } from "../types/interview";
import { Spinner } from "./Spinner";
import { numberToPercentage } from "../utils/numberToPercentage";

interface InterviewCardProps {
  interview: IInterview;
}

export const InterviewCard = ({ interview }: InterviewCardProps) => {
  return (
    <div
      className={`${
        interview.hasEnded ? "bg-emerald-400" : "bg-zinc-500"
      } min-h-20 flex rounded-2xl flex-row p-4 justify-between items-center`}
    >
      <Link to={`/interviews/${interview.id}/result`} className="ml-3 text-xl">
        {interview.title}
      </Link>
      {!interview.hasEnded || !interview.interviewResult ? (
        <Spinner />
      ) : (
        <p>
          {numberToPercentage(
            interview.interviewResult.requirementsTotal.totalSimilarity
          )}
        </p>
      )}
    </div>
  );
};
