import { IResumeResult } from "../types/resumeResult";
import { numberToPercentage } from "../utils/numberToPercentage";

interface ResumeResultModalProps {
  resumeResult: IResumeResult;
  onClose: () => void;
}

export const ResumeResultModal = ({
  resumeResult,
  onClose,
}: ResumeResultModalProps) => {
  console.log("hello");

  return (
    <>
      <div className="relative p-4 w-full">
        <div className="relative shadow-sm bg-gray-700 px-10 rounded-[2vw] w-[80vw] min-w-[350px]">
          <div className="flex items-center justify-center p-4 md:p-5 border-b rounded-t dark:border-gray-600 border-gray-200">
            <h3 className="text-center text-xl font-semibold text-gray-900 dark:text-white">
              {resumeResult.title}
            </h3>
          </div>
          <div className="flex flex-col gap-4 justify-center">
            <div className="p-4 md:p-5 space-y-4">
              {Object.entries(
                resumeResult.requirementsTotal.requirementResults
              ).map(([category, requirements], i) => (
                <div key={i}>
                  <h2 className="font-medium">{category}</h2>
                  {requirements.map((r, i) => (<p>{r.text}: {numberToPercentage(r.similarity)}</p>))}
                </div>
              ))}
            </div>
            <div className="p-4 md:p-5 space-y-4 self-center">
              <span>Total similarity: </span>
              <span className="font-medium">
                {numberToPercentage(
                  resumeResult.requirementsTotal.totalSimilarity
                )}
              </span>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};
