interface ErrorProps {
    status: number;
    message: string
}

export const ErrorComponent = ({status, message}: ErrorProps) => {

    const THEMES: Map<number, string[]> = new Map([
        [1, ["blue-900", "blue-400", "blue-200"]],
        [2, ["emerald-700", "emerald-200", "emerald-400"]],
        [3, ["cyan-800", "cyan-200", "cyan-400"]],
        [4, ["amber-500", "amber-100", "amber-200"]],
        [5, ["red-800", "red-200", "red-400"]]
    ])

    const themeIndex = Number(`${status}`[0]);

    return (
        <>
            <div className={`min-w-[28vw] min-h-[30vh] p-8 text-${THEMES.get(themeIndex)?.at(1)} rounded-2xl bg-${THEMES.get(themeIndex)?.at(0)} flex flex-col justify-start`}>
                <div className="mb-4 space-x-2">
                    <span>icon</span><span className="text-xl font-bold">Status code: {status}</span>
                </div>
                <div className={`max-w-screen-sm h-52 text-${THEMES.get(themeIndex)?.at(2)} px-4`}>
                    <p>{message}</p>
                </div>
            </div>
        </>
    )
}



