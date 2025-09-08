import { IProblemDetails } from "../types/problemDetails";

interface ErrorProps {
  problemDetails: IProblemDetails;
}

export const ErrorComponent = ({ problemDetails }: ErrorProps) => {
  const THEMES: Map<number, string[]> = new Map([
    [1, ["bg-blue-900", "text-blue-200", "text-blue-400"]],
    [2, ["bg-emerald-700", "text-emerald-200", "text-emerald-400"]],
    [3, ["bg-cyan-800", "text-cyan-200", "text-cyan-400"]],
    [4, ["bg-amber-500", "text-amber-100", "text-amber-200"]],
    [5, ["bg-red-800", "text-red-200", "text-red-400"]],
  ]);

  const themeIndex = Number(`${problemDetails.status}`[0]);
  const bgColor = THEMES.get(themeIndex)?.at(0);
  const textMainColor = THEMES.get(themeIndex)?.at(1);
  const textSubColor = THEMES.get(themeIndex)?.at(2);

  return (
    <>
      <div
        className={`p-8 ${textMainColor} rounded-2xl ${bgColor} flex flex-col justify-start`}
      >
        <div className="space-x-2 text-center">
          <span className="text-xl font-bold">
            Status code: {problemDetails.status}
          </span>
          <p>{problemDetails.detail}</p>
        </div>
        {problemDetails.errors && (
          <div className={`mt-2 max-w-screen-sm ${textSubColor} px-4`}>
            <ul>
              {problemDetails.errors.map((e, i) => (
                <li key={i}>{e}</li>
              ))}
            </ul>
          </div>
        )}
      </div>
    </>
  );
};
