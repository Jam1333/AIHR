interface ErrorProps {
    status: number;
    message: string
}


export const ErrorComponent = ({status, message}: ErrorProps) => {
    const THEMES: Map<number, string[]> = new Map([
        [1, ["bg-blue-900", "text-blue-200", "text-blue-400"]],
        [2, ["bg-emerald-700", "text-emerald-200", "text-emerald-400"]],
        [3, ["bg-cyan-800", "text-cyan-200", "text-cyan-400"]],
        [4, ["bg-amber-500", "text-amber-100", "text-amber-200"]],
        [5, ["bg-red-800", "text-red-200", "text-red-400"]]
    ])

    const themeIndex = Number(`${status}`[0]);
    const bgColor = THEMES.get(themeIndex)?.at(0);
    const textMainColor = THEMES.get(themeIndex)?.at(1);
    const textSubColor = THEMES.get(themeIndex)?.at(2);

    return (
        <>
            <div className={`min-w-[28vw] min-h-[30vh] p-8 ${textMainColor} rounded-2xl ${bgColor} flex flex-col justify-start`}>
                <div className="mb-4 space-x-2">
                    <span>icon</span><span className="text-xl font-bold">Status code: {status}</span>
                </div>
                <div className={`max-w-screen-sm h-52 ${textSubColor} px-4`}>
                    <p>{message}</p>
                </div>
            </div>
        </>
    )
}



