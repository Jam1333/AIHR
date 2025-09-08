import { Spinner } from "./Spinner";
import { Link } from "react-router-dom";


export const InterviewList = () => {
    const interviews: any[] = [
        ["Тестов Тест Тестович", true],
        ["Дотов Бог Старый", false],
        ["ФИО", true]
    ]

    return (
        <>
            <div className=" w-[20vw] min-h-2 flex flex-col  space-y-2">

                <div className="flex p-3 m-2 flex-row justify-around">
                    <span className="text-2xl font-bold tracking-wide">Собеседования</span>
                    <button className="bg-emerald-500 px-4 py-2  rounded-full text-xl text-center align-text-top transition duration-200 hover:scale-105 shadow-lg shadow-emerald-400/0  hover:shadow-emerald-400/50">+</button>
                </div>

                {/* Я не смог победить логику мапинга но стили кароч накидал, вот отсюда начинается часть которая мапится */}

                {/* Link'и пока ведут на главную */}
                <div className="bg-emerald-400 min-h-20 flex rounded-2xl flex-row p-4 justify-start items-center">
                    <Link to="" className="ml-3 text-xl">{interviews[0][0]}</Link>
                    {!interviews[0][1] ? <Spinner /> : <></>}
                </div>
                <div className="bg-zinc-500 min-h-20 flex rounded-2xl flex-row p-4 justify-between items-center">
                    <Link to="" className="ml-3 text-xl">{interviews[1][0]}</Link>
                    {!interviews[1][1] ? <Spinner /> : <></>}
                </div>
            </div>
        </>
    )
}