import { Link } from "react-router-dom";
import { IAnalysis } from "../types/analysis";
import { Spinner } from "./Spinner";

interface AnalysisCardProps {
  analysis: IAnalysis;
}

export const AnalysisCard = ({ analysis }: AnalysisCardProps) => {
  return (
    <div
      className={`${
        analysis.isLoaded ? "bg-emerald-400" : "bg-zinc-500"
      } min-h-20 flex rounded-2xl flex-row p-4 justify-between items-center`}
    >
      {analysis.isLoaded ? (
        <Link to={`/analyses/${analysis.id}`} className="ml-3 text-xl">
          {analysis.title}
        </Link>
      ) : (
        <p className="ml-3 text-xl">{analysis.title}</p>
      )}
      {!analysis.isLoaded ? (
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
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
          />
        </svg>
      )}
    </div>
  );
};
