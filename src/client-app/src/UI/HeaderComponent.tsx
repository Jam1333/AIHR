import { Link } from "react-router-dom";

export const HeaderComponent = () => {
    const loc = document.location.href.split('/').at(-1);

    return (
        <header className="w-1/3 h-16 m-6 flex justify-center rounded-full bg-gray-700">
            <a href="/UI">UI</a>
            <nav className="text-white w-5/6 flex items-center justify-center space-x-52 text-xl">
                <div className="w-[70%] flex justify-between">
                    {loc === "" ? <Link to="/" className="w-auto h-auto rounded-full bg-emerald-400 px-[1vw] py-[.5vh] text-white text-xl shadow-lg shadow-emerald-400/0 transition-all duration-300 hover:shadow-emerald-400/100">Главная</Link>: <Link to="/" className="w-auto h-auto rounded-full border-2 scale-1 border-emerald-400 px-[1vw] py-[.5vh] text-white text-xl shadow-lg shadow-emerald-400/0 transition-all duration-200 hover:scale-110 hover:bg-emerald-400 hover:shadow-emerald-400/100">Главная</Link>}

                    {loc === "vacancies" ? <Link to="/vacancies" className="w-auto h-auto rounded-full bg-emerald-400 px-[1vw] py-[.5vh] text-white text-xl shadow-lg shadow-emerald-400/0 transition-all duration-300 hover:shadow-emerald-400/100">Вакансии</Link>: <Link to="/vacancies" className="w-auto h-auto rounded-full border-2 scale-1 border-emerald-400 px-[1vw] py-[.5vh] text-white text-xl shadow-lg shadow-emerald-400/0 transition-all duration-200 hover:scale-110 hover:bg-emerald-400 hover:shadow-emerald-400/100">Вакансии</Link>}
                </div>
            </nav>
        </header>
    )
}

// <a className="bg-emerald-400 w-3/12 h-1/2 text-center" href="">Главная</a>