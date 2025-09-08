import { Link, useLocation } from "react-router-dom";
import { useAppSelector } from "../hooks/redux";
import { LogoutButton } from "./LogoutButton";

export const HeaderComponent = () => {
  const { currentUser } = useAppSelector((state) => state.userReducer);

  const location = useLocation();

  const paths = [
    { title: "Главная", link: "/" },
    { title: "Вакансии", link: "/vacancies" },
  ];

  return (
    <header className="w-[100vw] flex md:flex-row flex-col justify-around items-center">
      <div className="w-40 m-3 flex justify-center items-center text-center">
        Logo
      </div>

      <div className="w-[28vw] m-6 min-w-80 h-16 flex justify-center rounded-full bg-gray-700">
        <nav className="text-white w-5/6 flex items-center justify-center space-x-52 text-xl">
          <div className="w-[70%] flex justify-between">
            {paths.map((l, i) => (
              <Link
                key={i}
                to={l.link}
                className={
                  l.link === location.pathname
                    ? "w-auto h-auto rounded-full bg-emerald-400 px-[1vw] py-[.5vh] text-white text-xl shadow-lg shadow-emerald-400/0 transition-all duration-300 hover:shadow-emerald-400/100"
                    : "w-auto h-auto rounded-full border-2 scale-1 border-emerald-400 px-[1vw] py-[.5vh] text-white text-xl shadow-lg shadow-emerald-400/0 transition-all duration-200 hover:scale-110 hover:bg-emerald-400 hover:shadow-emerald-400/100"
                }
              >
                {l.title}
              </Link>
            ))}
          </div>
        </nav>
      </div>
      <div className="w-40 m-3 flex justify-center items-center">
        {currentUser ? (
          <Link
            className="text-white text-xl bg-gray-700 p-3 px-4 rounded-full"
            to="/profile"
          >
            {`Профиль`}
          </Link>
        ) : (
          <Link
            className="text-white text-xl bg-gray-700 p-3 px-4 rounded-full"
            to="/login"
          >
            Войти
          </Link>
        )}
      </div>
    </header>
  );
};

// <a className="bg-emerald-400 w-3/12 h-1/2 text-center" href="">Главная</a>
