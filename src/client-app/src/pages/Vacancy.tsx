import { AnalyzesListComponent } from "../UI/AnalyzesListComponent"
import { InterviewList } from "../UI/InterviewList"

export const Vacancy = () => {
    return(
        <>
            <div className="bg-gray-700 pt-6 px-10 rounded-[2vw] w-[80vw] min-h-[80vh]">
                <div className="text-4xl  mb-3">Вакансия №Леха п1др</div>
                <div className="h-1 bg-white rounded-full"></div>

                <div className="mt-4 mx-4 min-h-[22vh] flex flex-col space-y-2">
                    <div className="flex flex-row gap-12">
                        <span>Описание:</span><span>Далеко-далеко за словесными горами в стране гласных и согласных живут рыбные тексты. Если вдали переулка решила ручеек, реторический, коварных текстами, которой свой заглавных лучше но проектах парадигматическая обеспечивает своего рыбного о своих!</span>
                    </div>
                    <div className="flex flex-row gap-8 mb-2">
                        <span>Язык вакансии:</span><span>Русский</span>
                    </div>
                    <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

                    <div className="text-xl">Категории</div>
                    <div className="">
                        <div className="">
                            <span>Категория 1: </span>
                            <span>Требование 1</span>
                            , <span>Требование 2</span>
                        </div>
                        <div className="">
                            <span>Категория 2: </span>
                            <span>Требование 1</span>
                            , <span>Требование 2</span>
                        </div>
                    </div>
                    <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>

                    <div className="">
                        <span>Дата создания: </span><span>00.00.0000</span>
                    </div>
                </div>

                <div className="flex flex-row justify-around py-12 px-12 min-h-[28vh]">
                    <InterviewList />
                    <AnalyzesListComponent />
                </div>
            </div>
        </>
    )
}