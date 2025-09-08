import { Link } from "react-router-dom";
import { IInterview } from "../types/interview";
import { Spinner } from "./Spinner";

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
      {interview.hasEnded ? (
        <Link to={`/analyses/${interview.id}`} className="ml-3 text-xl">
          {interview.title}
        </Link>
      ) : (
        <p className="ml-3 text-xl">{interview.title}</p>
      )}
      {!interview.hasEnded ? (
        <Spinner />
      ) : (
        <svg
          width="24"
          height="24"
          viewBox="0 0 24 24"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M5 13L9 17L19 7"
            stroke="white"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      )}
    </div>
  );
};
