export const Analysis = () => {

    // Если нужно сделать, чтобы при нажатии на конкретное резюме юзер редиректился на другую страницу, то можно поменять тег <span> на <Link/>
    return (
        <>
            <div className="bg-gray-700 pt-6 px-10 rounded-[2vw] w-[80vw] min-h-[80vh]">
                            <div className="text-4xl  mb-3">Анализ №1</div>
                            <div className="h-1 bg-white rounded-full"></div>
            
                            <div className="mt-4 mx-4 min-h-[22vh] flex flex-col space-y-2">
                                <div className="text-xl">Категории</div>
                                <div className="">
                                    <div className="">
                                        <span>Категория 1: </span>
                                        <span>Вес</span>
                                    </div>
                                    <div className="">
                                        <span>Категория 2: </span>
                                        <span>Вес</span>
                                    </div>
                                </div>
                                <div className="h-0.5 bg-[#b4b4b4] rounded-full"></div>
            
                                <div className="px-8 py-4 flex flex-row justify-start items-center">
                                    <table className="table-auto">
                                        <thead className="border-b-2 border-b-white">
                                            <tr>
                                                <th>
                                                    <span className="p-4">Название Резюме</span>
                                                </th>
                                                <th className="border-l-2 border-l-white">
                                                    <span className="p-4">Общий процент сходства</span>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody className="">
                                            <tr>
                                                <td>
                                                    <span className="p-4">Резюме 1</span>
                                                </td>
                                                <td className="border-l-2 border-l-white">
                                                    <span className="p-4">100%</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span className="p-4">Резюме 2</span>
                                                </td>
                                                <td className="border-l-2 border-l-white">
                                                    <span className="p-4">99.9999999%</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span className="p-4">Резюме 3</span>
                                                </td>
                                                <td className="border-l-2 border-l-white">
                                                    <span className="p-4">52%</span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
        </>
    )
}
