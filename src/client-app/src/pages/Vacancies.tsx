import { useAppSelector } from "../hooks/redux";
import { RedirectToLogin } from "../UI/RedirectToLogin";

export const Vacancies = () => {
  const { currentUser } = useAppSelector((state) => state.userReducer);

  const vacancies = [
    "Вакансия 1",
    "Вакансия 5",
    "Вакансия 9",
    "Вакансия 2",
    "Вакансия 6",
    "Вакансия 10",
    "Вакансия 3",
    "Вакансия 7",
    "Вакансия 11",
    "Вакансия 4",
    "Вакансия 8",
    "Вакансия 12",
  ];

  if (!currentUser) {
    return <RedirectToLogin />;
  }

  return (
    <div className="min-h-screen">
      <div className="flex justify-center items-center mb-8">
        <button className="bg-green-800 hover:bg-green-700 text-white px-6 py-3 rounded-4xl font-medium">
          Создать вакансию
        </button>
      </div>

      <div className="grid grid-cols-3 gap-5">
        {vacancies.map((vacancy, index) => (
          <div key={index} className="bg-gray-800 p-5 rounded-4xl relative">
            <div className="absolute top-3 right-3">
              <div
                className={`w-4 h-4 rounded-full ${
                  index % 2 === 0 ? "bg-green-800" : "bg-red-800"
                }`}
              ></div>
            </div>
            <h3 className="text-white font-medium">{vacancy}</h3>
          </div>
        ))}
      </div>
    </div>
  );
};
